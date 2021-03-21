using NUnit.Framework;
using NLog;
using MySpector.Objects;

namespace MySpector.UnitTest
{
    public class FileLoaderTest
    {
        static Logger _log = LogManager.GetCurrentClassLogger();

        FileLoader _sut;
        [SetUp]
        public void Setup()
        {
            _sut = new FileLoader();
        }

        [Test]
        public void Download_WenInputIsValid_ThenOk()
        {
            var target = new FileTarget("LocalFile.txt");

            var data = _sut.Grab(target);

            Assert.IsTrue(data.Success);
        }

        [Test]
        public void Download_WenInputIsInvalid_ThenKo()
        {
            var target = new FileTarget("InvalidPath.txt");

            var data = _sut.Grab(target);

            Assert.IsFalse(data.Success);
        }
    }
}