using MySpector.Objects;
using NLog;
using System;

namespace MySpector
{
    public class TextDoContainChecker : IChecker
    {
        static Logger _log = LogManager.GetCurrentClassLogger();

        public int? DbId { get; set; }
        public CheckerType Type => CheckerType.TextDoContain;
        public string JsonArg { get { return Jsoner.ToJson(_arg); } }
        TextDoContainArg _arg;

        public TextDoContainChecker(TextDoContainArg arg)
        {
            if(arg == null || string.IsNullOrEmpty(arg.Token))
            {
                _log.Error("Invalid null input");
                throw new ArgumentNullException("TextDoContainArg");
            }
            _arg = arg;
        }

        public bool Check(IDataTruck input)
        {
            string Text = input?.GetText();
            StringComparison comparisonType = _arg.IgnoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
            bool ret = Text.Contains(_arg.Token, comparisonType);
            return ret;
        }
    }
}
