using MySpector.Objects;
using System;

namespace MySpector
{
    public class TextDoNotContainChecker : IChecker
    {
        public int? DbId { get; set; }
        public CheckerType Type => CheckerType.TextDoNotContain;
        public string JsonArg => throw new NotImplementedException("JsonArg");

        string Text;
        string Token;
        bool IgnoreCase;

        public TextDoNotContainChecker(string token, bool ignoreCase)
        {
            Token = token;
            IgnoreCase = ignoreCase;
        }

        public bool Check(IDataTruck input)
        {
            Text = input?.GetText();
            StringComparison comparisonType = IgnoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
            bool doContain = Text.Contains(Token, comparisonType);
            bool ret = !doContain;
            return ret;
        }
    }
}
