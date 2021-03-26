using HtmlAgilityPack;
using System;
using NLog;
using MySpector.Objects.Args;

namespace MySpector.Objects
{

    public class HtmlXpathXtrax : Xtrax
    {
        public override XtraxType Type => XtraxType.HtmlXpath;
        public override string JsonArg { get { return Jsoner.ToJson(_arg); } }

        static Logger _log = LogManager.GetCurrentClassLogger();
        private string NOT_FOUND = "NOT_FOUND";
        private readonly XpathArg _arg;

        public HtmlXpathXtrax(XpathArg arg)
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
                    string message = $"Node not found '{_arg.Path}'";
                    _log.Error(message);
                    ErrorMessage.AppendLine(message);
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
                ErrorMessage.AppendLine(ex.Message);
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