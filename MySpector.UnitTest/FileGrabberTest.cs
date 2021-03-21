using NUnit.Framework;
using NLog;
using MySpector.Objects;

namespace MySpector.UnitTest
{
    public class FileGrabberTest
    {
        static Logger _log = LogManager.GetCurrentClassLogger();
        FileGrabber _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new FileGrabber();
        }

        [Test]
        public void Grab_WenInputIsValid_ThenOk()
        {
            var target = new FileTarget("LocalFile.txt");

            var data = _sut.Grab(target);

            Assert.IsTrue(data.Success);
        }

        [Test]
        public void Grab_WenInputIsInvalid_ThenKo()
        {
            var target = new FileTarget("InvalidPath.txt");

            var data = _sut.Grab(target);

            Assert.IsFalse(data.Success);
        }
    }
}