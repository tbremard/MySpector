using NLog;
using NUnit.Framework;

namespace MySpector.Repo.IntTest
{
    public class RepoTest
    {
        static Logger _log = LogManager.GetCurrentClassLogger();
        const int TROX_ID = 4;

        Repo _sut;
        [SetUp]
        public void Setup()
        {
            _sut = new Repo();
            _sut.Connect();
        }

        [TearDown]
        public void TearDown()
        {
            _sut.Disconnect();
        }

        [Test]
        public void Test1()
        {
            var ret = _sut.GetNames();

            Assert.IsNotNull(ret);
            Assert.Greater(ret.Count, 0, "no items in this simple query");
        }

        [Test]
        public void GetAllTroxes()
        {
            var ret = _sut.GetAllTroxes();

            Assert.IsNotNull(ret);
            Assert.Greater(ret.Count, 0, "no items in this simple query");
            foreach (var item in ret)
            {
                _log.Debug(item.Name);
            }
        }

        [Test]
        public void GetAllXtrax()
        {
            var ret = _sut.GetAllXtrax(TROX_ID);

            Assert.IsNotNull(ret);
            Assert.Greater(ret.Count, 0, "no items in this simple query");
        }

        [Test]
        public void GetAllChecker()
        {
            var ret = _sut.GetAllChecker(TROX_ID);

            Assert.IsNotNull(ret);
            Assert.Greater(ret.Count, 0, "no items in this simple query");
        }

        [Test]
        public void GetAllNotifier()
        {
            var ret = _sut.GetAllNotifier(TROX_ID);

            Assert.IsNotNull(ret);
            Assert.Greater(ret.Count, 0, "no items in this simple query");
        }
    }
}