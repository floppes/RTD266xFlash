using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace RTD266xFlash
{
    public class HashInfo
    {
        /// <summary>
        /// Start address
        /// </summary>
        public int Start
        {
            get;
        }

        /// <summary>
        /// Length
        /// </summary>
        public int Length
        {
            get;
        }

        /// <summary>
        /// SHA256 hash
        /// </summary>
        public string Hash
        {
            get;
        }

        /// <summary>
        /// Sections to skip for hash calculation
        /// </summary>
        public HashSkip[] SkippedSections
        {
            get;
        }

        public HashInfo(int start, int length, string hash, HashSkip[] skippedSections)
        {
            Start = start;
            Length = length;
            Hash = hash;
            SkippedSections = skippedSections;
        }

        /// <summary>
        /// Returns a hash of the specified firmware by considering all hash parameters like start, length and skipped sections
        /// </summary>
        /// <param name="firmware">Firmware</param>
        /// <returns>SHA256 hash or null</returns>
        public string GetHash(byte[] firmware)
        {
            if (firmware.Length < Start)
            {
                return null;
            }

            if (firmware.Length < Start + Length)
            {
                return null;
            }

            List<byte> comparedBytes = new List<byte>();

            for (int i = Start; i < Start + Length; i++)
            {
                bool skip = false;

                foreach (HashSkip hashSkip in SkippedSections)
                {
                    if (i >= Start + hashSkip.Offset && i < Start + hashSkip.Offset + hashSkip.Length)
                    {
                        skip = true;
                    }
                }

                if (!skip)
                {
                    comparedBytes.Add(firmware[i]);
                }
            }

            return Sha256Hash(comparedBytes.ToArray(), 0, comparedBytes.Count);
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
