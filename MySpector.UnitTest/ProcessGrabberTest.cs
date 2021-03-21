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
            var target = new ProcessTarget() { FileName = "cmd.exe", Arguments = "/c ipconfig", StandardInput = null, WaitForExitMs = null };

            var data = _sut.Grab(target);

            _log.Debug(data.Content);
            Assert.IsTrue(data.Success);
            Assert.IsTrue(data.Content.Contains("Configuration IP "), "Invalid Content");
        }

        [Test]
        public void Grab_WenInputIsInvalid_ThenKo()
        {
            var target = new ProcessTarget() { FileName = "xxxxxxxxxxxxxx.exe", Arguments = "xxxxxxxxxx", StandardInput = null, WaitForExitMs = null };

            var data = _sut.Grab(target);

            Assert.IsFalse(data.Success);
        }
    }
}