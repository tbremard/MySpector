using HtmlAgilityPack;
using System;
using NLog;

namespace MySpector.Objects
{
    public class XpathXtrax : Xtrax
    {
        public override XtraxType Type => XtraxType.Xpath;
        static Logger _log = LogManager.GetCurrentClassLogger();
        private string NOT_FOUND = "NOT_FOUND";
        private readonly XpathArg _arg;

        public XpathXtrax(XpathArg arg)
        {
            _arg = arg;
        }

        protected override IDataTruck GetOutput(IDataTruck data)
        {
            IDataTruck ret;
            try
            {
                var htmldoc = new HtmlDocument();
                _log.Trace($"Extracting XPath from: [{data.PreviewText}]");
                htmldoc.LoadHtml(data.GetText());
                var node = htmldoc.DocumentNode.SelectSingleNode(_arg.Path);
                if (node == null)
                {
                    _log.Error($"Node not found '{_arg.Path}'");
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
            string ret = GetType().Name+ " " + _arg.Path;
            return ret;
        }
    }
}