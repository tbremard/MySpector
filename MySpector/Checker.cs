using System;

namespace MySpector
{
    public interface IChecker
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

    public class TextDoNotContainChecker : IChecker
    {
        string Text;
        string Token;
        bool IgnoreCase;

        public TextDoNotContainChecker(string text, string token, bool ignoreCase)
        {
            Text = text;
            Token = token;
            IgnoreCase = ignoreCase;
        }

        public bool Check()
        {
            StringComparison comparisonType = IgnoreCase ? StringComparison.OrdinalIgnoreCase : StringComparison.Ordinal;
            bool doContain = Text.Contains(Token, comparisonType);
            bool ret = !doContain;
            return ret;
        }
    }

    public class NumberIsLesserChecker : IChecker
    {
        decimal Sample;
        decimal Reference;
        bool OrEqual;

        public NumberIsLesserChecker(decimal sample, decimal reference, bool orEqual)
        {
            Sample = sample;
            Reference = reference;
            OrEqual = orEqual;
        }

        public bool Check()
        {
            bool ret;
            if (OrEqual)
            {
                ret = Sample <= Reference;
            }
            else
            {
                ret = Sample < Reference;
            }
            return ret;
        }
    }
}
