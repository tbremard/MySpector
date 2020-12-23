using NUnit.Framework;

namespace MySpector.UnitTest
{
    public class LengthOfTextXtraxRuleTest
    {
        [TestCase(26, "../*--*123456567890°+-_çàà")]
        [TestCase(26, "abcdefghijklmnopqrstuvwxyz")]
        [TestCase(2 , "             ab           ")]
        [TestCase(10, "             a        b   ")]
        [TestCase(1 , "             a            ")]
        [TestCase(0 , "")]
        [TestCase(0 , null)]
        [TestCase(1 , "a")]
        public void TransformTextToNumber_WhenStringIsValid_ThenNumberIsValid(decimal? expectedNumber, string text)
        {
            var _sut = new LengthOfTextXtrax();

            var actual = _sut.GetOutputChained(DataTruck.CreateText(text));

            Assert.IsNotNull(actual);
            decimal? actualNumber = actual.GetNumber();
            Assert.AreEqual(expectedNumber, actualNumber);
        }
    }
    }
