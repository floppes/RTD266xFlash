namespace RTD266xFlash
{
    public class Font
    {
        public string Name
        {
            get;
        }

        public int Width
        {
            get;
        }

        public int Height
        {
            get;
        }

        public byte[] Data
        {
            get;
        }

        public Font(string name, int width, int height, byte[] data)
        {
            Name = name;
            Width = width;
            Height = height;
            Data = data;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}
