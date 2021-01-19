namespace MySpector.Objects
{
    public class NotifierParam
    {
        public NotifierType Type { get; }
        public string Arg { get; }

        public NotifierParam(NotifierType type, string arg)
        {
            Type = type;
            Arg = arg;
        }
    }
}