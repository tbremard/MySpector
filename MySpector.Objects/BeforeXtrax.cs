using NLog;

namespace MySpector.Objects
{
    public class BeforeXtrax : Xtrax
    {
        public override XtraxType Type => XtraxType.Before;
        public override string JsonArg { get { return Jsoner.ToJson(_arg); } }

        static Logger _log = LogManager.GetCurrentClassLogger();
        private readonly BeforeArg _arg;

        public BeforeXtrax(BeforeArg arg)
        {
            this._arg = arg;
        }

        protected override IDataTruck GetOutput(IDataTruck data)
        {
            IDataTruck ret;
            string content = data.GetText();
            if (!content.Contains(_arg.Suffix))
            {
                _log.Error($"data do not contain '{_arg.Suffix}'");
                ret = DataTruck.CreateText(XtraxConst.NOT_FOUND);
            }
            else
            {
                int index = content.IndexOf(_arg.Suffix);
                string contentExtracted = content.Substring(0, index);
                ret = DataTruck.CreateText(contentExtracted.Trim());
            }
            return ret;
        }
    }

}