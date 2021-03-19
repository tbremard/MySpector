using MySpector.Objects;
using NUnit.Framework;
using MySpector.Objects.Args;

namespace MySpector.UnitTest
{
    public class JsonXtraxTest
    {
        [Test]
        public void GetOutputChained_WhenXmlIsValid_ThenDataIsExtracted()
        {
            string json = "{\"MyItems\":{\"a\":\"tata\", \"b\":\" toto\" , \"c\":\"titi\"}}";
            string Xpath = "\\MyItems\\b";
            var arg = new JsonArg() { Path = Xpath };
            var truck = DataTruck.CreateText(json);
            var rootRule = new JsonXtrax(arg);

            var data = rootRule.GetOutputChained(truck);

            string ExpectedOutput = "toto";
            string actual = data.GetText();
            Assert.AreEqual(ExpectedOutput, actual);
        }
    }
}
