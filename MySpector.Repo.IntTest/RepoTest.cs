using NLog;
using NUnit.Framework;
using System.Collections.Generic;

namespace MySpector.Repo.IntTest
{
    public class RepoTest
    {
        static Logger _log = LogManager.GetCurrentClassLogger();
        const int TROX_ID = 2;

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
        public void GetAllTroxes_WhenIdsAreProvided_ThenOk()
        {
            var troxIds = new List<int>();
            troxIds.Add(TROX_ID);

            var ret = _sut.GetAllTroxes(troxIds);

            Assert.IsNotNull(ret);
            Assert.Greater(ret.Count, 0, "no items in this simple query");
            foreach (var item in ret)
            {
                _log.Debug(item.Name);
            }
        }

        [Test]
        public void GetWebTarget()
        {
            var ret = _sut.GetWebTarget(TROX_ID);

            Assert.IsNotNull(ret);
            _log.Debug(ret);
        }


        [Test]
        public void GetTargetHttp()
        {
            int idWebTarget = 1;// must be http id

            var ret = _sut.GetTargetHttp(idWebTarget);

            Assert.IsNotNull(ret);
            _log.Debug(ret);
        }


        [Test]
        public void GetTargetSql()
        {
            int idWebTarget = 2;// must be sql id

            var ret = _sut.GetTargetSql(idWebTarget);

            Assert.IsNotNull(ret);
            _log.Debug(ret);
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