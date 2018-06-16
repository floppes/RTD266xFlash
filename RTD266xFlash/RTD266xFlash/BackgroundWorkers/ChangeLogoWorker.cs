using System;
using System.ComponentModel;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace RTD266xFlash.BackgroundWorkers
{
    public class ChangeLogoWorker : BaseWorker
    {
        private readonly string _logoFileName;

        public delegate void ChangeLogoeWorkerFinishedEvent(RTD266x.Result result);
        public event ChangeLogoeWorkerFinishedEvent ChangeLogoWorkerFinished;

        private readonly Firmware[] _firmwares =
        {
            new Firmware("KeDei v1.0", 0x260D8, 0x12346, 1507, new HashInfo[]
            {
                new HashInfo(0, 0x12346, "9F8FE8DEC3783B239172F442D6F26B856AE49112E196B98571ED539189C83F1C"),
                new HashInfo(0x1234C, 0x13D8C, "6E27CC989A3DEE14D0DE54952F62ADF8AF31467DA9ED9D857C88A2CB7635ECA8")
            }),
            new Firmware("KeDei v1.1", 0x260D8, 0x12346, 1507, new HashInfo[]
            {
                new HashInfo(0, 0x12346, "F3931E17F6E033A9FA5BAA7785153B40D1AA79BD0F96E9252035BC90543ADD57"),
                new HashInfo(0x1234C, 0x13D8C, "52BB272C51DA5FCC172C73B2DD4E096F2D97E04C6A77E467097463F30DDD8375")
            })
        };

        public ChangeLogoWorker(RTD266x rtd, string logoFileName) : base(rtd)
        {
            _logoFileName = logoFileName;
        }

        protected override void _backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (string.IsNullOrEmpty(_logoFileName))
            {
                ReportStatus("Error! No logo input file specified.\r\n");
                e.Result = RTD266x.Result.NotOk;
                return;
            }

            ReportStatus("Checking logo file... ");

            string error;

            if (!FontCoder.CheckFile(_logoFileName, FontCoder.FontWidthKedei, FontCoder.FontHeightKedei, out error))
            {
                ReportStatus($"Error! {error}\r\n");
                e.Result = RTD266x.Result.NotOk;
                return;
            }

            ReportStatus("ok\r\n");
            ReportStatus("Identifying device... ");

            RTD266x.Result result;
            RTD266x.StatusInfo status;

            result = _rtd.ReadStatus(out status);

            if (result != RTD266x.Result.Ok || status.ManufacturerId != 0xC8 || status.DeviceId != 0x12)
            {
                ReportStatus("Error! Cannot identify chip.\r\n");
                e.Result = result;
                return;
            }

            ReportStatus("ok\r\n");
            ReportStatus("Reading firmware...\r\n");

            byte[] firmware;

            result = Read(0, 512 * 1024, out firmware, true);

            if (result != RTD266x.Result.Ok)
            {
                ReportStatus(RTD266x.ResultToString(result) + "\r\n");
                e.Result = result;
                return;
            }

            string backupFirmwareFileName = "firmware-" + DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss") + ".bin";

            ReportStatus($"Creating firmware backup file \"{backupFirmwareFileName}\"... ");

            try
            {
                File.WriteAllBytes(backupFirmwareFileName, firmware);
            }
            catch (Exception ex)
            {
                ReportStatus($"Error! Could not save file \"{backupFirmwareFileName}\". {ex.Message}\r\n");
                e.Result = result;
                return;
            }

            ReportStatus("ok\r\n");
            ReportStatus("Checking firmware... ");

            Firmware detectedFirmware = null;

            foreach (Firmware fw in _firmwares)
            {
                bool hashesMatch = true;

                foreach (HashInfo hash in fw.Hashes)
                {
                    if (Sha256Hash(firmware, hash.Start, hash.Length) != hash.Hash)
                    {
                        hashesMatch = false;
                    }
                }

                if (hashesMatch)
                {
                    detectedFirmware = fw;
                    break;
                }
            }

            if (detectedFirmware == null)
            {
                ReportStatus("Error! Could not detect firmware.\r\n");
                e.Result = result;
                return;
            }

            ReportStatus("ok\r\n");
            ReportStatus($"Detected firmware is {detectedFirmware.Name}\r\n");
            ReportStatus("Converting logo... ");

            FontCoder logo = new FontCoder(FontCoder.FontWidthKedei, FontCoder.FontHeightKedei);

            if (!logo.LoadImage(_logoFileName))
            {
                ReportStatus($"Error! Cannot load logo from \"{_logoFileName}\".\r\n");
                e.Result = result;
                return;
            }

            byte[] logoBytes = logo.Encode();

            if (logoBytes.Length > detectedFirmware.MaxLogoLength)
            {
                ReportStatus("Error! Encoded logo is too long and would overwrite other firmware parts.\r\n");
                e.Result = result;
                return;
            }

            ReportStatus("ok\r\n");
            ReportStatus("Embedding the new logo... ");

            Array.Copy(logoBytes, 0, firmware, detectedFirmware.LogoOffset, logoBytes.Length);

            ReportStatus("ok\r\n");
            ReportStatus("Writing patched sector...\r\n");

            byte[] sector = new byte[4096];
            int sectorAddress = (detectedFirmware.LogoOffset / 4096) * 4096;

            Array.Copy(firmware, sectorAddress, sector, 0, sector.Length);

            if (Write(sectorAddress, sector, true) != RTD266x.Result.Ok)
            {
                e.Result = result;
                return;
            }

            ReportStatus("Finished! Now reboot the display and enjoy your new boot logo :)\r\n");

            e.Result = RTD266x.Result.Ok;
        }

        protected override void _backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ChangeLogoWorkerFinished?.Invoke((RTD266x.Result)e.Result);
        }

        private string Sha256Hash(byte[] data, int offset, int length)
        {
            SHA256Managed sha256 = new SHA256Managed();

            byte[] hash = sha256.ComputeHash(data, offset, length);

            StringBuilder hashString = new StringBuilder();

            foreach (byte hashByte in hash)
            {
                hashString.Append($"{hashByte:X2}");
            }

            return hashString.ToString();
        }
    }
}
