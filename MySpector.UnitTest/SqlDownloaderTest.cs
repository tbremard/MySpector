using NUnit.Framework;
using NLog;
using MySpector.Core;
using MySpector.Models;

namespace MySpector.UnitTest
{
    public class SqlDownloaderTest
    {
        static Logger _log = LogManager.GetCurrentClassLogger();

        SqlDownloader _sut;

        [SetUp]
        public void Setup()
        {
            _sut = new SqlDownloader();
        }

        [Test]
        public void Download_WenInputIsValid_ThenOk()
        {
            var httpTarget = new SqlTarget();
            Trox item = new Trox("test", httpTarget, true, null, null, null);

            var data = _sut.Download(item);

            Assert.IsTrue(data.Success);
        }
    }
}