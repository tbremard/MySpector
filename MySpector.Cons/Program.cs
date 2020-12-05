using System;
using System.Net;
using System.IO;

namespace MySpector.Cons
{
    class Program
    {
        const string BDI_XPath = "/html/body/div[6]/div/div/section[2]/div[1]/div[2]/section[1]/section/section[2]/section/div[1]/span[1]";
        const string BDI_URL = "https://www.bloomberg.com/quote/BDIY:IND?sref=GKjIETf1";

        const string myToys_Url = "https://www.mytoys.de/hasbro-looping-louie-4097320.html?sku=4097320";
        const string myToys_xpath = "/html/body/div[2]/div[3]/div[2]/div[2]/div[2]/div[1]/div";
        const decimal myToys_price = 19.99m; 

        static void Main(string[] args)
        {
            HttpTarget target = HttpTarget.Create(myToys_Url);
            var downloader = Downloader.Create();
            var response = downloader.HttpRequest(target);
            if(response.HttpResponseCode != HttpStatusCode.OK)
            {
                Console.WriteLine("error in download");
                return ;
            }
            var stubNotifier = new StubNotifier();
            var checker = new NumberIsEqualChecker(myToys_price);
            var transformer = new TextToNumberTransformer();
            string timeStamp = DateTime.Now.ToString("yyyy-mm-dd_hh-mm-ss");
            File.WriteAllText(timeStamp+"_dl.html", response.Content);
            var truck = DataTruck.CreateText(response.Content);
            var rootRule = new XpathXtraxRule(myToys_xpath);
            var nextRule = new BeforeXtraxRule("€");
            rootRule.SetNext(nextRule);
            var sut = new SpectorPipeline(truck, rootRule, transformer, checker, stubNotifier);
            bool isOk = sut.Process();
            Console.WriteLine($"isOk:{isOk}");
            Console.ReadKey();
        }
    }
}
