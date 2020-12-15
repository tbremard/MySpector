using NUnit.Framework;

namespace MySpector.UnitTest
{
    public class WebCallbackNotifierTest
    {
        WebCallbackNotifier _sut;
        [SetUp]
        public void Setup()
        {
            _sut = new WebCallbackNotifier();
        }

        [Test]
        public void Check_WenInputIsValid_ThenOk()
        {
            var ret = _sut.Notify("This is a test");

            Assert.IsTrue(ret, "cannot send webcallback");
        }
    }
}
