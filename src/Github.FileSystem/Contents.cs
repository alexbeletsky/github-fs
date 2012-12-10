namespace Github
{
    public class Contents
    {
        public string Type { get; set; }

        public string Encoding { get; set; }

        public int Size { get; set; }

        public string Path { get; set; }

        public byte[] Content { get; set; }

        public string FileContents { get { return System.Text.Encoding.ASCII.GetString(Content); } }
    }
}