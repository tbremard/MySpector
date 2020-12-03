using System;

namespace MySpector
{

    public interface IChecker
    {
        public bool Check(IDataTruck input);
    }

    public class TextDoContainChecker : IChecker
    {
        string Text;
        string Token;
        bool IgnoreCase;

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

    public class TextDoNotContainChecker : IChecker
    {
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
