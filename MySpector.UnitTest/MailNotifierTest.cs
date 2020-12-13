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
            _sut.Notify("This is a test");
        }
    }
}
