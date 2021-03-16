using MySpector.Objects;
using NUnit.Framework;
using System.Text.Json;
using MySpector.Objects.Args;

namespace MySpector.UnitTest
{
    public class JsonerTest
    {

        [Test]
        public void Deserialize_WhenValid_ThenOk()
        {
            string jsonArg = "{\"Path\":\"xxx\"}";

            var xpathArg = JsonSerializer.Deserialize<XpathArg>(jsonArg);

            var expected = "xxx";
            Assert.AreEqual(expected, xpathArg.Path);
        }

        [Test]
        public void FromJson_WhenValid_ThenOk()
        {
            string jsonArg = "{\"Path\":\"xxx\"}";

            var xpathArg = Jsoner.FromJson<XpathArg>(jsonArg);

            var expected = "xxx";
            Assert.AreEqual(expected, xpathArg.Path);
        }

        [Test]
        public void ToJson_WhenValid_ThenOk()
        {
            var arg = new XpathArg() { Path = "xxx" };

            var json = Jsoner.ToJson(arg);

            string expected = "{\"Path\":\"xxx\"}";
            Assert.AreEqual(expected, json);
        }
    }
}
