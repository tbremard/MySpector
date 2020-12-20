using NUnit.Framework;

namespace MySpector.UnitTest
{
    public class TextToNumberXtraxRuleTest
    {
        [TestCase(1123.56,  "1 1 2 3 , 5 6")]
        [TestCase(1123.56,  "1123,56")]
        [TestCase(1123.56,  "1.123,56")]
        [TestCase(1123.56,  "1,123.56")]
        [TestCase(1123.56,  "1123.56")]
        [TestCase(123.56,   "123,56")]
        [TestCase(1209,     "1209,-")]
        [TestCase(1209,     "1209,–")]
        [TestCase(1209,     "1209.-")]
        [TestCase(1209,     "1209.–")]
        [TestCase(1325,     "1,325")]
        [TestCase(13630.51, "13.630,51")]
        [TestCase(13630.51, "13630,51")]
        [TestCase(null,     "")]
        [TestCase(null,     null)]
        [TestCase(null,     "a")]
        public void TransformTextToNumber_WhenStringIsValid_ThenNumberIsValid(decimal? expectedNumber, string textNumber)
        {
            var _sut = new TextToNumberXtraxRule();

            var actual = _sut.GetOutputChained(DataTruck.CreateText(textNumber));

            Assert.IsNotNull(actual);
            decimal? actualNumber = actual.GetNumber();
            Assert.AreEqual(expectedNumber, actualNumber);
        }

        [Test]
        public void TransformTextToNumber_WhenStringIsVeryLongWithLotOfComa_ThenNumberIsValid()
        {
            var _sut = new TextToNumberXtraxRule();
            string textNumber = "111,222,333,444,123.5698";

            var actual = _sut.GetOutputChained(DataTruck.CreateText(textNumber));

            Assert.IsNotNull(actual);
            decimal? actualNumber = actual.GetNumber();
            decimal expectedNumber = 111222333444123.5698m;
            Assert.AreEqual(expectedNumber, actualNumber);
        }

        [Test]
        public void TransformStringToNumber_WhenStringIsVeryLongWithLotOfPoints_ThenNumberIsValid()
        {
            var _sut = new TextToNumberXtraxRule();
            string textNumber = "111.222.333.444.123,5698";

            var actual = _sut.GetOutputChained(DataTruck.CreateText(textNumber));

            Assert.IsNotNull(actual);
            decimal? actualNumber = actual.GetNumber();
            decimal expectedNumber = 111222333444123.5698m;
            Assert.AreEqual(expectedNumber, actualNumber);
        }
    }
}
