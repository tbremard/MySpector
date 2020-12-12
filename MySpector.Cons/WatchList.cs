﻿using System.Collections.Generic;
using MySpector.Core;

namespace MySpector.Cons
{
    class WatchList
    {
        public static IList<WatchItem> Create()
        {
            var ret = new List<WatchItem>();
            ret.Add(CreateZotacMagnus());
            ret.Add(CreateHystouF7());

            //  ret.Add(new WatchItem() { Name = "Saturn: PS4 Pro", Url = "https://www.saturn.de/de/product/_sony-playstation-4-pro-1tb-jet-black-g-eur-2495539.html", Xpath = "/html/body/div[1]/div[2]/div[2]/div[1]/div/div[4]/div/div/div[1]/div/div[1]/div/div/div/div[2]/div[2]/span[2]" });
            return ret;
        }

        private static WatchItem CreateZotacMagnus()
        {
            string rawString = "/html/body/div/div/div[2]/div/main/div/div[1]/div/div[2]/div/div[1]/strong";
            string escapedString = EscapeDoubleQuotes(rawString);
            var xpathParam = new XtraxDefinition(XtraxType.Xpath, "{\"Path\":\"" + escapedString + "\"}");
            var xTraxParams = new List<XtraxDefinition>();
            xTraxParams.Add(xpathParam);
            var xtraxChain = XtraxFactory.CreateChain(xTraxParams);

            var ret = new WatchItem()
            {
                Name = "Galaxus: Zotac 72070",
                Url = "https://www.galaxus.de/de/s1/product/zotac-zbox-magnus-en72070v-intel-core-i7-9750h-0gb-pc-13590721",
                XtraxChain = xtraxChain,
                CheckerParam = new CheckerParam(CheckerType.IsLess, "{\"Reference\":1200, \"OrEqual\":true}")
            };
            return ret;
        }

        private static WatchItem CreateHystouF7()
        {
            string rawString = "//*[@id=\"goods_price\"]";
            string escapedString = EscapeDoubleQuotes(rawString);
            var xpathParam = new XtraxDefinition(XtraxType.Xpath, "{\"Path\":\"" + escapedString + "\"}");
            var afterParam = new XtraxDefinition(XtraxType.After, "{\"Prefix\":\"$\"}");
            var xTraxParams = new List<XtraxDefinition>();
            xTraxParams.Add(xpathParam);
            xTraxParams.Add(afterParam);
            var xtraxChain = XtraxFactory.CreateChain(xTraxParams);

            var ret = new WatchItem()
            {
                Name = "Hystou: F7",
                Url = "https://www.hystou.com/Gaming-Mini-PC-F7-with-Nvidia-GeForce-GTX-1650-p177717.html",
                XtraxChain = xtraxChain,
                CheckerParam = new CheckerParam(CheckerType.IsLess, "{\"Reference\":900, \"OrEqual\":true}")
            };
            return ret;
        }

        private static string EscapeDoubleQuotes(string rawString)
        {
            string ret = rawString.Replace("\"", "\\\"");
            return ret;
        }
    }
}
