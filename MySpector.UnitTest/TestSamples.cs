using System.ComponentModel;

namespace MySpector.UnitTest
{
    public enum TestSampleId
    {
        PS4_MEDIAMARK,
        ZOTAC_EN72070V_GALAXUS
    }

    internal class TestSampleFactory
    {
        internal const string PS4_MEDIAMARK = "< span display = \"inline-block\" font - size = \"xxxxxl\" font - family = \"price\" class=\"Typostyled__StyledInfoTypo-sc-1jga2g7-0 frzVZu Pricestyled__BrandedPriceTypo-sc-1bu146t-0 kcGTpT\">329." +
                         "    <sup font-size=\"xxxl\" font-family=\"price\" class=\"Typostyled__StyledInfoTypo-sc-1jga2g7-0 jOkPvc Pricestyled__BrandedPriceTypo-sc-1bu146t-0 kcGTpT\">52</sup>" +
                         "</span>";
        internal const string PS4_MEDIAMARK_RULE = "...";
        internal const string ZOTAC_EN72070V_GALAXUS = "<html><head></head><body><div class=\"Z1h2\"><strong class=\"ZZ9v\"> 1189,99</strong></div></body></html>";
        internal const string ZOTAC_EN72070V_GALAXUS_RULE = "/html/body/div/strong";


        //html/body/div[1]/div/div[2]/div[1]/main/div/div[2]/div/div[2]/div/div[1]/strong
        internal static TestSample CreateSample(TestSampleId sampleId)
        {
            TestSample ret;
            switch (sampleId)
            {
                case TestSampleId.PS4_MEDIAMARK:
                    ret = new TestSample(PS4_MEDIAMARK, PS4_MEDIAMARK_RULE);
                    break;
                case TestSampleId.ZOTAC_EN72070V_GALAXUS:
                    ret = new TestSample(ZOTAC_EN72070V_GALAXUS, ZOTAC_EN72070V_GALAXUS_RULE);
                    break;
                default:
                    throw new InvalidEnumArgumentException();
            }
            return ret;
        }
    }
}
