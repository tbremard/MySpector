using NLog;

namespace MySpector.Objects
{
    public class AfterXtrax : Xtrax
    {
        static Logger _log = LogManager.GetCurrentClassLogger();
        private readonly string _prefix;

        public AfterXtrax(string prefix)
        {
            _prefix = prefix;
        }

        protected override IDataTruck GetOutput(IDataTruck data)
        {
            IDataTruck ret;
            string content = data.GetText();
            if (!content.Contains(_prefix))
            {
                _log.Error($"data do not contain '{_prefix}'");
                ret = DataTruck.CreateText(XtraxConst.NOT_FOUND);
            }
            else
            {
                int index = content.IndexOf(_prefix);
                int indexAfter = index + _prefix.Length;
                string contentAfter = content.Substring(indexAfter);
                ret = DataTruck.CreateText(contentAfter.Trim());
            }
            return ret;
        }
    }
}