using MySpector.Objects;
using NUnit.Framework;

namespace MySpector.UnitTest
{
    public class MailNotifierTest
    {
        MailNotifier _sut;
        [SetUp]
        public void Setup()
        {
            _sut = new MailNotifier();
        }

        [Test]
        public void Check_WenInputIsValid_ThenOk()
        {
            var ret = _sut.NotifyChained("This is a test");

            Assert.IsTrue(ret, "cannot send mail");
        }
    }
}
