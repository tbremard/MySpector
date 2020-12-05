using NUnit.Framework;

namespace MySpector.UnitTest
{
    public class TroxTest
    {
        [SetUp]
        public void Setup()
        {
            TestSampleFactory.Setup();
        }

        [TestCase(TestSampleId.ZOTAC_EN72070V_GALAXUS)]
        [TestCase(TestSampleId.ZOTAC_EN72070V_GALAXUS_FULL_PAGE)]
        [TestCase(TestSampleId.PS4_SATURN)]
        [TestCase(TestSampleId.PS4_SATURN_FULL_PAGE)]
        [TestCase(TestSampleId.BDIY_FULL_PAGE)]
        public void GetOutputChained_WhenXpathIsValidAndContentIsThere_ThenFound(TestSampleId sampleId)
        {
            var sample = TestSampleFactory.CreateSample(sampleId);

            var data = sample.Rule.GetOutputChained(sample.Data);

            string actual = data.GetText();
            Assert.AreEqual(sample.ExpectedOutput, actual);
        }


        [TestCase(1123.56, "1 1 2 3 , 5 6")]
        [TestCase(1123.56, "1123,56")]
        [TestCase(1123.56, "1.123,56")]
        [TestCase(1123.56, "1,123.56")]
        [TestCase(1123.56, "1123.56")]
        [TestCase(123.56,  "123,56")]
        [TestCase(null,  "")]
        [TestCase(null,  null)]
        [TestCase(null,  "a")]
        public void TransformTextToNumber_WhenStringIsValid_ThenNumberIsValid(decimal? expectedNumber, string textNumber)
        {
            var _sut = new TextToNumberTransformer();

            var actual = _sut.Transform(DataTruck.CreateText(textNumber));

            Assert.IsNotNull(actual);
            decimal? actualNumber = actual.GetNumber();
            Assert.AreEqual(expectedNumber, actualNumber);
        }

        [Test]
        public void TransformTextToNumber_WhenStringIsVeryLongWithLotOfComa_ThenNumberIsValid()
        {
            var _sut = new TextToNumberTransformer();
            string textNumber = "111,222,333,444,123.5698";

            var actual = _sut.Transform(DataTruck.CreateText(textNumber));

            Assert.IsNotNull(actual);
            decimal? actualNumber = actual.GetNumber();
            decimal expectedNumber = 111222333444123.5698m;
            Assert.AreEqual(expectedNumber, actualNumber);
        }

        [Test]
        public void TransformStringToNumber_WhenStringIsVeryLongWithLotOfPoints_ThenNumberIsValid()
        {
            var _sut = new TextToNumberTransformer();
            string textNumber = "111.222.333.444.123,5698";

            var actual = _sut.Transform(DataTruck.CreateText(textNumber));

            Assert.IsNotNull(actual);
            decimal? actualNumber = actual.GetNumber();
            decimal expectedNumber = 111222333444123.5698m;
            Assert.AreEqual(expectedNumber, actualNumber);
        }

        [TestCase("aaaaXaaaaa", "aaaaXaaaaa", "XX", "Y")]
        [TestCase("aaaaYaaaaa", "aaaaXaaaaa", "X", "Y")]
        [TestCase("aaaaYaaaaa", "aaaaXXaaaaa", "XX", "Y")]
        [TestCase("YaaaaYaaaaaY", "XXaaaaXXaaaaaXX", "XX", "Y")]
        [TestCase("", "", "", "")]
        [TestCase("", "", null, null)]
        [TestCase("aaa", "aaa", null, null)]
        [TestCase("", null, null, null)]
        public void TransformTextReplace_WhenStringIsValid_ThenOk(string expected, string text, string oldToken, string newToken)
        {
            var _sut = new TextReplaceTransformer(oldToken, newToken);

            var actual = _sut.Transform(DataTruck.CreateText(text));

            Assert.AreEqual(expected, actual.GetText());
        }

    }
}
