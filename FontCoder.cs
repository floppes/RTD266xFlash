using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;

namespace RTD266xFlash
{
    public class FontCoder
    {
        /// <summary>
        /// Font width of KeDei logo
        /// </summary>
        public const int FontWidthKedei = 17;

        /// <summary>
        /// Font height of KeDei logo
        /// </summary>
        public const int FontHeightKedei = 4;

        /// <summary>
        /// Individual character width in pixels
        /// </summary>
        private const int CharWidth = 12;

        /// <summary>
        /// Individual character height in pixels
        /// </summary>
        private const int CharHeight = 18;

        private readonly int _fontWidth;

        private readonly int _fontHeight;

        /// <summary>
        /// Logo width in pixels
        /// </summary>
        private int Width
        {
            get
            {
                return _fontWidth * CharWidth;
            }
        }

        /// <summary>
        /// Logo height in pixels
        /// </summary>
        private int Height
        {
            get
            {
                return _fontHeight * CharHeight;
            }
        }

        private Image _image;

        public FontCoder(int fontWidth, int fontHeight)
        {
            _fontWidth = fontWidth;
            _fontHeight = fontHeight;
        }

        public bool LoadImage(string filename)
        {
            Image img;

            try
            {
                img = Image.FromFile(filename);
            }
            catch (Exception)
            {
                return false;
            }

            if (img.Width != Width || img.Height != Height)
            {
                return false;
            }

            _image = img;

            return true;
        }

        private byte[] VariableLengthEncode(byte[] data)
        {
            Dictionary<byte, int> occurances = new Dictionary<byte, int>();

            for (byte i = 0; i < 16; i++)
            {
                occurances.Add(i, 0);
            }

            // count number of nibble occurances
            foreach (byte dataByte in data)
            {
                occurances[(byte)((dataByte >> 4) & 0x0F)]++;
                occurances[(byte)((dataByte >> 0) & 0x0F)]++;
            }

            // sort by occurance
            List<KeyValuePair<byte, int>> occurancesSorted = occurances.ToList();

            occurancesSorted.Sort(delegate (KeyValuePair<byte, int> pair1, KeyValuePair<byte, int> pair2)
            {
                return -pair1.Value.CompareTo(pair2.Value);
            });

            // create code table
            Dictionary<byte, string> runLengthCodes = new Dictionary<byte, string>
            {
                {occurancesSorted[0].Key, "0"},
                {occurancesSorted[1].Key, "100"},
                {occurancesSorted[2].Key, "1010"},
                {occurancesSorted[3].Key, "1011"},
                {occurancesSorted[4].Key, "1100"},
                {occurancesSorted[5].Key, "11010"},
                {occurancesSorted[6].Key, "11011"},
                {occurancesSorted[7].Key, "11100"},
                {occurancesSorted[8].Key, "111010"},
                {occurancesSorted[9].Key, "111011"},
                {occurancesSorted[10].Key, "111100"},
                {occurancesSorted[11].Key, "111101"},
                {occurancesSorted[12].Key, "1111100"},
                {occurancesSorted[13].Key, "1111101"},
                {occurancesSorted[14].Key, "1111110"},
                {occurancesSorted[15].Key, "11111110"}
            };


            List<byte> vlc = new List<byte>();

            // add code table
            for (int i = 0; i < occurancesSorted.Count; i += 2)
            {
                vlc.Add((byte)((occurancesSorted[i].Key << 4) | occurancesSorted[i + 1].Key));
            }

            // add dummy length
            vlc.Add(0);
            vlc.Add(0);

            // encode to binary string
            StringBuilder binary = new StringBuilder();

            foreach (byte dataByte in data)
            {
                binary.Append(runLengthCodes[(byte)((dataByte >> 4) & 0x0F)]);
                binary.Append(runLengthCodes[(byte)((dataByte >> 0) & 0x0F)]);
            }

            while (binary.Length % 8 != 0)
            {
                binary.Append("0");
            }

            // convert to bytes
            string binaryString = binary.ToString();

            for (int i = 0; i < binaryString.Length; i += 8)
            {
                char[] byteChars = binaryString.Substring(i, 8).ToCharArray();
                Array.Reverse(byteChars);
                string byteString = new string(byteChars);

                vlc.Add(Convert.ToByte(byteString, 2));
            }

            // set length
            vlc[8] = (byte)(((vlc.Count - 10) >> 8) & 0xFF);
            vlc[9] = (byte)(((vlc.Count - 10) >> 0) & 0xFF);

            return vlc.ToArray();
        }

