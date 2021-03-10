using MySpector.Objects.Args;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json;

namespace MySpector.Objects
{
    public class XtraxFactory
    {
        public static Xtrax Create(XtraxDefinition def)
        {
            Xtrax ret;
            switch (def.XtraxType)
            {
                case XtraxType.Xpath:
                    var xpathArg = Jsoner.FromJson<XpathArg>(def.Arg);
                    ret = new XpathXtrax(xpathArg);
                    break;
                case XtraxType.After:
                    var argAfter = JsonSerializer.Deserialize<AfterArg>(def.Arg);
                    ret = new AfterXtrax(argAfter);
                    break;
                case XtraxType.Before:
                    var argBefore = JsonSerializer.Deserialize<BeforeArg>(def.Arg);
                    ret = new BeforeXtrax(argBefore);
                    break;
                case XtraxType.Between:
                    var argBetween = JsonSerializer.Deserialize<BetweenArg>(def.Arg);
                    ret = new BetweenXtrax(argBetween);
                    break;
                case XtraxType.TextToNumber:
                    ret = new TextToNumberXtrax();
                    break;
                default:
                    throw new InvalidEnumArgumentException("unknown XtraxType: " + def.XtraxType);
            }
            ret.DbId = def.DbId;
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