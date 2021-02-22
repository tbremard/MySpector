using MySpector.Objects.Args;
using NLog;

namespace MySpector.Objects
{
    public class AfterXtrax : Xtrax
    {
        public override XtraxType Type => XtraxType.After;
        public override string JsonArg { get { return Jsoner.ToJson(_arg); } }

        static Logger _log = LogManager.GetCurrentClassLogger();
        private readonly AfterArg _arg;

        public AfterXtrax(AfterArg arg)
        {
            _arg = arg;
        }

        protected override IDataTruck GetOutput(IDataTruck data)
        {
            IDataTruck ret;
            string content = data.GetText();
            if (!content.Contains(_arg.Prefix))
            {
                _log.Error($"data do not contain '{_arg.Prefix}'");
                ret = DataTruck.CreateText(XtraxConst.NOT_FOUND);
            }
            else
            {
                int index = content.IndexOf(_arg.Prefix);
                int indexAfter = index + _arg.Prefix.Length;
                string contentAfter = content.Substring(indexAfter);
                ret = DataTruck.CreateText(contentAfter.Trim());
            }
            return ret;
        }
    }
}