using System.Collections.Generic;
using System.IO;

namespace RTD266xFlash
{
    public class Firmware
    {
        /// <summary>
        /// Firmware name
        /// </summary>
        public string Name
        {
            get; set;
        }

        /// <summary>
        /// Logo offset
        /// </summary>
        public int LogoOffset
        {
            get;
        }

        /// <summary>
        /// Maximum logo length
        /// </summary>
        public int MaxLogoLength
        {
            get;
        }

        /// <summary>
        /// "HDMI" string offset
        /// </summary>
        public int HdmiStringOffset
        {
            get;
        }

        /// <summary>
        /// Offset of function AdjustBackgroundColor
        /// </summary>
        public int AdjustBackgroundColorOffset
        {
            get;
        }

        /// <summary>
        /// Offset of function ShowNote
        /// </summary>
        public int ShowNoteOffset
        {
            get;
        }

        /// <summary>
        /// Palette offset
        /// </summary>
        public int PaletteOffset
        {
            get;
        }

        /// <summary>
        /// Offset of function CShowNoSignal
        /// </summary>
        public int NoSignalOffset
        {
            get;
        }

        /// <summary>
        /// Hash info for identifying the firmware
        /// </summary>
        public HashInfo HashInfo
        {
            get;
        }

        /// <summary>
        /// Known firmwares
        /// </summary>
        public static Firmware[] KnownFirmwares;

        public Firmware(string name, int logoOffset, int maxLogoLength, int hdmiStringOffset, int adjustBackgroundColorOffset, int showNoteOffset, int paletteOffset, int noSignalOffset, HashInfo hashInfo)
        {
            Name = name;
            LogoOffset = logoOffset;
            MaxLogoLength = maxLogoLength;
            HdmiStringOffset = hdmiStringOffset;
            AdjustBackgroundColorOffset = adjustBackgroundColorOffset;
            ShowNoteOffset = showNoteOffset;
            PaletteOffset = paletteOffset;
            NoSignalOffset = noSignalOffset;
            HashInfo = hashInfo;
        }

