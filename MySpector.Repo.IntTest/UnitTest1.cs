using NUnit.Framework;

namespace MySpector.Repo.IntTest
{
    public class Tests
    {
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
    }
}