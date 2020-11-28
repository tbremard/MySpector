using System.ComponentModel;

namespace MySpector.UnitTest
{
    public enum TestSampleId
    {
        PS4_MEDIAMARK,
        ZOTAC_EN72070V_GALAXUS
    }

    public class TestDescriptor
    {
        public string Html;
        public string Xpath;
        public string Url;
    }

    internal class TestSampleFactory
    {
        static TestDescriptor PS4_MEDIAMARK = new TestDescriptor()
        {
            Html = "<html><body>"+
                   "<span display=\"inline-block\" font-size=\"xxxxxl\" font-family=\"price\" class=\"Typ bWz Pr bU\">329.<sup font-size=\"xxxl\" font-family=\"price\" class=\"Typo0 gmP Pried_0 b\">52</sup></span>"+
                   "</body></html>",
            Xpath = "/html/body/span",
            Url = "https://www.saturn.de/de/product/_sony-playstation-4-pro-1tb-jet-black-g-eur-2495539.html"

        };

        static TestDescriptor ZOTAC_EN72070V_GALAXUS = new TestDescriptor()
        {
            Html = "<html><head></head><body><div class=\"Z1h2\"><strong class=\"ZZ9v\"> 1189,99</strong></div></body></html>",
            Xpath = "/html/body/div/strong",
            Url = "https://www.saturn.de/de/product/_sony-playstation-4-pro-1tb-jet-black-g-eur-2495539.html"
        };

        internal static TestSample CreateSample(TestSampleId sampleId)
        {
            TestSample ret;
            switch (sampleId)
            {
                case TestSampleId.PS4_MEDIAMARK:
                    ret = new TestSample(PS4_MEDIAMARK.Html, PS4_MEDIAMARK.Xpath);
                    break;
                case TestSampleId.ZOTAC_EN72070V_GALAXUS:
                    ret = new TestSample(ZOTAC_EN72070V_GALAXUS.Html, ZOTAC_EN72070V_GALAXUS.Xpath);
                    break;
                default:
                    throw new InvalidEnumArgumentException();
            }
            return ret;
        }
    }
}