        public void ToFile(string path, byte[] firmware = null)
        {
            File.WriteAllLines(path, new string[] 
            {
                "[Metadata]",
                "Name="+Name,
                "Start=0x"+HashInfo.Start.ToString("X1"),
                "Length=0x"+HashInfo.Length.ToString("X1"),
                "Hash="+HashInfo.Hash, //firmware == null ? HashInfo.Hash : HashInfo.GetHash(firmware),
                "[Offsets]",
                "CAdjustBackgroundColor=0x"+AdjustBackgroundColorOffset.ToString("X1"),
                "HDMIPopup=0x"+HdmiStringOffset.ToString("X1"),
                "Palette=0x"+PaletteOffset.ToString("X1"),
                "CShowNote=0x"+ShowNoteOffset.ToString("X1"),
                "CShowNoSignal=0x"+NoSignalOffset.ToString("X1"),
                "Logo=0x"+LogoOffset.ToString("X1"),
                "[HashInfoOffsets]",
                "CAdjustBackgroundColor=0x"+ (HashInfo.SkippedSections[0].Offset > AdjustBackgroundColorOffset ? (HashInfo.SkippedSections[0].Offset - AdjustBackgroundColorOffset) : (AdjustBackgroundColorOffset - HashInfo.SkippedSections[0].Offset)).ToString("X1"),
                "HDMIPopup=0x"+ (HashInfo.SkippedSections[1].Offset > HdmiStringOffset ? (HashInfo.SkippedSections[1].Offset - HdmiStringOffset) : (HdmiStringOffset - HashInfo.SkippedSections[1].Offset)).ToString("X1"),
                "Palette=0x"+ (HashInfo.SkippedSections[2].Offset > PaletteOffset ? (HashInfo.SkippedSections[2].Offset - PaletteOffset) : (PaletteOffset - HashInfo.SkippedSections[2].Offset)).ToString("X1"),
                "CShowNote=0x"+ (HashInfo.SkippedSections[3].Offset > ShowNoteOffset ? (HashInfo.SkippedSections[3].Offset - ShowNoteOffset) : (ShowNoteOffset - HashInfo.SkippedSections[3].Offset)).ToString("X1"),
                "CShowNoSignal=0x"+ (HashInfo.SkippedSections[4].Offset > NoSignalOffset ? (HashInfo.SkippedSections[4].Offset - NoSignalOffset) : (NoSignalOffset - HashInfo.SkippedSections[4].Offset)).ToString("X1"),
                "Logo=0x"+ (HashInfo.SkippedSections[5].Offset > LogoOffset ? (HashInfo.SkippedSections[5].Offset - LogoOffset) : (LogoOffset - HashInfo.SkippedSections[5].Offset)).ToString("X1"),
                "[Lengths]",
                "CAdjustBackgroundColor=0x"+HashInfo.SkippedSections[0].Length.ToString("X1"),
                "HDMIPopup=0x"+HashInfo.SkippedSections[1].Length.ToString("X1"),
                "Palette=0x"+HashInfo.SkippedSections[2].Length.ToString("X1"),
                "CShowNote=0x"+HashInfo.SkippedSections[3].Length.ToString("X1"),
                "CShowNoSignal=0x"+HashInfo.SkippedSections[4].Length.ToString("X1"),
                "Logo=0x"+MaxLogoLength.ToString("X1")
            });
        }
        public static Firmware FromFile(string path)
        {
            string[] lines = File.ReadAllLines(path);
            Dictionary<string, string> FileValues = new Dictionary<string, string>();

            string sectionName = "";
            foreach (string line in lines)
            {
                if (line[0] == '[')
                {
                    sectionName = line.Replace("[", null).Replace("]", null);
                }
                else
                {
                    string argName = line.Split('=')[0],
                        value = line.Split('=')[1];
                    FileValues.Add(sectionName + "." + argName, value);
                }
            }
            return new Firmware(
                FileValues["Metadata.Name"],
                ToInt(FileValues["Offsets.Logo"]),
                ToInt(FileValues["Lengths.Logo"]),
                ToInt(FileValues["Offsets.HDMIPopup"]),
                ToInt(FileValues["Offsets.CAdjustBackgroundColor"]),
                ToInt(FileValues["Offsets.CShowNote"]),
                ToInt(FileValues["Offsets.Palette"]),
                ToInt(FileValues["Offsets.CShowNoSignal"]),
                new HashInfo(ToInt(FileValues["Metadata.Start"]), ToInt(FileValues["Metadata.Length"]), FileValues["Metadata.Hash"], new[]
                {
                    new HashSkip(ToInt(FileValues["Offsets.CAdjustBackgroundColor"]) + ToInt(FileValues["HashInfoOffsets.CAdjustBackgroundColor"]), ToInt(FileValues["Lengths.CAdjustBackgroundColor"])), // CAdjustBackgroundColor
                    new HashSkip(ToInt(FileValues["Offsets.HDMIPopup"]) + ToInt(FileValues["HashInfoOffsets.HDMIPopup"]), ToInt(FileValues["Lengths.HDMIPopup"])), // "HDMI"
                    new HashSkip(ToInt(FileValues["Offsets.Palette"]) + ToInt(FileValues["HashInfoOffsets.Palette"]), ToInt(FileValues["Lengths.Palette"])), // palette
                    new HashSkip(ToInt(FileValues["Offsets.CShowNote"]) + ToInt(FileValues["HashInfoOffsets.CShowNote"]), ToInt(FileValues["Lengths.CShowNote"])), // CShowNote
                    new HashSkip(ToInt(FileValues["Offsets.CShowNoSignal"]) + ToInt(FileValues["HashInfoOffsets.CShowNoSignal"]), ToInt(FileValues["Lengths.CShowNoSignal"])), // CShowNoSignal
                    new HashSkip(ToInt(FileValues["Offsets.Logo"]) + ToInt(FileValues["HashInfoOffsets.Logo"]), ToInt(FileValues["Lengths.Logo"])) // logo
                }));
        }
        private static int ToInt(string text)
        {
            return int.Parse(text.Replace("0x", null), text.StartsWith("0x") ? System.Globalization.NumberStyles.HexNumber : System.Globalization.NumberStyles.Number);
        }

        /// <summary>
        /// Checks if all of the hashes matches the specified firmware
        /// </summary>
        /// <param name="firmware">Firmware</param>
        /// <returns>True if all hashes match, otherwise false</returns>
        public bool CheckHash(byte[] firmware)
        {
            string firmwareHash = HashInfo.GetHash(firmware);

            if (string.IsNullOrEmpty(firmwareHash))
            {
                return false;
            }

            if (firmwareHash != HashInfo.Hash)
            {
                return false;
            }

            return true;
        }
    }
}
