using MySpector.Models;
using NUnit.Framework;

namespace MySpector.UnitTest
{
    public class TextReplaceXtraxRuleTest
    {
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
            var _sut = new TextReplaceXtrax(oldToken, newToken);

            var actual = _sut.GetOutputChained(DataTruck.CreateText(text));

            Assert.AreEqual(expected, actual.GetText());
        }

    }

}
