using NUnit.Framework;
using NLog;
using MySpector.Core;
using System.Collections.Generic;

namespace MySpector.UnitTest
{
    public class XtraxFactoryTest
    {
        static Logger _log = LogManager.GetCurrentClassLogger();

        [Test]
        public void Create_WenSingleRule_ThenOk()
        {
            string arg="{\"Prefix\":\"is:\"}";
            var param = new XtraxDefinition(0, XtraxType.After, arg);

            var root = XtraxFactory.Create(param);

            Assert.IsNotNull(root);
            var data = DataTruck.CreateText("The Price of item is: 100");
            var output = root.GetOutputChained(data);
            Assert.AreEqual("100", output.GetText());
        }

        [Test]
        public void Create_WenTwoRules_ThenOk()
        {
            var xpathParam = new XtraxDefinition(0, XtraxType.Xpath, "{\"Path\":\"/html/body\"}");
            var afterParam = new XtraxDefinition(1, XtraxType.After, "{\"Prefix\":\"is:\"}");
            var xTraxParams = new List<XtraxDefinition>();
            xTraxParams.Add(xpathParam);
            xTraxParams.Add(afterParam);

            var root = XtraxFactory.CreateChain(xTraxParams);

            Assert.IsNotNull(root);
            var data = DataTruck.CreateText("<html>is: <body>The Price of item is: 100</body></html>");
            var output = root.GetOutputChained(data);
            Assert.AreEqual("100", output.GetText());
        }
    }
}