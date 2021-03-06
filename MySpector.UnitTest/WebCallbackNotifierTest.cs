using MySpector.Objects;
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
        [Ignore("No webservice")]
        public void Check_WenInputIsValid_ThenOk()
        {
            var ret = _sut.NotifyChained("This is a test");

            Assert.IsTrue(ret, "cannot send webcallback");
        }
    }
}
