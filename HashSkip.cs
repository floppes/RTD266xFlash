namespace RTD266xFlash
{
    public class HashSkip
    {
        /// <summary>
        /// Offset
        /// </summary>
        public int Offset
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

        public HashSkip(int offset, int length)
        {
            Offset = offset;
            Length = length;
        }
    }
}
