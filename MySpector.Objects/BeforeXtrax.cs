using NLog;

namespace MySpector.Objects
{
    public class BeforeXtrax : Xtrax
    {
        public override XtraxType Type => XtraxType.Before;

        static Logger _log = LogManager.GetCurrentClassLogger();
        private readonly string _suffix;

        public BeforeXtrax(string suffix)
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
                ret = DataTruck.CreateText(XtraxConst.NOT_FOUND);
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