using MySpector.Objects;
using System;

namespace MySpector
{
    public class TextDoContainChecker : IChecker
    {
        string Text;
        string Token;
        bool IgnoreCase;
        public int? DbId { get; set; }
        public CheckerType Type => CheckerType.TextDoContain;
        public string JsonArg { get; }

        public TextDoContainChecker(string token, bool ignoreCase)
        {
            Token = token;
            IgnoreCase = ignoreCase;
        }

        public bool Check(IDataTruck input)
        {
            Text = input?.GetText();
            StringComparison comparisonType = IgnoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
            bool ret = Text.Contains(Token, comparisonType);
            return ret;
        }
    }
}
