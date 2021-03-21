namespace MySpector.Objects
{
    public class FileTarget : IGrabTarget
    {
        public string Path { get; private set; }
        public FileTarget(string path)
        {
            Path = path;
        }

        public TargetType TargetType => TargetType.FILE;
    }
    
    public class ProcessTarget : IGrabTarget
    {
        public TargetType TargetType => TargetType.PROCESS;

        public string FileName { get; set; }
        public string Arguments { get; set; }
        public int? WaitForExitMs { get; set; }
        public string StandardInput { get; set; }
    }
}