        public byte[] Encode()
        {
            if (_image == null)
            {
                return null;
            }

            Bitmap bmp = new Bitmap(_image);

            List<byte> bytes = new List<byte>();
            int step = 0;
            int bit = 7;
            byte[] pixel = new byte[3];

            for (int fontY = 0; fontY < _fontHeight; fontY++)
            {
                for (int fontX = 0; fontX < _fontWidth; fontX++)
                {
                    for (int charY = 0; charY < CharHeight; charY++)
                    {
                        for (int charX = 0; charX < CharWidth; charX++)
                        {
                            byte pixelValue;

                            Color pixelColor = bmp.GetPixel(fontX * CharWidth + charX, fontY * CharHeight + charY);

                            if (pixelColor.ToArgb() == Color.White.ToArgb())
                            {
                                pixelValue = 0;
                            }
                            else if (pixelColor.ToArgb() == Color.Black.ToArgb())
                            {
                                pixelValue = 1;
                            }
                            else
                            {
                                return null;
                            }

                            pixel[step] |= (byte)(pixelValue << bit);
                            bit--;

                            if (bit < 0)
                            {
                                bit = 7;
                                step++;

                                if (step == 3)
                                {
                                    step = 0;

                                    bytes.Add(pixel[2]);
                                    bytes.Add(pixel[1]);
                                    bytes.Add(pixel[0]);

                                    pixel[0] = 0;
                                    pixel[1] = 0;
                                    pixel[2] = 0;
                                }
                            }
                        }
                    }
                }
            }

            return VariableLengthEncode(bytes.ToArray());
        }

        public static bool CheckFile(string fileName, int fontWidth, int fontHeight, out string error)
        {
            error = "";

            Image img;

            try
            {
                img = Image.FromFile(fileName);
            }
            catch (Exception)
            {
                error = $"Cannot load file \"{fileName}\"!";
                return false;
            }

            if (img.Width != fontWidth * CharWidth || img.Height != fontHeight * CharHeight)
            {
                error = $"Wrong image dimensions! Width must be {fontWidth * CharWidth} pixels, height must be {fontHeight * CharHeight} pixels.";
                img.Dispose();
                return false;
            }

            Bitmap bmp = new Bitmap(img);

            for (int y = 0; y < bmp.Height; y++)
            {
                for (int x = 0; x < bmp.Width; x++)
                {
                    int color = bmp.GetPixel(x, y).ToArgb();

                    if (color != Color.Black.ToArgb() && color != Color.White.ToArgb())
                    {
                        error = $"Only black (#000000) and white (#FFFFFF) pixels without transparency are allowed! Pixel at x = {x}, y = {y} is #{color:X8}.";
                        bmp.Dispose();
                        img.Dispose();
                        return false;
                    }
                }
            }

            bmp.Dispose();
            img.Dispose();

            return true;
        }

        private static void SetChrPixel(Bitmap bmp, ref Point pos, ref Point offset, byte data, int charWidth, int charHeight)
        {
            for (int i = 7; i >= 0; i--)
            {
                if ((data & (0x1 << i)) != 0)
                {
                    bmp.SetPixel(offset.X + pos.X, offset.Y + pos.Y, Color.Black);
                }
                else
                {
                    bmp.SetPixel(offset.X + pos.X, offset.Y + pos.Y, Color.White);
                }

                pos.X++;

                if (pos.X == charWidth)
                {
                    pos.X = 0;
                    pos.Y++;

                    if (pos.Y == charHeight)
                    {
                        pos.Y = 0;
                        offset.X += charWidth;

                        if (offset.X == bmp.Width)
                        {
                            offset.X = 0;
                            offset.Y += charHeight;
                        }
                    }
                }
            }
        }

