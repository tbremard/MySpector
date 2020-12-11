namespace MySpector.Core
{
    public class WatchItem
    {
        public string Name;
        public string Url;
        public string Xpath;

        public CheckerParam Checker { get; set; }
    }
}
