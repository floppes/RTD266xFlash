namespace RTD266xFlash
{
    public class Firmware
    {
        public string Name
        {
            get;
        }

        public int LogoOffset
        {
            get;
        }

        public int HdmiStringOffset
        {
            get;
        }

        public int MaxLogoLength
        {
            get;
        }

        public HashInfo[] Hashes
        {
            get;
        }

        public Firmware(string name, int logoOffset, int hdmiStringOffset, int maxLogoLength, HashInfo[] hashes)
        {
            Name = name;
            LogoOffset = logoOffset;
            HdmiStringOffset = hdmiStringOffset;
            MaxLogoLength = maxLogoLength;
            Hashes = hashes;
        }
    }
}
