using MySpector.Objects;
using NUnit.Framework;

namespace MySpector.UnitTest
{
    public class NumberIsLesserCheckerTest
    {
        NumberIsLessChecker _sut;
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
            var arg = new ComparaisonArg() { Reference = reference, OrEqual = orEqual };
            _sut = new NumberIsLessChecker(arg);
            var input = new DataTruck(null, new decimal(sample));

            var data = _sut.Check(input);

            Assert.AreEqual(expectedOutput, data);
        }
    }
}
