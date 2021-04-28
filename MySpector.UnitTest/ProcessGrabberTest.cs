using NUnit.Framework;
using NLog;
using MySpector.Objects;

namespace MySpector.UnitTest
{
    public class ProcessGrabberTest
    {
        static Logger _log = LogManager.GetCurrentClassLogger();
        ProcessGrabber _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new ProcessGrabber();
        }

        [Test]
        public void Grab_WenInputIsValid_ThenOk()
        {
            var target = new ProcessTarget() { FileName = "cmd.exe", Arguments = "/c ipconfig", StandardInput = null};

            var data = _sut.Grab(target);

            _log.Debug(data.Content);
            Assert.IsTrue(data.Success);
            Assert.IsTrue(data.Content.Contains("Ethernet"), "Invalid Content");
        }

        [Test]
        public void Grab_WenInputIsInvalid_ThenKo()
        {
            var target = new ProcessTarget() { FileName = "xxxxxxxxxxxxxx.exe", Arguments = "xxxxxxxxxx", StandardInput = null};

            var data = _sut.Grab(target);

            Assert.IsFalse(data.Success);
        }

        [Test]
        public void Grab_WenTimeoutIsReached_ThenKo()
        {
            var target = new ProcessTarget() { FileName = "cmd.exe", Arguments = "/c ping localhost -w 1000 -n 4", StandardInput = null};
            target.TimeoutMs = 3000;

            var data = _sut.Grab(target);

            _log.Debug(data.Content);
            Assert.IsFalse(data.Success);
        }

        [Test]
        public void Grab_WenTimeoutIsNotReached_ThenOk()
        {
            var target = new ProcessTarget() { FileName = "cmd.exe", Arguments = "/c ping localhost -w 1000 -n 4", StandardInput = null };
            target.TimeoutMs = 5000;

            var data = _sut.Grab(target);

            _log.Debug(data.Content);
            Assert.IsTrue(data.Success);
        }
    }
}