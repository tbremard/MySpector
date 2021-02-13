using HtmlAgilityPack;
using System;
using NLog;
using System.Text.Json;

namespace MySpector.Objects
{
    public class XpathXtrax : Xtrax
    {
        public override XtraxType Type => XtraxType.Xpath;
        static Logger _log = LogManager.GetCurrentClassLogger();
        private readonly string _xPath;
        private string NOT_FOUND = "NOT_FOUND";

        public XpathXtrax(string jsonArg)
        {
            JsonArg = jsonArg;
            var xpathArg = JsonSerializer.Deserialize<XpathArg>(jsonArg);
            _xPath = xpathArg.Path;
        }

        protected override IDataTruck GetOutput(IDataTruck data)
        {
            IDataTruck ret;
            try
            {
                var htmldoc = new HtmlDocument();
                _log.Trace($"Extracting XPath from: [{data.PreviewText}]");
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
                    _log.Trace($"Succesfully extracted: [{ret.PreviewText}]");
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                ret = DataTruck.CreateText(NOT_FOUND);
            }
            return ret;
        }

        public override string ToString()
        {
            string ret = GetType().Name+ " " + _xPath;
            return ret;
        }
    }
}