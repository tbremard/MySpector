using HtmlAgilityPack;

namespace MySpector
{
    public class XpathTroxRule : ITroxRule
    {
        private readonly string _xPath;
        private string NOT_FOUND="NOT_FOUND";

        public XpathTroxRule(string xPath)
        {
            _xPath = xPath;
        }

        public string GetOutput(IRump rump)
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