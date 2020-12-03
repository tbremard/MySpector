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

        protected override IInputData GetOutput(IInputData data)
        {
            var htmldoc = new HtmlDocument();
            htmldoc.LoadHtml(data.GetText());
            var node = htmldoc.DocumentNode.SelectSingleNode(_xPath);
            IInputData ret;
            if (node == null)
            {
                ret = InputData.CreateText(NOT_FOUND);
            }
            else
            {
                ret = InputData.CreateText(node.InnerText.Trim());
            }
            return ret;
        }
    }
}