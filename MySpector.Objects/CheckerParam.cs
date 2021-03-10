namespace MySpector.Objects
{
    public class CheckerParam
    {
        public CheckerType Type { get; }
        public string Arg { get; }
        public int? DbId { get; }

        public CheckerParam(CheckerType type, string arg)
        {
            Type = type;
            Arg = arg;
        }

        public CheckerParam(CheckerType type, string arg, int dbId)
        {
            Type = type;
            Arg = arg;
            DbId = dbId;
        }
    }
}