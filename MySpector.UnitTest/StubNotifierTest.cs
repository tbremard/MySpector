using NUnit.Framework;

namespace MySpector.UnitTest
{
    public class StubNotifierTest
    {
        StubNotifier _sut;
        [SetUp]
        public void Setup()
        {
            _sut = new StubNotifier();
        }

        [Test]
        public void Check_WenInputIsValid_ThenOk()
        {
            _sut.Notify("This is a test");
        }
    }
}
