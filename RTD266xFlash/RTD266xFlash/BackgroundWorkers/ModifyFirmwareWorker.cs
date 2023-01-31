using System;
using System.ComponentModel;
using System.Drawing;
using System.IO;

namespace RTD266xFlash.BackgroundWorkers
{
    public class ModifyFirmwareWorker : BaseWorker
    {
        private readonly string _logoFileName;

        private readonly Color _logoBackground;

        private readonly Color _logoForeground;

        private readonly Color _displayBackground;

        private readonly bool _removeHdmi;

        private readonly string _replaceHdmi;

        private readonly bool _removeNoSignal;

        public delegate void ModifyFirmwareWorkerFinishedEvent(RTD266x.Result result);
        public event ModifyFirmwareWorkerFinishedEvent ModifyFirmwareWorkerFinished;

        public ModifyFirmwareWorker(RTD266x rtd, string logoFileName, Color logoBackground, Color logoForeground, Color displayBackground, bool removeHdmi, string replaceHdmi, bool removeNoSignal) : base(rtd)
        {
            _logoFileName = logoFileName;
            _logoBackground = logoBackground;
            _logoForeground = logoForeground;
            _displayBackground = displayBackground;
            _removeHdmi = removeHdmi;
            _replaceHdmi = replaceHdmi;
            _removeNoSignal = removeNoSignal;
        }

        protected override void _backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            string error;

            if (!string.IsNullOrEmpty(_logoFileName))
            {
                ReportStatus("Checking logo file... ");

                if (!FontCoder.CheckFile(_logoFileName, FontCoder.FontWidthKedei, FontCoder.FontHeightKedei, out error))
                {
                    ReportStatus($"Error! {error}\r\n");
                    e.Result = RTD266x.Result.NotOk;
                    return;
                }

                ReportStatus("ok\r\n");
            }

            if (!string.IsNullOrEmpty(_replaceHdmi))
            {
                ReportStatus("Checking \"HDMI\" replacement... ");

                if (!FirmwareModifier.CheckHdmiReplacement(_replaceHdmi, out error))
                {
                    ReportStatus($"{error}\r\n");
                    e.Result = RTD266x.Result.NotOk;
                    return;
                }
                
                ReportStatus("ok\r\n");
            }

            ReportStatus("Identifying device... ");

            RTD266x.Result result;
            RTD266x.StatusInfo status;

            result = _rtd.ReadStatus(out status);

            if (result != RTD266x.Result.Ok || (status.ManufacturerId != 0xC8 && status.ManufacturerId != 0x1C && status.ManufacturerId != 0xC2 && status.ManufacturerId != 0x85) || status.DeviceId != 0x12)
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

            string backupFirmwareFileName = "firmware-" + DateTime.Now.ToString("yyyy-MM-dd HH-mm-ss") + ".bin";

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

            Firmware detectedFirmware = FirmwareModifier.DetectFirmware(firmware);

            if (detectedFirmware == null)
            {
                ReportStatus("Error! Unknown firmware. You can send your firmware and the name of the display (printed on the cable on the back side) to the author (floppes@gmx.de). Maybe it can be added to the known firmwares.\r\n");
                ReportStatus("Trying to automatically analyze the firmware... ");

                detectedFirmware = FirmwareAnalyzer.AnalyzeFirmware(firmware);

                if (detectedFirmware == null)
                {
                    ReportStatus("Error! The firmware could not be analyzed automatically.");
                    e.Result = result;
                    return;
                }

                ReportStatus("Success! The firmware was analyzed automatically. It may be possible to apply the modifications. Please note that this may not be 100 % accurate.\r\n");
            }
            else
            {
                ReportStatus("ok\r\n");
                ReportStatus($"Detected firmware is {detectedFirmware.Name}\r\n");
            }

            if (!string.IsNullOrEmpty(_logoFileName))
            {
                ReportStatus("Converting and embedding the new logo... ");

                if (!FirmwareModifier.ChangeLogo(_logoFileName, firmware, detectedFirmware, out error))
                {
                    ReportStatus($"{error}\r\n");
                    e.Result = result;
                    return;
                }

                ReportStatus("ok\r\n");

                result = WritePatchedSector(firmware, detectedFirmware.LogoOffset);

                if (result != RTD266x.Result.Ok)
                {
                    e.Result = result;
                    return;
                }
            }

