using NUnit.Framework;
using NLog;
using MySpector.Core;

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
            WatchItem item = new WatchItem("test", httpTarget, true, null, null, null);

            var data = _sut.Download(item);

            Assert.IsTrue(data.Success);
        }
    }
}