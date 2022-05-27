using System;
using System.Collections.Generic;
using System.Text;

namespace RTD266xFlash
{
    public class FirmwareAnalyzer
    {
        private static FirmwarePattern _patternCAdjustBackgroundColor = new FirmwarePattern(
                "CAdjustBackgroundColor",
                new byte[] { 0xEF, 0xF0, 0xA3, 0xED, 0xF0, 0xA3, 0xEB, 0xF0, 0xE4, 0xFB, 0x7D, 0xDF, 0x7F, 0x6C, 0x12 },
                new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF },
                -3);

        private static FirmwarePattern _patternHdmi = new FirmwarePattern(
                "sHDMI",
                new byte[] { 0x19, 0x14, 0x1E, 0x1F, 0x1A, 0x00, 0x2E },
                new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF },
                0);

        private static FirmwarePattern _patternPalette = new FirmwarePattern(
                "tPALETTE_0",
                new byte[] { 0x9F, 0xED, 0xAB, 0xD2, 0xC1, 0x8B, 0x2E, 0x6D, 0xA3, 0x00, 0x00, 0x00 },
                new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF },
                0);

        private static FirmwarePattern _patternCShowNote = new FirmwarePattern(
                "CShowNote",
                new byte[] { 0xE4, 0x78, 0x00, 0xF6, 0x12, 0x00, 0x00, 0x90, 0x00, 0x00, 0xE0, 0x30, 0xE3, 0x04, 0x7F, 0x03, 0x80, 0x02, 0x7F, 0x00 },
                new byte[] { 0xFF, 0xFF, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF },
                0);

        private static FirmwarePattern _patternCDisplaySourceMessage = new FirmwarePattern(
                "CDisplayCurrentSourceMessage",
                new byte[] { 0xE4, 0x78, 0x00, 0xF6, 0x12, 0x00, 0x00, 0x30, 0x00, 0x00, 0x7B, 0xFF },
                new byte[] { 0xFF, 0xFF, 0x00, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0x00, 0x00, 0xFF, 0xFF },
                0);

        private static FirmwarePattern _patternFntLogo = new FirmwarePattern(
                "FntLogo",
                new byte[] { 0x1B, 0x00, 0x84, 0x1A, 0x68, 0xD0, 0x37, 0x08, 0x20, 0xEC, 0x03, 0x06 },
                new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF },
                12);

        private static FirmwarePattern _patternFntLogoDot3 = new FirmwarePattern(
                "FntLogoDot3",
                new byte[] { 0x0F, 0xC3, 0x78, 0x1E, 0x49, 0xAB, 0x5D, 0x62, 0x00, 0x3C, 0x00, 0xD0 },
                new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF },
                0);

        private static FirmwarePattern _patternCallCShowNoSignal = new FirmwarePattern(
                "call CShowNoSignal",
                new byte[] { 0xE0, 0x30, 0xE3, 0x04, 0x7F, 0x03, 0x80, 0x02, 0x7F, 0x00, 0xEF, 0x44, 0x0C, 0xFF, 0x12, 0x00, 0x00, 0x12, 0x00, 0x00, 0x7F, 0x01, 0x12, 0x00, 0x00, 0x90, 0x00, 0x00, 0x74, 0xA4, 0xF0, 0x74, 0x50 },
                new byte[] { 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0x00, 0x00, 0xFF, 0x00, 0x00, 0xFF, 0xFF, 0xFF, 0x00, 0x00, 0x90, 0x00, 0x00, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF },
                -6);

        private static FirmwarePattern _patternPanelType = new FirmwarePattern(
                "PanelType",
                new byte[] { 0x04, 0x4C, 0x00, 0x64, 0x03, 0x70, 0x01 },
                new byte[] { 0xFF, 0xFF, 0xF0, 0xFF, 0xFF, 0xFF, 0xFF },
                -22);

        private static FirmwarePattern[] _patterns =
        {
            _patternCAdjustBackgroundColor,
            _patternHdmi,
            _patternPalette,
            _patternCShowNote,
            _patternCDisplaySourceMessage,
            _patternFntLogo,
            _patternFntLogoDot3,
            _patternCallCShowNoSignal,
            _patternPanelType
        };

        public static string AnalyzeFirmwareToString(byte[] firmware)
        {
            StringBuilder output = new StringBuilder();

            // find patterns
            foreach (FirmwarePattern pattern in _patterns)
            {
                output.AppendLine($"Searching for pattern \"{pattern.Name}\"...");

                int[] offsets = FindPattern(firmware, pattern);
                
                foreach (int offset in offsets)
                {
                    output.AppendLine($"0x{(offset):X}");
                }

                output.AppendLine();
            }

            Firmware analyzedFirmware = AnalyzeFirmware(firmware);

            if (analyzedFirmware == null)
            {
                output.AppendLine("AnalyzeFirmware() returned null.");
                output.AppendLine();
            }
            else
            {
                output.AppendLine($"Logo length: {analyzedFirmware.MaxLogoLength}");
                output.AppendLine();
                output.AppendLine($"Hash: {analyzedFirmware.HashInfo.Hash}");
                output.AppendLine();
            }

            int[] panelTypeOffsets = FindPattern(firmware, _patternPanelType);

            if (panelTypeOffsets.Length == 0)
            {
                output.AppendLine("PanelType not found.");
                output.AppendLine();
            }
            else
            {
                byte[] panelType = new byte[34];

                Array.Copy(firmware, panelTypeOffsets[0], panelType, 0, panelType.Length);

                output.AppendLine($"Display resolution: {(panelType[4] << 8) + panelType[5]}x{(panelType[14] << 8) + panelType[15]}");
                output.AppendLine();
            }

            return output.ToString();
        }

        public static Firmware AnalyzeFirmware(byte[] firmware)
        {
            Firmware analyzedFirmware;

            if (firmware.Length != 0x80000)
            {
                return null;
            }

            int[] offsets;
            int logoOffset;
            int maxLogoLength;
            int hdmiStringOffset;
            int adjustBackgroundColorOffset;
            int showNoteOffset;
            int paletteOffset;
            int noSignalOffset;

            offsets = FindPattern(firmware, _patternFntLogo);

            if (offsets.Length == 1)
            {
                logoOffset = offsets[0];
            }
            else
            {
                return null;
            }

            byte[] logoData = new byte[firmware.Length - logoOffset];

            Array.Copy(firmware, logoOffset, logoData, 0, logoData.Length);

            maxLogoLength = FontCoder.DecodeLength(logoData);

            offsets = FindPattern(firmware, _patternHdmi);

            if (offsets.Length == 1)
            {
                hdmiStringOffset = offsets[0];
            }
            else
            {
                return null;
            }

            offsets = FindPattern(firmware, _patternCAdjustBackgroundColor);

            if (offsets.Length == 1)
            {
                adjustBackgroundColorOffset = offsets[0];
            }
            else
            {
                return null;
            }

            offsets = FindPattern(firmware, _patternCShowNote);

            if (offsets.Length == 1)
            {
                showNoteOffset = offsets[0];
            }
            else
            {
                return null;
            }

            offsets = FindPattern(firmware, _patternPalette);

            if (offsets.Length == 1)
            {
                paletteOffset = offsets[0];
            }
            else
            {
                return null;
            }

            offsets = FindPattern(firmware, _patternCallCShowNoSignal);

            if (offsets.Length == 1)
            {
                noSignalOffset = offsets[0];
            }
            else
            {
                return null;
            }

            List<HashSkip> hashSkips = new List<HashSkip>();

            hashSkips.Add(new HashSkip(adjustBackgroundColorOffset + 0x1D, 48));
            hashSkips.Add(new HashSkip(hdmiStringOffset, 16));
            hashSkips.Add(new HashSkip(paletteOffset, 48));
            hashSkips.Add(new HashSkip(showNoteOffset, 1));
            hashSkips.Add(new HashSkip(noSignalOffset, 1));
            hashSkips.Add(new HashSkip(logoOffset, maxLogoLength));

            HashInfo hashInfoTemp = new HashInfo(0, 0x80000, string.Empty, hashSkips.ToArray());

            string hash = hashInfoTemp.GetHash(firmware);

            HashInfo hashInfo = new HashInfo(0, 0x80000, hash, hashSkips.ToArray());

            analyzedFirmware = new Firmware("automatically analyzed", logoOffset, maxLogoLength, hdmiStringOffset, adjustBackgroundColorOffset, showNoteOffset, paletteOffset, noSignalOffset, hashInfo);

            return analyzedFirmware;
        }

        private static int[] FindPattern(byte[] firmware, FirmwarePattern pattern)
        {
            List<int> offsets = new List<int>();
            int patternPosition = 0;

            for (int i = 0; i < firmware.Length; i++)
            {
                if ((firmware[i] & pattern.Mask[patternPosition]) == pattern.Pattern[patternPosition])
                {
                    patternPosition++;

                    if (patternPosition == pattern.Pattern.Length)
                    {
                        offsets.Add(i - patternPosition + 1 + pattern.Offset);

                        patternPosition = 0;
                    }
                }
                else
                {
                    patternPosition = 0;
                }
            }

            return offsets.ToArray();
        }
    }
}
