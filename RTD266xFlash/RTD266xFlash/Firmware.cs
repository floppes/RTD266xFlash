﻿namespace RTD266xFlash
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
        public static readonly Firmware[] KnownFirmwares =
        {
            new Firmware("KeDei v1.0, panel type 1 (S90329A-DT035HV), FW variant 1", 0x260D8, 1507, 0x12346, 0xD237, 0x14733, 0x13A31, 0x15577,
                new HashInfo(0, 0x80000, "7CFE5690907442A634355A016C0717A7D4FF75E1F1571D2FCC4FB453AD3DA483", new []
                {
                    new HashSkip(0xD237 + 0x1D, 48), // CAdjustBackgroundColor
                    new HashSkip(0x12346, 16), // "HDMI"
                    new HashSkip(0x13A31, 48), // palette
                    new HashSkip(0x14733, 1), // CShowNote
                    new HashSkip(0x15577, 1), // CShowNoSignal
                    new HashSkip(0x260D8, 1507) // logo
                })
            ),
            new Firmware("KeDei v1.0, panel type 1 (S90329A-DT035HV), FW variant 2", 0x260D8, 1507, 0x12346, 0xD237, 0x14733, 0x13A31, 0x15577,
                new HashInfo(0, 0x80000, "FC5A211CED95CB10FC739956AB839308CF298B7DD7B078DE6618242788E9E47E", new []
                {
                    new HashSkip(0xD237 + 0x1D, 48),
                    new HashSkip(0x12346, 16),
                    new HashSkip(0x13A31, 48),
                    new HashSkip(0x14733, 1),
                    new HashSkip(0x15577, 1),
                    new HashSkip(0x260D8, 1507)
                })
            ),
            new Firmware("KeDei v1.1, panel type 1 (SKY035S13B00-14439)", 0x260D8, 1507, 0x12346, 0xD432, 0x14733, 0x13A31, 0x15577,
                new HashInfo(0, 0x80000, "71B15FF1D67113982FE62B8C01B349AF778762427076BC620E55963024FA076B", new []
                {
                    new HashSkip(0xD432 + 0x1D, 48),
                    new HashSkip(0x12346, 16),
                    new HashSkip(0x13A31, 48),
                    new HashSkip(0x14733, 1),
                    new HashSkip(0x15577, 1),
                    new HashSkip(0x260D8, 1507)
                })
            ),
            new Firmware("KeDei v1.1, panel type 2 (SKY035S13D-199), FW variant 1", 0x260D8, 1507, 0x12346, 0xD2A5, 0x14733, 0x13A31, 0x15577,
                new HashInfo(0, 0x80000, "513B8CDA8C5C9D73838C9B027E6A07350FC49BE2014EC4540332A6C4D29B6769", new []
                {
                    new HashSkip(0xD2A5 + 0x1D, 48),
                    new HashSkip(0x12346, 16),
                    new HashSkip(0x13A31, 48),
                    new HashSkip(0x14733, 1),
                    new HashSkip(0x15577, 1),
                    new HashSkip(0x260D8, 1507)
                })
            ),
            new Firmware("KeDei v1.1, panel type 2 (SKY035S13D-199), FW variant 2", 0x260D8, 1507, 0x12346, 0xD341, 0x14733, 0x13A31, 0x15577,
                new HashInfo(0, 0x80000, "FD99CDCD4A884DFAC4B015B150DE6EEE0E15B604955AAA10D18B614C5E519F0B", new []
                {
                    new HashSkip(0xD341 + 0x1D, 48),
                    new HashSkip(0x12346, 16),
                    new HashSkip(0x13A31, 48),
                    new HashSkip(0x14733, 1),
                    new HashSkip(0x15577, 1),
                    new HashSkip(0x260D8, 1507)
                })
            ),
            new Firmware("KeDei v1.1, panel type 2 (SKY035S13D-199), FW variant 3", 0x260D8, 1507, 0x12346, 0xD2FA, 0x14733, 0x13A31, 0x15577,
                new HashInfo(0, 0x80000, "01DCE11FB20C636041F1338AD820B084F2E63A86C0D785EE06BCEAC323CFC808", new []
                {
                    new HashSkip(0xD2FA + 0x1D, 48),
                    new HashSkip(0x12346, 16),
                    new HashSkip(0x13A31, 48),
                    new HashSkip(0x14733, 1),
                    new HashSkip(0x15577, 1),
                    new HashSkip(0x260D8, 1507)
                })
            ),
            new Firmware("KeDei v1.1, panel type 3 (SKY035S13E-180), FW variant 1", 0x260D8, 1507, 0x12346, 0xD2C9, 0x14733, 0x13A31, 0x15577,
                new HashInfo(0, 0x80000, "F4F4162FF987330578029CC24E554F3E3A05A3499E4B095B3B53548D34B17DF6", new []
                {
                    new HashSkip(0xD2C9 + 0x1D, 48),
                    new HashSkip(0x12346, 16),
                    new HashSkip(0x13A31, 48),
                    new HashSkip(0x14733, 1),
                    new HashSkip(0x15577, 1),
                    new HashSkip(0x260D8, 1507)
                })
            ),
            new Firmware("KeDei v1.1, panel type 3 (SKY035S13E-180), FW variant 2", 0x260D8, 1507, 0x12346, 0xD2C9, 0x14733, 0x13A31, 0x15577,
                new HashInfo(0, 0x80000, "5A84689EAD34C09EB2054A0309A5B088B2F36D07FE62B31FCC192095D0BDCAC8", new []
                {
                    new HashSkip(0xD2C9 + 0x1D, 48),
                    new HashSkip(0x12346, 16),
                    new HashSkip(0x13A31, 48),
                    new HashSkip(0x14733, 1),
                    new HashSkip(0x15577, 1),
                    new HashSkip(0x260D8, 1507)
                })
            ),
            new Firmware("KeDei v1.1, panel type 4 (LGH-3509), FW variant 1", 0x260D8, 1507, 0x12346, 0xD22D, 0x14733, 0x13A31, 0x15577,
                new HashInfo(0, 0x80000, "3DBD9A130F20124FA851108EF67DB6E0FA38D08AB37828B8ED272966F6B6CA4D", new []
                {
                    new HashSkip(0xD22D + 0x1D, 48),
                    new HashSkip(0x12346, 16),
                    new HashSkip(0x13A31, 48),
                    new HashSkip(0x14733, 1),
                    new HashSkip(0x15577, 1),
                    new HashSkip(0x260D8, 1507)
                })
            ),
            new Firmware("KeDei v1.1, panel type 4 (LGH-3509), FW variant 2", 0x260D8, 1507, 0x12346, 0xD1B2, 0x14733, 0x13A31, 0x15577,
                new HashInfo(0, 0x80000, "0F6B1CEB8B3034D6006076CEC395A0114FD5AEF1DF21C9DC3668F5B031E2CB55", new []
                {
                    new HashSkip(0xD1B2 + 0x1D, 48),
                    new HashSkip(0x12346, 16),
                    new HashSkip(0x13A31, 48),
                    new HashSkip(0x14733, 1),
                    new HashSkip(0x15577, 1),
                    new HashSkip(0x260D8, 1507)
                })
            ),
            new Firmware("KeDei v1.1, panel type 4 (LGH-3509), FW variant 3", 0x260D8, 1507, 0x12346, 0xD258, 0x14733, 0x13A31, 0x15577,
                new HashInfo(0, 0x80000, "4BDF3565609A5DC28030FDB490B0DE5A336B159B4BF434CB859D7B2C06FDC4FD", new []
                {
                    new HashSkip(0xD258 + 0x1D, 48),
                    new HashSkip(0x12346, 16),
                    new HashSkip(0x13A31, 48),
                    new HashSkip(0x14733, 1),
                    new HashSkip(0x15577, 1),
                    new HashSkip(0x260D8, 1507)
                })
            ),
            new Firmware("KeDei v1.1, panel type 5 (SKY035S7D-11039, 800x480), FW variant 1", 0x260D8, 1507, 0x12346, 0xD46E, 0x14733, 0x13A31, 0x15577,
                new HashInfo(0, 0x80000, "E4FA1EA5A1D1AD2646883E654C8919E4497946C2688D410969BDB87342B46C96", new []
                {
                    new HashSkip(0xD46E + 0x1D, 48),
                    new HashSkip(0x12346, 16),
                    new HashSkip(0x13A31, 48),
                    new HashSkip(0x14733, 1),
                    new HashSkip(0x15577, 1),
                    new HashSkip(0x260D8, 1507)
                })
            ),
            new Firmware("KeDei KD0350AV02, panel type unknown ('unknown 1'), FW variant 1", 0x260D8, 1507, 0x12346, 0xD2F3, 0x14733, 0x13A31, 0x15577,
                new HashInfo(0, 0x80000, "CD5A18729A0BF57478981AEC00D8AAB00C328AB8E36E7E8EAA502D8CA51B45C9", new []
                {
                    new HashSkip(0xD2F3 + 0x1D, 48),
                    new HashSkip(0x12346, 16),
                    new HashSkip(0x13A31, 48),
                    new HashSkip(0x14733, 1),
                    new HashSkip(0x15577, 1),
                    new HashSkip(0x260D8, 1507)
                })
            ),
            new Firmware("KeDei KD0350AV02, panel type unknown ('unknown 1'), FW variant 2", 0x260D8, 1507, 0x12346, 0xD44C, 0x14733, 0x13A31, 0x15577,
                new HashInfo(0, 0x80000, "B30F3589118335BB54D07E666EFB4DB37DEF4D5C3947B43A8B5A120D655EA9B9", new []
                {
                    new HashSkip(0xD237 + 0x1D, 48),
                    new HashSkip(0x12346, 16),
                    new HashSkip(0x13A31, 48),
                    new HashSkip(0x14733, 1),
                    new HashSkip(0x15577, 1),
                    new HashSkip(0x260D8, 1507)
                })
            ),
            new Firmware("KeDei KD0350AV02, panel type unknown ('unknown 1'), FW variant 3", 0x260D8, 1507, 0x12346, 0xD55A, 0x14733, 0x13A31, 0x15577,
                new HashInfo(0, 0x80000, "D366BB22A38EEA6B495918DECCA96C73DEF691FD84927023DB20462D7E69534F", new []
                {
                    new HashSkip(0xD55A + 0x1D, 48),
                    new HashSkip(0x12346, 16),
                    new HashSkip(0x13A31, 48),
                    new HashSkip(0x14733, 1),
                    new HashSkip(0x15577, 1),
                    new HashSkip(0x260D8, 1507)
                })
            ),
            new Firmware("KeDei v1.1, panel type unknown ('unknown 1'), FW variant unknown", 0x260D8, 1507, 0x12346, 0xD2F6, 0x14733, 0x13A31, 0x15577,
                new HashInfo(0, 0x80000, "3E2DD7ECE098A69EB18C011DECCD162B6D55B4EDADFC965EAF5E2DD690414F7E", new []
                {
                    new HashSkip(0xD2F6 + 0x1D, 48),
                    new HashSkip(0x12346, 16),
                    new HashSkip(0x13A31, 48),
                    new HashSkip(0x14733, 1),
                    new HashSkip(0x15577, 1),
                    new HashSkip(0x260D8, 1507)
                })
            ),
            new Firmware("KeDei version unknown, panel type unknown ('unknown 2'), FW variant unknown", 0x260D8, 1507, 0x12346, 0xD2A3, 0x14733, 0x13A31, 0x15577,
                new HashInfo(0, 0x80000, "AB0EE7234C0D1B949955E13A19577D443731959C8FC842A6EF4A578D9DE2F456", new []
                {
                    new HashSkip(0xD2A3 + 0x1D, 48),
                    new HashSkip(0x12346, 16),
                    new HashSkip(0x13A31, 48),
                    new HashSkip(0x14733, 1),
                    new HashSkip(0x15577, 1),
                    new HashSkip(0x260D8, 1507)
                })
            ),
            new Firmware("7\" KeDei KD070V02 1024x600 FW variant 1", 0x260D8, 1507, 0x12346, 0xD04F, 0x14733, 0x13A31, 0x15577,
                new HashInfo(0, 0x80000, "6EEF496354199EA4147F6EF5589194D7FD547CC43BC33E3567A0D2411A84C5AC", new []
                {
                    new HashSkip(0xD04F + 0x1D, 48),
                    new HashSkip(0x12346, 16),
                    new HashSkip(0x13A31, 48),
                    new HashSkip(0x14733, 1),
                    new HashSkip(0x15577, 1),
                    new HashSkip(0x260D8, 1507)
                })
            ),
            new Firmware("7\" KeDei KD070V02 1024x600 FW variant 2", 0x260D8, 1507, 0x12346, 0xD27E, 0x1473B, 0x13A31, 0x1557F,
                new HashInfo(0, 0x80000, "8B070F377F700ABA6D59D265DBC19CE2DF8B56DA384EF884325BD29A5EDE88A0", new []
                {
                    new HashSkip(0xD27E + 0x1D, 48),
                    new HashSkip(0x12346, 16),
                    new HashSkip(0x13A31, 48),
                    new HashSkip(0x1473B, 1),
                    new HashSkip(0x1557F, 1),
                    new HashSkip(0x260D8, 1507)
                })
            ),
            new Firmware("RTD2660 board, panel type 1, FW variant 1", 0x260AE, 1075, 0x1241B, 0xD546, 0x14537, 0x13B01, 0x1554B,
                new HashInfo(0, 0x80000, "7F649ED082611385FC7E4767009217E91DB23821BBA2031858A2CCC659A14F8A", new []
                {
                    new HashSkip(0xD546 + 0x1D, 48),
                    new HashSkip(0x1241B, 16),
                    new HashSkip(0x13B01, 48),
                    new HashSkip(0x14537, 1),
                    new HashSkip(0x1554B, 1),
                    new HashSkip(0x260AE, 1075)
                })
            ),
            new Firmware("7\" MPI7002, FW variant 1", 0x261A2, 2271, 0x1235A, 0xD150, 0x14455, 0x13A59, 0x150F2,
                new HashInfo(0, 0x80000, "C5EC6796D1BE968B6D8C4514567179360D0723E60D83FED92ED5C89BC6E6CDAB", new []
                {
                    new HashSkip(0xD150 + 0x1D, 48),
                    new HashSkip(0x1235A, 16),
                    new HashSkip(0x13A59, 48),
                    new HashSkip(0x14455, 1),
                    new HashSkip(0x150F2, 1),
                    new HashSkip(0x261A2, 2271)
                })
            ),
            new Firmware("Elecrow RC050, FW variant 1", 0x261A2, 2271, 0x1235A, 0xD09F, 0x14073, 0x13A59, 0x14D4E,
                new HashInfo(0, 0x80000, "22FAE2A9E7376EE4F5CD8C8352282B5377E6DEC2FCA83BAFA9A5A1C474A2D9EB", new []
                {
                    new HashSkip(0xD09F + 0x1D, 48),
                    new HashSkip(0x1235A, 16),
                    new HashSkip(0x13A59, 48),
                    new HashSkip(0x14073, 1),
                    new HashSkip(0x14D4E, 1),
                    new HashSkip(0x261A2, 2271)
                })
            ),
            new Firmware("P08-RT2660TP-MAIN-V1.0, FW variant 1", 0x26280, 56, 0x125D6, 0xE1F7, 0x14F45, 0x13D20, 0x15CC8,
                new HashInfo(0, 0x80000, "A6BD01F16EE30D21FDEF844217337063AE5BD42ED563FC8327889F8E28CC2A7B", new []
                {
                    new HashSkip(0xE1F7 + 0x1D, 48),
                    new HashSkip(0x125D6, 16),
                    new HashSkip(0x13D20, 48),
                    new HashSkip(0x14F45, 1),
                    new HashSkip(0x15CC8, 1),
                    new HashSkip(0x26280, 56)
                })
            )
        };

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
