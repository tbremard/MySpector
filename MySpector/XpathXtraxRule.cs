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

        protected override IDataTruck GetOutput(IDataTruck data)
        {
            var htmldoc = new HtmlDocument();
            htmldoc.LoadHtml(data.GetText());
            var node = htmldoc.DocumentNode.SelectSingleNode(_xPath);
            IDataTruck ret;
            if (node == null)
            {
                ret = DataTruck.CreateText(NOT_FOUND);
            }
            else
            {
                ret = DataTruck.CreateText(node.InnerText.Trim());
            }
            return ret;
        }
    }
}