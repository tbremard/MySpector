using System.Collections.Generic;
using MySpector.Core;

namespace MySpector.Cons
{
    class WatchList
    {
        public static IList<Trox> Create()
        {
            var ret = new List<Trox>();
            ret.Add(CreateZotacMagnus());
            ret.Add(CreateHystouF7());
            ret.Add(CreateAllianzOblig());
            ret.Add(CreateIdealoPs4Pro());
            ret.Add(CreateBalticDryIndex());
            return ret;
        }

        private static Trox CreateZotacMagnus()
        {
            string rawString = "/html/body/div/div/div[2]/div/main/div/div[2]/div/div[2]/div/div[1]/strong";
            string escapedString = EscapeDoubleQuotes(rawString);
            var xpathParam = new XtraxDefinition(0, XtraxType.Xpath, "{\"Path\":\"" + escapedString + "\"}");
            var textToNumberParam = new XtraxDefinition(1, XtraxType.TextToNumber, null);
            var xTraxParams = new List<XtraxDefinition>();
            xTraxParams.Add(xpathParam);
            xTraxParams.Add(textToNumberParam);
            var checkerParam = new CheckerParam(CheckerType.IsLess, "{\"Reference\":800, \"OrEqual\":true}");
            string name = "Galaxus: Zotac 72070";
            string url = "https://www.galaxus.de/de/s1/product/zotac-zbox-magnus-en72070v-intel-core-i7-9750h-0gb-pc-13590721";
            bool enabled = true;
            var target = new HttpTarget(url);
            Trox ret = CreateSpecificItem(xTraxParams, checkerParam, name, target, enabled);
            return ret;
        }

        private static Trox CreateSpecificItem(List<XtraxDefinition> xTraxParams, CheckerParam checkerParam, string name, HttpTarget target, bool enabled)
        {
            var xtraxChain = XtraxFactory.CreateChain(xTraxParams);
            var checker = CheckerFactory.Create(checkerParam);
            var notifier = NotifyFactory.CreateChain();
            var ret = new Trox(name, target, enabled, xtraxChain, checker, notifier);
            return ret;
        }

        private static Trox CreateAllianzOblig()
        {
            string rawString = "/html/body/div[2]/div/header/div/div/div/div/div/div[1]/div[2]/div[1]/div[1]/div/span[3]";
            string escapedString = EscapeDoubleQuotes(rawString);
            var xpathParam = new XtraxDefinition(0, XtraxType.Xpath, "{\"Path\":\"" + escapedString + "\"}");
            var textToNumberParam = new XtraxDefinition(1, XtraxType.TextToNumber, null);
            var xTraxParams = new List<XtraxDefinition>();
            xTraxParams.Add(xpathParam);
            xTraxParams.Add(textToNumberParam);
            var checkerParam = new CheckerParam(CheckerType.IsGreater, "{\"Reference\":105, \"OrEqual\":true}");
            string name = "AllianzOblig";
            string url = "https://allianz-fonds.webfg.net/sheet/fund/FR0013192572/730?date_entree=2018-04-04";
            bool enabled = true;
            var target = new HttpTarget(url);
            Trox ret = CreateSpecificItem(xTraxParams, checkerParam, name, target, enabled);
            return ret;
        }

        private static Trox CreateHystouF7()
        {
            string rawString = "//*[@id=\"goods_price\"]";
            string escapedString = EscapeDoubleQuotes(rawString);
            var xpathParam = new XtraxDefinition(0, XtraxType.Xpath, "{\"Path\":\"" + escapedString + "\"}");
            var afterParam = new XtraxDefinition(1, XtraxType.After, "{\"Prefix\":\"$\"}");
            var textToNumberParam = new XtraxDefinition(2, XtraxType.TextToNumber, null);
            var xTraxParams = new List<XtraxDefinition>();
            xTraxParams.Add(xpathParam);
            xTraxParams.Add(afterParam);
            xTraxParams.Add(textToNumberParam);
            var checkerParam = new CheckerParam(CheckerType.IsLess, "{\"Reference\":500, \"OrEqual\":true}");
            string name = "Hystou: F7";
            string url = "https://www.hystou.com/Gaming-Mini-PC-F7-with-Nvidia-GeForce-GTX-1650-p177717.html";
            bool enabled = true;
            var target = new HttpTarget(url);
            Trox ret = CreateSpecificItem(xTraxParams, checkerParam, name, target, enabled);
            return ret;
        }

        private static Trox CreateIdealoPs4Pro()
        {
            string rawString = "/html/head/title";
            string escapedString = EscapeDoubleQuotes(rawString);
            var xpathParam = new XtraxDefinition(0, XtraxType.Xpath, "{\"Path\":\"" + escapedString + "\"}");
            var betweenParam = new XtraxDefinition(1, XtraxType.Between, "{\"Prefix\":\"ab\", \"Suffix\":\"€\"}");
            var textToNumberParam = new XtraxDefinition(2, XtraxType.TextToNumber, null);
            var xTraxParams = new List<XtraxDefinition>();
            xTraxParams.Add(xpathParam);
            xTraxParams.Add(betweenParam);
            xTraxParams.Add(textToNumberParam);
            var checkerParam = new CheckerParam(CheckerType.IsLess, "{\"Reference\":250, \"OrEqual\":true}");
            string name = "Idealo: PS4 Pro";
            string url = "https://www.idealo.de/preisvergleich/OffersOfProduct/5113034_-playstation-4-ps4-pro-1tb-sony.html";
            bool enabled = true;
            var target = new HttpTarget(url);
            Trox ret = CreateSpecificItem(xTraxParams, checkerParam, name, target, enabled);
            return ret;
        }

        private static Trox CreateBalticDryIndex()
        {
            string rawString = "//*[@id=\"description\"]";
            string escapedString = EscapeDoubleQuotes(rawString);
            var xpathParam = new XtraxDefinition(0, XtraxType.Xpath, "{\"Path\":\"" + escapedString + "\"}");
            var betweenParam = new XtraxDefinition(1, XtraxType.Between, "{\"Prefix\":\"percent to\", \"Suffix\":\"in the\"}");
            var textToNumberParam = new XtraxDefinition(2, XtraxType.TextToNumber, null);
            var xTraxParams = new List<XtraxDefinition>();
            xTraxParams.Add(xpathParam);
            xTraxParams.Add(betweenParam);
            xTraxParams.Add(textToNumberParam);
            var checkerParam = new CheckerParam(CheckerType.IsLess, "{\"Reference\":1100, \"OrEqual\":true}");
            string name = "BDI";
            string url = "https://markets.tradingeconomics.com/chart?s=bdiy:ind&span=5y&securify=new&url=commoditybaltic&AUTH=tH0xFlF0V3aKjqyJD51nR45z9WSUuCX4Bal%2FBJBXp%2FY1Pe6%2BxXY9n%2F0Zer2of37E";
            bool enabled = true;
            var target = new HttpTarget(url);
            target.Headers.Add(new HeaderEntry("Referer", "https://tradingeconomics.com/commodity/baltic"));
            target.Headers.Add(new HeaderEntry("Origin", "https://tradingeconomics.com"));
            Trox ret = CreateSpecificItem(xTraxParams, checkerParam, name, target, enabled);
            return ret;
        }

        private static string EscapeDoubleQuotes(string rawString)
        {
            string ret = rawString.Replace("\"", "\\\"");
            return ret;
        }
    }

}


