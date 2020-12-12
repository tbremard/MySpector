namespace MySpector.Core
{
    public class WatchItem
    {
        public string Name;
        public string Url;
        public XtraxRule XtraxChain;
        public CheckerParam CheckerParam { get; set; }
    }
}
