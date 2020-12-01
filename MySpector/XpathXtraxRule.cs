using HtmlAgilityPack;

namespace MySpector
{
    public class XpathXtraxRule : XtraxRule
    {
        private readonly string _xPath;
        private string NOT_FOUND="NOT_FOUND";

        public XpathXtraxRule(string xPath)
        {
            _xPath = xPath;
        }

        protected override string GetOutput(IRump rump)
        {
            var htmldoc = new HtmlDocument();
            htmldoc.LoadHtml(rump.Content);
            var node = htmldoc.DocumentNode.SelectSingleNode(_xPath);
            string ret;
            if (node == null)
            {
                ret = NOT_FOUND;
            }
            else
            {
                ret = node.InnerText.Trim();
            }
            return ret;
        }
    }
}