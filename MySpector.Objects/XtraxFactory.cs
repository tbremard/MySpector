using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json;

namespace MySpector.Objects
{
    public class AfterArg
    {
        public string Prefix { get; set; }
    }

    public class BeforeArg
    {
        public string Suffix { get; set; }
    }

    public class XpathArg
    {
        public string Path { get; set; }
    }


    public class BetweenArg
    {
        public string Prefix { get; set; }
        public string Suffix { get; set; }
    }


    public class XtraxFactory
    {
        public static Xtrax Create(XtraxDefinition def)
        {
            Xtrax ret;
            switch (def.XtraxType)
            {
                case XtraxType.Xpath:
                    ret = new XpathXtrax(def.Arg);
                    break;
                case XtraxType.After:
                    var argAfter = JsonSerializer.Deserialize<AfterArg>(def.Arg);
                    ret = new AfterXtrax(argAfter.Prefix);
                    break;
                case XtraxType.Before:
                    var argBefore = JsonSerializer.Deserialize<BeforeArg>(def.Arg);
                    ret = new BeforeXtrax(argBefore.Suffix);
                    break;
                case XtraxType.Between:
                    var argBetween = JsonSerializer.Deserialize<BetweenArg>(def.Arg);
                    ret = new BetweenXtrax(argBetween.Prefix, argBetween.Suffix);
                    break;
                case XtraxType.TextToNumber:
                    ret = new TextToNumberXtrax();
                    break;
                default:
                    throw new InvalidEnumArgumentException("unknown XtraxType: " + def.XtraxType);
            }
            return ret;
        }

        public static Xtrax CreateChain(IList<XtraxDefinition> xTraxParams)
        {
            if (xTraxParams.Count == 0)
                return new EmptyXtrax();
            Xtrax ret = Create(xTraxParams[0]);
            for (int i = 1; i < xTraxParams.Count; i++)
            {
                Xtrax next = Create(xTraxParams[i]);
                ret.SetNext(next);
            }
            return ret;
        }
    }
}