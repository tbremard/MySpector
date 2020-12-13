using HtmlAgilityPack;
using System;
using NLog;

namespace MySpector
{
    public class XpathXtraxRule : XtraxRule
    {
        static Logger _log = LogManager.GetCurrentClassLogger();
        private readonly string _xPath;
        private string NOT_FOUND="NOT_FOUND";

        public XpathXtraxRule(string xPath)
        {
            _xPath = xPath;
        }

        protected override IDataTruck GetOutput(IDataTruck data)
        {
            IDataTruck ret;
            try
            {
                var htmldoc = new HtmlDocument();
                htmldoc.LoadHtml(data.GetText());
                var node = htmldoc.DocumentNode.SelectSingleNode(_xPath);
                if (node == null)
                {
                    _log.Error($"Node not found '{_xPath}'");
                    ret = DataTruck.CreateText(NOT_FOUND);
                }
                else
                {
                    ret = DataTruck.CreateText(node.InnerText.Trim());
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                ret = DataTruck.CreateText(NOT_FOUND);
            }
            return ret;
        }
    }
}