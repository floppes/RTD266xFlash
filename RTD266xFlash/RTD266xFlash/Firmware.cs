namespace RTD266xFlash
{
    public class Firmware
    {
        /// <summary>
        /// Firmware name
        /// </summary>
        public string Name
        {
            get;
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
        /// Hashes for identifying the firmware
        /// </summary>
        public HashInfo[] Hashes
        {
            get;
        }

        /// <summary>
        /// Known firmwares
        /// </summary>
        public static readonly Firmware[] KnownFirmwares =
        {
            new Firmware("KeDei v1.0", 0x260D8, 1507, 0x12346, 0xD237, 0x14733, 0x13A31, new[]
            {
                new HashInfo(0, 0x80000, "8B3A476ED11F6B802D425851DF9A220F9CD11386B60C3FA5D05FEE404E00900B", new []
                {
                    new HashSkip(0xD254, 48),   // CAdjustBackgroundColor
                    new HashSkip(0x12346, 16),  // "HDMI"
                    new HashSkip(0x13A31, 48),  // palette
                    new HashSkip(0x14733, 1),   // CShowNote
                    new HashSkip(0x260D8, 1507) // logo
                })
            }),
            new Firmware("KeDei v1.1, panel type 1 (SKY035S13B00-14439)", 0x260D8, 1507, 0x12346, 0xD432, 0x14733, 0x13A31, new[]
            {
                new HashInfo(0, 0x80000, "B76EDB90981D998673B8E28A4B3EC2A0312AB3648DFEDB360290A484BA54962D", new []
                {
                    new HashSkip(0xD44F, 48),
                    new HashSkip(0x12346, 16),
                    new HashSkip(0x13A31, 48),
                    new HashSkip(0x14733, 1),
                    new HashSkip(0x260D8, 1507)
                })
            }),
            new Firmware("KeDei v1.1, panel type 2 (SKY035S13D-199), FW variant 1", 0x260D8, 1507, 0x12346, 0xD2A5, 0x14733, 0x13A31, new[]
            {
                new HashInfo(0, 0x80000, "6CA952288044CF62C0BAC24758761850071B82B16120848370FE045ED5E5ECF9", new []
                {
                    new HashSkip(0xD2C2, 48),
                    new HashSkip(0x12346, 16),
                    new HashSkip(0x13A31, 48),
                    new HashSkip(0x14733, 1),
                    new HashSkip(0x260D8, 1507)
                })
            }),
            new Firmware("KeDei v1.1, panel type 2 (SKY035S13D-199), FW variant 2", 0x260D8, 1507, 0x12346, 0xD341, 0x14733, 0x13A31, new[]
            {
                new HashInfo(0, 0x80000, "600DDF52D1FB753C3FA4EA81CE26BD6FF5CBEF0A3ECAAAE1CA8C726F2CDC3B17", new []
                {
                    new HashSkip(0xD35E, 48),
                    new HashSkip(0x12346, 16),
                    new HashSkip(0x13A31, 48),
                    new HashSkip(0x14733, 1),
                    new HashSkip(0x260D8, 1507)
                })
            }),
            new Firmware("KeDei v1.1, panel type 3 (SKY035S13E-180), FW variant 1", 0x260D8, 1507, 0x12346, 0xD2C9, 0x14733, 0x13A31, new[]
            {
                new HashInfo(0, 0x80000, "B66467BA9FCD80CC5411DD200C07E048A069DA4648E6872F42E5B01904B6DDE6", new []
                {
                    new HashSkip(0xD2E6, 48),
                    new HashSkip(0x12346, 16),
                    new HashSkip(0x13A31, 48),
                    new HashSkip(0x14733, 1),
                    new HashSkip(0x260D8, 1507)
                })
            }),
            new Firmware("KeDei v1.1, panel type 3 (SKY035S13E-180), FW variant 2", 0x260D8, 1507, 0x12346, 0xD2C9, 0x14733, 0x13A31, new[]
            {
                new HashInfo(0, 0x80000, "E9B425767FD2159ED716EF5EB052622A3527287C29656B8605942CAE131276E9", new []
                {
                    new HashSkip(0xD2E6, 48),
                    new HashSkip(0x12346, 16),
                    new HashSkip(0x13A31, 48),
                    new HashSkip(0x14733, 1),
                    new HashSkip(0x260D8, 1507)
                })
            }),
            new Firmware("KeDei v1.1, panel type 4 (LGH-3509)", 0x260D8, 1507, 0x12346, 0xD22D, 0x14733, 0x13A31, new[]
            {
                new HashInfo(0, 0x80000, "057AA1A7F26B32677AFE137A80F7E46A5736FB5A09A56BC10EB71AE309C9C37D", new []
                {
                    new HashSkip(0xD24A, 48),
                    new HashSkip(0x12346, 16),
                    new HashSkip(0x13A31, 48),
                    new HashSkip(0x14733, 1),
                    new HashSkip(0x260D8, 1507)
                })
            })
        };

        public Firmware(string name, int logoOffset, int maxLogoLength, int hdmiStringOffset, int adjustBackgroundColorOffset, int showNoteOffset, int paletteOffset, HashInfo[] hashes)
        {
            Name = name;
            LogoOffset = logoOffset;
            MaxLogoLength = maxLogoLength;
            HdmiStringOffset = hdmiStringOffset;
            AdjustBackgroundColorOffset = adjustBackgroundColorOffset;
            ShowNoteOffset = showNoteOffset;
            PaletteOffset = paletteOffset;
            Hashes = hashes;
        }

        /// <summary>
        /// Checks if all of the hashes matches the specified firmware
        /// </summary>
        /// <param name="firmware">Firmware</param>
        /// <returns>True if all hashes match, otherwise false</returns>
        public bool CheckHash(byte[] firmware)
        {
            foreach (HashInfo hash in Hashes)
            {
                string firmwareHash = hash.GetHash(firmware);

                if (string.IsNullOrEmpty(firmwareHash))
                {
                    return false;
                }

                if (firmwareHash != hash.Hash)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
