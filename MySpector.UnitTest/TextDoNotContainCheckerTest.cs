using MySpector.Objects;
using NUnit.Framework;

namespace MySpector.UnitTest
{
    public class TextDoNotContainCheckerTest
    {
        TextDoNotContainChecker _sut;
        [SetUp]
        public void Setup()
        {

        }

        [TestCase(true, "zzzzzzzzzzzzzz", "yyy", true)]
        [TestCase(false, "zzzyyyzzzzzzzz", "yyy", true)]
        [TestCase(false, "zzzzzzzzzzzyyy", "yyy", true)]
        [TestCase(true, "zzzzzzzzzzzYYY", "yyy", false)]
        public void Check_WenInputIsValid_ThenOk(bool expectedOutput, string text, string token, bool ignoreCase)
        {
            _sut = new TextDoNotContainChecker(token, ignoreCase);
            var inputText = DataTruck.CreateText(text);

            var data = _sut.Check(inputText);

            Assert.AreEqual(expectedOutput, data);
        }
    }
}