            if (_logoBackground != Color.Empty)
            {
                ReportStatus("Patching logo background color... ");

                FirmwareModifier.ChangeLogoBackgroundColor(_logoBackground, firmware, detectedFirmware);

                ReportStatus("ok\r\n");
            }

            if (_logoForeground != Color.Empty)
            {
                ReportStatus("Patching logo foreground color... ");

                FirmwareModifier.ChangeLogoForegroundColor(_logoForeground, firmware, detectedFirmware);

                ReportStatus("ok\r\n");
            }

            if ((_logoBackground != Color.Empty) || (_logoForeground != Color.Empty))
            {
                result = WritePatchedSector(firmware, detectedFirmware.PaletteOffset);

                if (result != RTD266x.Result.Ok)
                {
                    e.Result = result;
                    return;
                }
            }

            if (_displayBackground != Color.Empty)
            {
                ReportStatus("Patching display background color... ");

                FirmwareModifier.ChangeBackgroundColor(_displayBackground, firmware, detectedFirmware);

                ReportStatus("ok\r\n");

                result = WritePatchedSector(firmware, detectedFirmware.AdjustBackgroundColorOffset);

                if (result != RTD266x.Result.Ok)
                {
                    e.Result = result;
                    return;
                }
            }

            if (_removeHdmi)
            {
                ReportStatus("Removing \"HDMI\" pop-up... ");

                FirmwareModifier.ToggleHdmiPopup(false, firmware, detectedFirmware);

                ReportStatus("ok\r\n");
            }
            else
            {
                ReportStatus("Enabling \"HDMI\" pop-up... ");

                FirmwareModifier.ToggleHdmiPopup(true, firmware, detectedFirmware);

                ReportStatus("ok\r\n");
            }

            result = WritePatchedSector(firmware, detectedFirmware.ShowNoteOffset);

            if (result != RTD266x.Result.Ok)
            {
                e.Result = result;
                return;
            }

            if (_removeNoSignal)
            {
                ReportStatus("Removing \"No Signal\" pop-up...");

                FirmwareModifier.ToggleNoSignalPopup(false, firmware, detectedFirmware);

                ReportStatus("ok\r\n");
            }
            else
            {
                ReportStatus("Enabling \"No Signal\" pop-up...");

                FirmwareModifier.ToggleNoSignalPopup(true, firmware, detectedFirmware);

                ReportStatus("ok\r\n");
            }

            result = WritePatchedSector(firmware, detectedFirmware.NoSignalOffset);

            if (result != RTD266x.Result.Ok)
            {
                e.Result = result;
                return;
            }

            if (!string.IsNullOrEmpty(_replaceHdmi))
            {
                ReportStatus($"Replacing \"HDMI\" pop-up with \"{_replaceHdmi}\"... ");

                FirmwareModifier.ReplaceHdmiPopup(_replaceHdmi, firmware, detectedFirmware);

                ReportStatus("ok\r\n");

                result = WritePatchedSector(firmware, detectedFirmware.HdmiStringOffset);

                if (result != RTD266x.Result.Ok)
                {
                    e.Result = result;
                    return;
                }
            }

            ReportStatus("Finished! Now reboot the display and enjoy your new firmware :)\r\n");

            e.Result = RTD266x.Result.Ok;
        }

        protected override void _backgroundWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ModifyFirmwareWorkerFinished?.Invoke((RTD266x.Result)e.Result);
        }

        private RTD266x.Result WritePatchedSector(byte[] firmware, int address)
        {
            int sectorAddress = (address / 4096) * 4096;
            byte[] sector = new byte[4096];

            Array.Copy(firmware, sectorAddress, sector, 0, sector.Length);

            ReportStatus("Writing patched sector...\r\n");

            RTD266x.Result result = Write(sectorAddress, sector, true);

            if (result != RTD266x.Result.Ok)
            {
                ReportStatus(RTD266x.ResultToString(result) + "\r\n");
            }

            return result;
        }
    }
}
