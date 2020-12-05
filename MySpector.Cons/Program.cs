using System;
using System.Net;
using System.IO;

namespace MySpector.Cons
{
    class Program
    {
        static void Main(string[] args)
        {
            const string BLOOMBERG_BDI = "https://www.bloomberg.com/quote/BDIY:IND?sref=GKjIETf1";
            HttpTarget target = HttpTarget.Create(BLOOMBERG_BDI);
            var downloader = Downloader.Create();
            var response = downloader.HttpRequest(target);
            if(response.HttpResponseCode != HttpStatusCode.OK)
            {
                Console.WriteLine("error in download");
                return ;
            }
            var stubNotifier = new StubNotifier();
            const decimal TARGET_PRICE = 1197m;
            var checker = new NumberIsEqualChecker(TARGET_PRICE);
            var transformer = new TextToNumberTransformer();
            File.WriteAllText("dl.html", response.Content);
            var truck = DataTruck.CreateText(response.Content);
            var rootRule = new XpathXtraxRule("/html/body/div[6]/div/div/section[2]/div[1]/div[2]/section[1]/section/section[2]/section/div[1]/span[1]");
            var sut = new SpectorPipeline(truck, rootRule, transformer, checker, stubNotifier);
            bool isOk = sut.Process();
            Console.WriteLine($"isOk:{isOk}");
            Console.ReadKey();
        }
    }
}
