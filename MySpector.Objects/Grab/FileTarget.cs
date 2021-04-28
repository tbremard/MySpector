namespace MySpector.Objects
{
    public class FileTarget : IGrabTarget
    {
        public TargetType TargetType => TargetType.FILE;
        public string Name { get; set; }
        public int TimeoutMs { get; set; }
        public string Path { get; private set; }

        public FileTarget(string path)
        {
            Path = path;
        }
    }
}