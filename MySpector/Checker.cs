using System;

namespace MySpector
{
    interface IChecker
    {
        public bool Check();
    }

    public class TextDoContainChecker : IChecker
    {
        string Text;
        string Token;
        bool IgnoreCase;

        public TextDoContainChecker(string text, string token, bool ignoreCase)
        {
            Text = text;
            Token = token;
            IgnoreCase = ignoreCase;
        }

        public bool Check()
        {
            StringComparison comparisonType = IgnoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
            bool ret = Text.Contains(Token, comparisonType);
            return ret;
        }
    }
}
