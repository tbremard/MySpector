using MySpector.Objects;
using MySpector.Objects.Args;
using NLog;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace MySpector.Repo.IntTest
{
    public class RepoTest
    {
        static Logger _log = LogManager.GetCurrentClassLogger();
        const int TROX_ID = 2;
        const int HTTP_TARGET_ID = 1;
        const int SQL_TARGET_ID = 2;

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
                _log.Debug(item);
            }
        }

        [Test]
        public void GetWebTarget()
        {
            var ret = _sut.GetWebTarget(HTTP_TARGET_ID);

            Assert.IsNotNull(ret);
            _log.Debug(ret);
        }

        [Test]
        public void GetTargetHttp()
        {
            int idWebTarget = HTTP_TARGET_ID;// must be http id

            var ret = _sut.GetTargetHttp(idWebTarget);

            Assert.IsNotNull(ret);
            _log.Debug(ret);
        }

        [Test]
        public void GetTargetSql()
        {
            int idWebTarget = SQL_TARGET_ID;// must be sql id

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

        [Test]
        public void SaveTrox_WhenInputIsValid_ThenDbIdIsSet()
        {
            var trox = new Trox("test", true, new HttpTarget("test"), new AfterXtrax( new AfterArg() { Prefix = "test" }), new TextDoContainChecker("test", true), new StubNotifier());

            _sut.BeginTransaction();
            int? id = _sut.SaveTrox(trox);
            //_sut.Commit();
            _sut.RollBack();

            Assert.IsNotNull(id);
            Assert.AreNotEqual(0, id);
        }


        [Test]
        public void CheckEnum_CheckerType()
        {
            bool ret = _sut.CheckEnum_CheckerType();

            Assert.IsTrue(ret, "Enum integrity failed");
        }


        [Test]
        public void RoundTrip_WhenTroxIsSaved_ThenLoadedTroxIsSame()
        {
            var trox = new Trox("test", true, new HttpTarget("test"), new AfterXtrax(new AfterArg() { Prefix = "test" }), new TextDoContainChecker("test", true), new StubNotifier());

            _sut.BeginTransaction();
            int? id = _sut.SaveTrox(trox);
            var troxIds = new List<int>() { id.Value};
            var loadedTroxes = _sut.GetAllTroxes(troxIds);
            //_sut.Commit();
            _sut.RollBack();

            Assert.IsNotNull(id);
            Assert.AreNotEqual(0, id);
            Assert.AreEqual(1, loadedTroxes.Count);
            var loadedTrox = loadedTroxes.First();
            Assert.AreEqual(CheckerType.TextDoContain ,loadedTrox.Checker.Type);
        }

        [Test]
        public void SaveWebTarget()
        {
            var target = new HttpTarget("test");

            _sut.BeginTransaction();
            int id = _sut.SaveWebTarget(target);
            _sut.RollBack();

            Assert.AreNotEqual(0, id);
            _log.Debug("ID: " + id);
        }
    }
}