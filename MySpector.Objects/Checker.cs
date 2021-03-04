using MySpector.Objects;
using System;

namespace MySpector
{
    public class TextDoContainChecker : IChecker
    {
        public int? DbId { get; set; }
        public CheckerType Type => CheckerType.TextDoContain;
        public string JsonArg { get { return Jsoner.ToJson(_arg); } }
        TextDoContainArg _arg;

        public TextDoContainChecker(TextDoContainArg arg)
        {
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
