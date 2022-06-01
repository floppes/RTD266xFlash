namespace RTD266xFlash
{
    public class FirmwarePattern
    {
        /// <summary>
        /// Pattern name
        /// </summary>
        public string Name
        {
            get;
        }

        /// <summary>
        /// Pattern
        /// </summary>
        public byte[] Pattern
        {
            get;
        }

        /// <summary>
        /// Mask
        /// </summary>
        public byte[] Mask
        {
            get;
        }

        /// <summary>
        /// Offset to pattern location
        /// </summary>
        public int Offset
        {
            get;
        }

        public FirmwarePattern(string name, byte[] pattern, byte[] mask, int offset)
        {
            Name = name;
            Pattern = pattern;
            Mask = mask;
            Offset = offset;
        }
    }
}
