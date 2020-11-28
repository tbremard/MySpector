using NUnit.Framework;

namespace MySpector.UnitTest
{
    public class TroxTest
    {
        Trox _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new Trox();
        }

        [TestCase("1189,99", TestSamples.ZOTAC_EN72070V_GALAXUS)]
       // [TestCase("329", TestSamples.PS4_MEDIAMARK)]
        public void ExtractData_WhenContentIsThere_ThenFound(string expected, string content)
        {
            var rump = CreateLocalRump(content);
            var rule = CreateLocaleRule();

            var data = _sut.ExtractData(rump, rule);

            Assert.AreEqual(expected, data.Value);
        }

        private ITroxRule CreateLocaleRule()
        {
            string xpath = "/html/body/div/strong";
            var ret = new XpathTroxRule(xpath);
            return ret;
        }

        private Rump CreateLocalRump(string content)
        {
            var ret = new Rump(content);
            return ret;
        }
    }

    internal class TestSamples
    {
        internal const string PS4_MEDIAMARK = "< span display = \"inline-block\" font - size = \"xxxxxl\" font - family = \"price\" class=\"Typostyled__StyledInfoTypo-sc-1jga2g7-0 frzVZu Pricestyled__BrandedPriceTypo-sc-1bu146t-0 kcGTpT\">329." +
                         "    <sup font-size=\"xxxl\" font-family=\"price\" class=\"Typostyled__StyledInfoTypo-sc-1jga2g7-0 jOkPvc Pricestyled__BrandedPriceTypo-sc-1bu146t-0 kcGTpT\">52</sup>" +
                         "</span>";

        internal const string ZOTAC_EN72070V_GALAXUS = "<html><head></head><body><div class=\"Z1h2\"><strong class=\"ZZ9v\"> 1189,99</strong></div></body></html>";

        //html/body/div[1]/div/div[2]/div[1]/main/div/div[2]/div/div[2]/div/div[1]/strong
    }
}
