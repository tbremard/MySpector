using MySpector.Objects;
using NUnit.Framework;

namespace MySpector.UnitTest
{
    public class TextDoContainCheckerTest
    {
        TextDoContainChecker _sut;
        [SetUp]
        public void Setup()
        {
            
        }

        [TestCase(false, "zzzzzzzzzzzzzz", "yyy", true)]
        [TestCase(true,  "zzzyyyzzzzzzzz", "yyy", true)]
        [TestCase(true,  "zzzzzzzzzzzyyy", "yyy", true)]
        [TestCase(false, "zzzzzzzzzzzYYY", "yyy", false)]
        public void Check_WenInputIsValid_ThenOk(bool expectedOutput, string text, string token, bool ignoreCase)
        {
            _sut = new TextDoContainChecker(token, ignoreCase);
            var inputText = DataTruck.CreateText(text);

            var data = _sut.Check(inputText);

            Assert.AreEqual(expectedOutput, data);
        }
    }
}
