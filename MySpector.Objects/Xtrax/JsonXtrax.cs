using System;
using NLog;
using MySpector.Objects.Args;
using System.Text.Json;

namespace MySpector.Objects
{
    public class JsonXtrax : Xtrax
    {
        public override XtraxType Type => XtraxType.JsonXpath;
        public override string JsonArg { get { return Jsoner.ToJson(_arg); } }

        static Logger _log = LogManager.GetCurrentClassLogger();
        private string NOT_FOUND = "NOT_FOUND";
        private readonly JsonArg _arg;

        public JsonXtrax(JsonArg arg)
        {
            _arg = arg;
        }

        protected override IDataTruck GetOutput(IDataTruck data)
        {
            IDataTruck ret;
            try
            {
                _log.Trace($"Extracting XPath[{_arg.Path}] from: [{data.PreviewText}]");
                var doc = JsonDocument.Parse(data.GetText());
                string[] tokens = _arg.Path.Split('\\');
                JsonElement node = doc.RootElement;
                for (int i = 0; i < tokens.Length; i++)
                {
                    _log.Trace($"GetProperty('{tokens[i]}')");
                    if (string.IsNullOrEmpty(tokens[i]))
                        continue;
                    node = node.GetProperty(tokens[i]);
                }
                char[] trimChars = { ' ', '\"' };
                ret = DataTruck.CreateText(node.GetRawText().Trim(trimChars));
                _log.Trace($"Succesfully extracted: [{ret.PreviewText}]");
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
            string ret = GetType().Name + " " + _arg.Path;
            return ret;
        }
    }
}