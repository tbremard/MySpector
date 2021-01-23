using NUnit.Framework;
using NLog;
using MySpector.Core;
using MySpector.Objects;

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
            string connectionString = "";
            string sqlQuery = "";
            var target = new SqlTarget(connectionString, sqlQuery);
            Trox item = new Trox("test", true, target, null, null, null);

            var data = _sut.Download(item);

            Assert.IsTrue(data.Success);
        }
    }
}