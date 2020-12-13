using NLog;

namespace MySpector
{
    public class BeforeXtraxRule: XtraxRule
    {
        static Logger _log = LogManager.GetCurrentClassLogger();
        private readonly string _suffix;

        public BeforeXtraxRule(string suffix)
        {
            _suffix = suffix;
        }

        protected override IDataTruck GetOutput(IDataTruck data)
        {
            IDataTruck ret;
            string content = data.GetText();
            if (!content.Contains(_suffix))
            {
                _log.Error($"data do not contain '{_suffix}'");
                ret = DataTruck.CreateText(XtraxRuleConst.NOT_FOUND);
            }
            else
            {
                int index = content.IndexOf(_suffix);
                string contentExtracted = content.Substring(0, index);
                ret = DataTruck.CreateText(contentExtracted.Trim());
            }
            return ret;
        }
    }
}