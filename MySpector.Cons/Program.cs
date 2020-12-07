using System;
using System.Net;
using System.IO;
using System.Collections.Generic;
using MySpector.Core;
using NLog;

namespace MySpector.Cons
{
    class Program
    {
        static Logger _log = LogManager.GetCurrentClassLogger();
        static Random random;
        const string BDI_XPath = "/html/body/div[6]/div/div/section[2]/div[1]/div[2]/section[1]/section/section[2]/section/div[1]/span[1]";
        const string BDI_URL = "https://www.bloomberg.com/quote/BDIY:IND?sref=GKjIETf1";

        const string myToys_Url = "https://www.mytoys.de/hasbro-looping-louie-4097320.html?sku=4097320";
        const string myToys_xpath = "/html/body/div[2]/div[3]/div[2]/div[2]/div[2]/div[1]/div";
        const decimal myToys_price = 19.99m; 

        static IList<WatchItem> CreateWatchList()
        {
            var ret = new List<WatchItem>();
          //  ret.Add(new WatchItem() {Name= "Galaxus: Zotac 72070 ",  Url = "https://www.galaxus.de/de/s1/product/zotac-zbox-magnus-en72070v-intel-core-i7-9750h-0gb-pc-13590721", Xpath = "/html/body/div[1]/div/div[2]/div[1]/main/div/div[2]/div/div[2]/div/div[1]/strong" });
            ret.Add(new WatchItem() { Name = "Saturn: PS4 Pro ", Url = "https://www.saturn.de/de/product/_sony-playstation-4-pro-1tb-jet-black-g-eur-2495539.html", Xpath = "/html/body/div[1]/div[2]/div[2]/div[1]/div/div[4]/div/div/div[1]/div/div[1]/div/div/div/div[2]/div[2]/span[2]" });
            return ret;
        }

        static void Main(string[] args)
        {
            _log.Debug("starting...");
            random = new Random((int)DateTime.Now.Ticks);
            var watchList = CreateWatchList();
            foreach (var item in watchList)
            {
                Process(item);

            }
            Console.ReadKey();
        }

        private static void Process(WatchItem item)
        {
            _log.Debug($"--------------------");
            _log.Debug($"Process: {item.Name}");
            HttpTarget target = HttpTarget.Create(item.Url);
            var downloader = Downloader.Create();
            var response = downloader.HttpRequest(target);
            string timeStamp = DateTime.Now.ToString("yyyy-mm-dd_hh-mm-ss");
            int rand = random.Next(1, 1000);
            string fileName = timeStamp + "__" + rand + "_dl.html";
            File.WriteAllText(fileName, response.Content);
            _log.Debug("File saved: " + fileName);
            if (response.HttpResponseCode != HttpStatusCode.OK)
            {
                _log.Debug("Error in download");
                return;
            }
            var stubNotifier = new StubNotifier();
            var checker = new NumberIsEqualChecker(1m);
            var transformer = new TextToNumberTransformer();
            var truck = DataTruck.CreateText(response.Content);
            var rootRule = new XpathXtraxRule(item.Xpath);
            var sut = new SpectorPipeline(truck, rootRule, transformer, checker, stubNotifier) {Name = item.Name };
            bool isOk = sut.Process();
            _log.Debug($"isOk:{isOk}");
        }
    }
}
