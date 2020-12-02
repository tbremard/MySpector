using NUnit.Framework;

namespace MySpector.UnitTest
{
    public class NumberIsLesserCheckerTest
    {
        NumberIsLesserChecker _sut;
        [SetUp]
        public void Setup()
        {

        }
         
        [TestCase(false,  0,  0, false)]
        [TestCase(true,   4,  5, false)]
        [TestCase(false,  5,  5, false)]
        [TestCase(false,  6,  5, false)]
        [TestCase(true,  -6, -5, false)]
        [TestCase(false, -5, -5, false)]
        [TestCase(false, -4, -5, false)]
        [TestCase(true,   4,  5, true)]
        [TestCase(true,   5,  5, true)]
        [TestCase(false,  6,  5, true)]
        [TestCase(true,  -6, -5, true)]
        [TestCase(true,  -5, -5, true)]
        [TestCase(false, -4, -5, true)]
        public void Check_WenInputIsValid_ThenOk(bool expectedOutput, int sample, int reference, bool orEqual)
        {
            _sut = new NumberIsLesserChecker(sample, reference, orEqual);

            var data = _sut.Check();

            Assert.AreEqual(expectedOutput, data);
        }
    }
}
