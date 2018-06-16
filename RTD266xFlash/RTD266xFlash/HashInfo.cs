namespace RTD266xFlash
{
    public class HashInfo
    {
        public int Start
        {
            get;
        }

        public int Length
        {
            get;
        }

        public string Hash
        {
            get;
        }

        public HashInfo(int start, int length, string hash)
        {
            Start = start;
            Length = length;
            Hash = hash;
        }
    }
}
