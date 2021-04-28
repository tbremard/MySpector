namespace MySpector.Objects
{
    public interface IGrabTarget
    {
        public TargetType TargetType { get; }
        public string Name { get; }
        public int TimeoutMs { get; }
    }
}
