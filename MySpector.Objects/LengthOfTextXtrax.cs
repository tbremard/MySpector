using NLog;

namespace MySpector.Objects
{
    public class LengthOfTextXtrax : Xtrax
    {
        public override XtraxType Type => XtraxType.LengthOfText;

        static Logger _log = LogManager.GetCurrentClassLogger();

        protected override IDataTruck GetOutput(IDataTruck data)
        {
            IDataTruck ret;
            string content = data.GetText();
            if (string.IsNullOrEmpty(content))
            {
                _log.Error("Text is empty : return 0");
                return DataTruck.CreateNumber(0);
            }
            string trimmed = content.Trim();
            ret = DataTruck.CreateNumber(trimmed.Length);
            _log.Trace("Length of [" + data.PreviewText + "] :" + trimmed.Length);
            return ret;
        }
    }
}