using System;
using System.Collections.Generic;
using System.Drawing;

namespace RTD266xFlash
{
    public static class FirmwareModifier
    {
        /// <summary>
        /// Character mapping to internal font
        /// </summary>
        private static readonly Dictionary<char, byte[]> _osdCharacters = new Dictionary<char, byte[]>
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

        public static Firmware DetectFirmware(byte[] firmware)
        {
            Firmware detectedFirmware = null;

            foreach (Firmware fw in Firmware.KnownFirmwares)
            {
                if (fw.CheckHash(firmware))
                {
                    detectedFirmware = fw;
                    break;
                }
            }

            return detectedFirmware;
        }

        public static bool ChangeLogo(string logoFileName, byte[] firmware, Firmware detectedFirmware, out string error)
        {
            error = string.Empty;

            FontCoder logo = new FontCoder(FontCoder.FontWidthKedei, FontCoder.FontHeightKedei);

            if (!logo.LoadImage(logoFileName))
            {
                error = $"Error! Cannot load logo from \"{logoFileName}\".";
                return false;
            }

            byte[] logoBytes = logo.Encode();

            if (logoBytes.Length > detectedFirmware.MaxLogoLength)
            {
                error = "Error! Encoded logo is too long and would overwrite other firmware parts.";
                return false;
            }

            Array.Copy(logoBytes, 0, firmware, detectedFirmware.LogoOffset, logoBytes.Length);

            return true;
        }

        public static void ChangeLogoBackgroundColor(Color color, byte[] firmware, Firmware detectedFirmware)
        {
            firmware[detectedFirmware.PaletteOffset + 42] = color.R;
            firmware[detectedFirmware.PaletteOffset + 43] = color.G;
            firmware[detectedFirmware.PaletteOffset + 44] = color.B;
        }

        public static void ChangeLogoForegroundColor(Color color, byte[] firmware, Firmware detectedFirmware)
        {
            firmware[detectedFirmware.PaletteOffset + 12] = color.R;
            firmware[detectedFirmware.PaletteOffset + 13] = color.G;
            firmware[detectedFirmware.PaletteOffset + 14] = color.B;
        }

        public static void ChangeBackgroundColor(Color color, byte[] firmware, Firmware detectedFirmware)
        {
            firmware[detectedFirmware.AdjustBackgroundColorOffset + 0x1D] = 0x7D; // MOV R5
            firmware[detectedFirmware.AdjustBackgroundColorOffset + 0x1E] = color.R;
            firmware[detectedFirmware.AdjustBackgroundColorOffset + 0x1F] = 0x00; // NOP

            firmware[detectedFirmware.AdjustBackgroundColorOffset + 0x23] = 0x7D; // MOV R5
            firmware[detectedFirmware.AdjustBackgroundColorOffset + 0x24] = color.G;
            firmware[detectedFirmware.AdjustBackgroundColorOffset + 0x25] = 0x00; // NOP

            firmware[detectedFirmware.AdjustBackgroundColorOffset + 0x29] = 0x7D; // MOV R5
            firmware[detectedFirmware.AdjustBackgroundColorOffset + 0x2A] = color.B;
            firmware[detectedFirmware.AdjustBackgroundColorOffset + 0x2B] = 0x00; // NOP
            firmware[detectedFirmware.AdjustBackgroundColorOffset + 0x2C] = 0x00; // NOP
            firmware[detectedFirmware.AdjustBackgroundColorOffset + 0x2D] = 0x00; // NOP

            firmware[detectedFirmware.AdjustBackgroundColorOffset + 0x3C] = 0x00; // NOP
            firmware[detectedFirmware.AdjustBackgroundColorOffset + 0x3D] = 0x00; // NOP
        }
        
        public static bool CheckHdmiReplacement(string hdmiReplacement, out string error)
        {
            error = string.Empty;

            if (hdmiReplacement.Length > 8)
            {
                error = "Error! String is too long!";
                return false;
            }

            foreach (char chr in hdmiReplacement)
            {
                if (!_osdCharacters.ContainsKey(chr))
                {
                    error = $"Error! Invalid character \"{chr}\"!";
                    return false;
                }
            }
            
            return true;
        }

        public static void ToggleHdmiPopup(bool enabled, byte[] firmware, Firmware detectedFirmware)
        {
            if (enabled)
            {
                firmware[detectedFirmware.ShowNoteOffset] = 0xE4; // CLR A
            }
            else
            {
                firmware[detectedFirmware.ShowNoteOffset] = 0x22; // RET
            }
        }

        public static void ReplaceHdmiPopup(string hdmiReplacement, byte[] firmware, Firmware detectedFirmware)
        {
            int offset = 0;

            foreach (char chr in hdmiReplacement)
            {
                byte[] bytes = _osdCharacters[chr];

                foreach (byte b in bytes)
                {
                    firmware[detectedFirmware.HdmiStringOffset + offset] = b;
                    offset++;
                }
            }

            firmware[detectedFirmware.HdmiStringOffset + offset] = 0x00;
        }
    }
}
