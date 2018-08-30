using System;
using System.Collections.Generic;
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

        public delegate void ModifyFirmwareWorkerFinishedEvent(RTD266x.Result result);
        public event ModifyFirmwareWorkerFinishedEvent ModifyFirmwareWorkerFinished;

        /// <summary>
        /// Known firmwared
        /// </summary>
        private readonly Firmware[] _firmwares =
        {
            new Firmware("KeDei v1.0", 0x260D8, 1507, 0x12346, 0xD237, 0x14733, 0x13A31, new[]
            {
                new HashInfo(0, 0x80000, "B325D2F791EF9122D7E84E2B261CEEA08A2672C94EC851CAC5480D43EA314B25", new []
                {
                    new HashSkip(0xD254, 48),  // CAdjustBackgroundColor
                    new HashSkip(0x12346, 16), // "HDMI"
                    new HashSkip(0x13A31, 48), // palette
                    new HashSkip(0x14733, 1),  // CShowNote
                    new HashSkip(0x260D8, 903) // logo
                })
            }),
            new Firmware("KeDei v1.1, panel type 1 (SKY035S13B00-14439)", 0x260D8, 1507, 0x12346, 0xD432, 0x14733, 0x13A31, new[]
            {
                new HashInfo(0, 0x80000, "ADAFD43BE9962E1383CE0223D44E53925C076013C6A88167451DD84F7C47AD42", new []
                {
                    new HashSkip(0xD44F, 48),
                    new HashSkip(0x12346, 16),
                    new HashSkip(0x13A31, 48),
                    new HashSkip(0x14733, 1),
                    new HashSkip(0x260D8, 903)
                })
            }),
            new Firmware("KeDei v1.1, panel type 2 (SKY035S13D-199)", 0x260D8, 1507, 0x12346, 0xD2A5, 0x14733, 0x13A31, new[]
            {
                new HashInfo(0, 0x80000, "FE61C30E7F78D342426BD175312E57309E3993356ED670155D02A5D4DD7405F9", new []
                {
                    new HashSkip(0xD2C2, 48),
                    new HashSkip(0x12346, 16),
                    new HashSkip(0x13A31, 48),
                    new HashSkip(0x14733, 1),
                    new HashSkip(0x260D8, 903)
                })
            }),
            new Firmware("KeDei v1.1, panel type 3 (SKY035S13E-180)", 0x260D8, 1507, 0x12346, 0xD2C9, 0x14733, 0x13A31, new[]
            {
                new HashInfo(0, 0x80000, "BF593462795B3AFD00AAAF7914693B8A8665B078F864AF5B55FC61C24F07F264", new []
                {
                    new HashSkip(0xD2E6, 48),
                    new HashSkip(0x12346, 16),
                    new HashSkip(0x13A31, 48),
                    new HashSkip(0x14733, 1),
                    new HashSkip(0x260D8, 903)
                })
            })
        };

        /// <summary>
        /// Character mapping to internal font
        /// </summary>
        private readonly Dictionary<char, byte[]> _osdCharacters = new Dictionary<char, byte[]>
        {
            { ' ', new byte[] { 0x01 }},
            { 'A', new byte[] { 0x10, 0x11 }},
            { 'B', new byte[] { 0x12 }},
            { 'C', new byte[] { 0x13 }},
            { 'D', new byte[] { 0x14 }},
            { 'E', new byte[] { 0x15 }},
            { 'F', new byte[] { 0x16 }},
            { 'G', new byte[] { 0x17, 0x18 }},
            { 'H', new byte[] { 0x19 }},
            { 'I', new byte[] { 0x1A }},
            { 'J', new byte[] { 0x1B }},
            { 'K', new byte[] { 0x1C }},
            { 'L', new byte[] { 0x1D }},
            { 'M', new byte[] { 0x1E, 0x1F }},
            { 'N', new byte[] { 0x20 }},
            { 'O', new byte[] { 0x21, 0x22 }},
            { 'P', new byte[] { 0x23 }},
            { 'Q', new byte[] { 0x24, 0x25 }},
            { 'R', new byte[] { 0x26 }},
            { 'S', new byte[] { 0x27 }},
            { 'T', new byte[] { 0x28 }},
            { 'U', new byte[] { 0x29 }},
            { 'V', new byte[] { 0x2A }},
            { 'W', new byte[] { 0x2B, 0x2C }},
            { 'X', new byte[] { 0x2D }},
            { 'Y', new byte[] { 0x2E }},
            { 'Z', new byte[] { 0x2F }},
            { '0', new byte[] { 0x30 }},
            { '1', new byte[] { 0x31 }},
            { '2', new byte[] { 0x32 }},
            { '3', new byte[] { 0x33 }},
            { '4', new byte[] { 0x34 }},
            { '5', new byte[] { 0x35 }},
            { '6', new byte[] { 0x36 }},
            { '7', new byte[] { 0x37 }},
            { '8', new byte[] { 0x38 }},
            { '9', new byte[] { 0x39 }},
            { 'a', new byte[] { 0x3A }},
            { 'b', new byte[] { 0x3B }},
            { 'c', new byte[] { 0x3C }},
            { 'd', new byte[] { 0x3D }},
            { 'e', new byte[] { 0x3E }},
            { 'f', new byte[] { 0x3F }},
            { 'g', new byte[] { 0x40 }},
            { 'h', new byte[] { 0x41 }},
            { 'i', new byte[] { 0x42 }},
            { 'j', new byte[] { 0x43 }},
            { 'k', new byte[] { 0x44 }},
            { 'l', new byte[] { 0x45 }},
            { 'm', new byte[] { 0x46, 0x47 }},
            { 'n', new byte[] { 0x48 }},
            { 'o', new byte[] { 0x49 }},
            { 'p', new byte[] { 0x4A }},
            { 'q', new byte[] { 0x4B }},
            { 'r', new byte[] { 0x4C }},
            { 's', new byte[] { 0x4D }},
            { 't', new byte[] { 0x4E }},
            { 'u', new byte[] { 0x4F }},
            { 'v', new byte[] { 0x50 }},
            { 'w', new byte[] { 0x51, 0x52 }},
            { 'x', new byte[] { 0x53 }},
            { 'y', new byte[] { 0x54 }},
            { 'z', new byte[] { 0x55 }},
            { ':', new byte[] { 0x5E }},
            { '.', new byte[] { 0x5F }},
        };

        public ModifyFirmwareWorker(RTD266x rtd, string logoFileName, Color logoBackground, Color logoForeground, Color displayBackground, bool removeHdmi, string replaceHdmi) : base(rtd)
        {
            _logoFileName = logoFileName;
            _logoBackground = logoBackground;
            _logoForeground = logoForeground;
            _displayBackground = displayBackground;
            _removeHdmi = removeHdmi;
            _replaceHdmi = replaceHdmi;
        }

        protected override void _backgroundWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            if (!string.IsNullOrEmpty(_logoFileName))
            {
                ReportStatus("Checking logo file... ");

                string error;

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

                if (_replaceHdmi.Length > 8)
                {
                    ReportStatus("Error! String is too long!\r\n");
                    e.Result = RTD266x.Result.NotOk;
                    return;
                }

                foreach (char chr in _replaceHdmi)
                {
                    if (!_osdCharacters.ContainsKey(chr))
                    {
                        ReportStatus($"Error! Invalid character \"{chr}\"!\r\n");
                        e.Result = RTD266x.Result.NotOk;
                        return;
                    }
                }

                ReportStatus("ok\r\n");
            }

            ReportStatus("Identifying device... ");

            RTD266x.Result result;
            RTD266x.StatusInfo status;

            result = _rtd.ReadStatus(out status);

            if (result != RTD266x.Result.Ok || (status.ManufacturerId != 0xC8 && status.ManufacturerId != 0x1C) || status.DeviceId != 0x12)
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
                if (fw.CheckHash(firmware))
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

            if (!string.IsNullOrEmpty(_logoFileName))
            {
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

                firmware[detectedFirmware.PaletteOffset + 42] = _logoBackground.R;
                firmware[detectedFirmware.PaletteOffset + 43] = _logoBackground.G;
                firmware[detectedFirmware.PaletteOffset + 44] = _logoBackground.B;

                ReportStatus("ok\r\n");
            }

            if (_logoForeground != Color.Empty)
            {
                ReportStatus("Patching logo foreground color... ");

                firmware[detectedFirmware.PaletteOffset + 12] = _logoForeground.R;
                firmware[detectedFirmware.PaletteOffset + 13] = _logoForeground.G;
                firmware[detectedFirmware.PaletteOffset + 14] = _logoForeground.B;

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

                firmware[detectedFirmware.AdjustBackgroundColorOffset + 0x1D] = 0x7D; // MOV R5
                firmware[detectedFirmware.AdjustBackgroundColorOffset + 0x1E] = _displayBackground.R;
                firmware[detectedFirmware.AdjustBackgroundColorOffset + 0x1F] = 0x00; // NOP

                firmware[detectedFirmware.AdjustBackgroundColorOffset + 0x23] = 0x7D; // MOV R5
                firmware[detectedFirmware.AdjustBackgroundColorOffset + 0x24] = _displayBackground.G;
                firmware[detectedFirmware.AdjustBackgroundColorOffset + 0x25] = 0x00; // NOP

                firmware[detectedFirmware.AdjustBackgroundColorOffset + 0x29] = 0x7D; // MOV R5
                firmware[detectedFirmware.AdjustBackgroundColorOffset + 0x2A] = _displayBackground.B;
                firmware[detectedFirmware.AdjustBackgroundColorOffset + 0x2B] = 0x00; // NOP
                firmware[detectedFirmware.AdjustBackgroundColorOffset + 0x2C] = 0x00; // NOP
                firmware[detectedFirmware.AdjustBackgroundColorOffset + 0x2D] = 0x00; // NOP

                firmware[detectedFirmware.AdjustBackgroundColorOffset + 0x3C] = 0x00; // NOP
                firmware[detectedFirmware.AdjustBackgroundColorOffset + 0x3D] = 0x00; // NOP

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

                firmware[detectedFirmware.ShowNoteOffset] = 0x22; // RET

                ReportStatus("ok\r\n");

                result = WritePatchedSector(firmware, detectedFirmware.ShowNoteOffset);

                if (result != RTD266x.Result.Ok)
                {
                    e.Result = result;
                    return;
                }
            }
            else
            {
                ReportStatus("Enabling \"HDMI\" pop-up... ");

                firmware[detectedFirmware.ShowNoteOffset] = 0xE4; // CLR A

                ReportStatus("ok\r\n");

                result = WritePatchedSector(firmware, detectedFirmware.ShowNoteOffset);

                if (result != RTD266x.Result.Ok)
                {
                    e.Result = result;
                    return;
                }
            }

            if (!string.IsNullOrEmpty(_replaceHdmi))
            {
                ReportStatus($"Replacing \"HDMI\" pop-up with \"{_replaceHdmi}\"... ");

                int offset = 0;

                foreach (char chr in _replaceHdmi)
                {
                    byte[] bytes = _osdCharacters[chr];

                    foreach (byte b in bytes)
                    {
                        firmware[detectedFirmware.HdmiStringOffset + offset] = b;
                        offset++;
                    }
                }

                firmware[detectedFirmware.HdmiStringOffset + offset] = 0x00;

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
