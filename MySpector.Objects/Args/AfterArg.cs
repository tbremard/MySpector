namespace MySpector.Objects.Args
{
    public class AfterArg
    {
        public string Prefix { get; set; }
    }

    public class BeforeArg
    {
        public string Suffix { get; set; }
    }

    public class XpathArg
    {
        public string Path { get; set; }

    }
    public class BetweenArg
    {
        public string Prefix { get; set; }
        public string Suffix { get; set; }
    }

    public class TextReplaceArg
    {
        public string OldToken { get; set; }
        public string NewToken { get; set; }
    }


}
