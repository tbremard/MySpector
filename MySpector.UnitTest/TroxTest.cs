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
            TestSampleFactory.Setup();
        }

        [TestCase(TestSampleId.ZOTAC_EN72070V_GALAXUS)]
        [TestCase(TestSampleId.ZOTAC_EN72070V_GALAXUS_FULL_PAGE)]
        [TestCase(TestSampleId.PS4_SATURN)]
        [TestCase(TestSampleId.PS4_SATURN_FULL_PAGE)]
        [TestCase(TestSampleId.BDIY_FULL_PAGE)]
        public void ExtractData_WhenXpathIsValidAndContentIsThere_ThenFound(TestSampleId sampleId)
        {
            var sample = TestSampleFactory.CreateSample(sampleId);

            var data = _sut.ExtractData(sample.Rump, sample.Rule);

            Assert.AreEqual(sample.ExpectedOutput, data.Value);
        }

        [Test]
        public void ExtractData_When2Rules_ThenFound()
        {
            string Html = "<html><head></head><body><div class=\"Z2\"><strong class=\"Z9v\">The price of the item is: 1189,99 EUR</strong></div></body></html>";
            string Xpath = "/html/body/div/strong";
            var rump = new Rump(Html);
            var rootRule = new XpathXtraxRule(Xpath);
            var nextRule = new BetweenXtraxRule("is:", "EUR");
            rootRule.SetNext(nextRule);

            var data = _sut.ExtractData(rump, rootRule);

            string ExpectedOutput = "1189,99";
            Assert.AreEqual(ExpectedOutput, data.Value);
        }

        [TestCase(1123.56, "1 1 2 3 , 5 6")]
        [TestCase(1123.56, "1123,56")]
        [TestCase(1123.56, "1.123,56")]
        [TestCase(1123.56, "1,123.56")]
        [TestCase(1123.56, "1123.56")]
        [TestCase(123.56,  "123,56")]
        [TestCase(0,  "")]
        [TestCase(0,  null)]
        [TestCase(0,  "a")]
        public void TransformStringToNumber_WhenStringIsValid_ThenNumberIsValid(decimal expectedNumber, string textNumber)
        {
            var actual = _sut.TransformStringToNumber(textNumber);

            Assert.AreEqual(expectedNumber, actual);
        }

        [Test]
        public void TransformStringToNumber_WhenStringIsVeryLongWithLotOfComa_ThenNumberIsValid()
        {
            string textNumber= "111,222,333,444,123.5698";

            var actual = _sut.TransformStringToNumber(textNumber);

            decimal expectedNumber = 111222333444123.5698m;
            Assert.AreEqual(expectedNumber, actual);
        }

        [Test]
        public void TransformStringToNumber_WhenStringIsVeryLongWithLotOfPoints_ThenNumberIsValid()
        {
            string textNumber = "111.222.333.444.123,5698";

            var actual = _sut.TransformStringToNumber(textNumber);

            decimal expectedNumber = 111222333444123.5698m;
            Assert.AreEqual(expectedNumber, actual);
        }
    }
}