        public static int DecodeLength(byte[] sourceData)
        {
            int length = (sourceData[8] << 8) | sourceData[9];

            return length + 10;
        }

        public static Image Decode(byte[] sourceData, int fontWidth, int fontHeight)
        {
            Dictionary<string, byte> runLengthCodes = new Dictionary<string, byte>();

            runLengthCodes.Add("0", (byte)(sourceData[0] >> 4));
            runLengthCodes.Add("100", (byte)(sourceData[0] & 0xF));
            runLengthCodes.Add("1010", (byte)(sourceData[1] >> 4));
            runLengthCodes.Add("1011", (byte)(sourceData[1] & 0xF));
            runLengthCodes.Add("1100", (byte)(sourceData[2] >> 4));
            runLengthCodes.Add("11010", (byte)(sourceData[2] & 0xF));
            runLengthCodes.Add("11011", (byte)(sourceData[3] >> 4));
            runLengthCodes.Add("11100", (byte)(sourceData[3] & 0xF));
            runLengthCodes.Add("111010", (byte)(sourceData[4] >> 4));
            runLengthCodes.Add("111011", (byte)(sourceData[4] & 0xF));
            runLengthCodes.Add("111100", (byte)(sourceData[5] >> 4));
            runLengthCodes.Add("111101", (byte)(sourceData[5] & 0xF));
            runLengthCodes.Add("1111100", (byte)(sourceData[6] >> 4));
            runLengthCodes.Add("1111101", (byte)(sourceData[6] & 0xF));
            runLengthCodes.Add("1111110", (byte)(sourceData[7] >> 4));
            runLengthCodes.Add("11111110", (byte)(sourceData[7] & 0xF));

            string code = "";
            int length = (sourceData[8] << 8) | sourceData[9];
            bool endReached = false;
            List<byte> decoded = new List<byte>();
            StringBuilder decodedBytes = new StringBuilder();
            bool byteComplete = false;
            byte nextNibble = 0;

            for (int j = 10; j < sourceData.Length; j++)
            {
                for (int i = 0; i < 8; i++)
                {
                    byte previousNibble;

                    if ((sourceData[j] & (0x01 << i)) != 0)
                    {
                        code = code + "1";
                    }
                    else
                    {
                        code = code + "0";
                    }

                    if (runLengthCodes.ContainsKey(code))
                    {
                        previousNibble = nextNibble;

                        nextNibble = runLengthCodes[code];

                        if (byteComplete)
                        {
                            decoded.Add((byte)((previousNibble << 4) | nextNibble));
                            byteComplete = false;
                        }
                        else
                        {
                            byteComplete = true;
                        }

                        decodedBytes.Append($"{nextNibble:X1}");

                        code = "";
                    }

                    if (code.Equals("11111111"))
                    {
                        // end
                        endReached = true;
                        Debug.WriteLine("end reached");
                        break;
                    }

                    if (code.Length > 8)
                    {
                        Debug.WriteLine("code is too long!");
                        return null;
                    }
                }

                if (endReached)
                {
                    break;
                }
            }

            Bitmap bmp = new Bitmap(fontWidth * CharWidth, fontHeight * CharHeight);

            Point offset = new Point(0, 0);
            Point chrPos = new Point(0, 0);

            for (int i = 0; i < decoded.Count - 3; i += 3)
            {
                SetChrPixel(bmp, ref chrPos, ref offset, decoded[i + 2], CharWidth, CharHeight);
                SetChrPixel(bmp, ref chrPos, ref offset, decoded[i + 1], CharWidth, CharHeight);
                SetChrPixel(bmp, ref chrPos, ref offset, decoded[i + 0], CharWidth, CharHeight);
            }

            return bmp;
        }
    }
}
