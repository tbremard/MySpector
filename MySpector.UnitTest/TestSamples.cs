using System.ComponentModel;
using System.IO;

namespace MySpector.UnitTest
{
    internal class TestSampleFactory
    {
        static TestDescriptor PS4_SATURN = new TestDescriptor()
        {
            Html = "<html><body>"+
                   "<span display=\"inline-block\" font-size=\"xxxxxl\" font-family=\"price\" class=\"Typ bWz Pr bU\">329.<sup font-size=\"xxxl\" font-family=\"price\" class=\"Typo0 gmP Pried_0 b\">52</sup></span>"+
                   "</body></html>",
            Xpath = "/html/body/span",
            Url = "https://www.saturn.de/de/product/_sony-playstation-4-pro-1tb-jet-black-g-eur-2495539.html",
            ExpectedOutput = "329.52"

        };
        static TestDescriptor PS4_SATURN_FULL_PAGE = new TestDescriptor()
        {
            Html = File.ReadAllText("ps4pro_saturn.html"),
            Xpath = "/html/body/div[1]/div[2]/div[2]/div[1]/div/div[4]/div/div/div[1]/div/div[1]/div/div/div/div[2]/div[2]/span[2]",
            Url = "https://www.saturn.de/de/product/_sony-playstation-4-pro-1tb-jet-black-g-eur-2495539.html",
            ExpectedOutput = "329.52"
        };
        static TestDescriptor ZOTAC_EN72070V_GALAXUS = new TestDescriptor()
        {
            Html = "<html><head></head><body><div class=\"Z1h2\"><strong class=\"ZZ9v\"> 1189,99</strong></div></body></html>",
            Xpath = "/html/body/div/strong",
            Url = "https://www.saturn.de/de/product/_sony-playstation-4-pro-1tb-jet-black-g-eur-2495539.html",
            ExpectedOutput = "1189,99"
        };

        internal static TestSample CreateSample(TestSampleId sampleId)
        {
            TestSample ret;
            TestDescriptor selected;
            switch (sampleId)
            {
                case TestSampleId.PS4_SATURN:
                    selected = PS4_SATURN;
                    break;
                case TestSampleId.PS4_SATURN_FULL_PAGE:
                    selected = PS4_SATURN_FULL_PAGE;
                    break;
                case TestSampleId.ZOTAC_EN72070V_GALAXUS:
                    selected = ZOTAC_EN72070V_GALAXUS;
                    break;
                default:
                    throw new InvalidEnumArgumentException();
            }
            ret = new TestSample(selected.Html, selected.Xpath, selected.ExpectedOutput);
            return ret;
        }
    }
}
