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

        static IList<WatchItem> CreateWatchList()
        {
            var ret = new List<WatchItem>();
            ret.Add(new WatchItem()
            {
                Name = "Galaxus: Zotac 72070",
                Url = "https://www.galaxus.de/de/s1/product/zotac-zbox-magnus-en72070v-intel-core-i7-9750h-0gb-pc-13590721",
                Xpath = "/html/body/div/div/div[2]/div/main/div/div[1]/div/div[2]/div/div[1]/strong",
                CheckerParam = new CheckerParam(CheckerType.IsLess, "{\"Reference\":1200, \"OrEqual\":true}")
            });
            ret.Add(new WatchItem()
            {
                Name = "Hystou: F7",
                Url = "https://www.hystou.com/Gaming-Mini-PC-F7-with-Nvidia-GeForce-GTX-1650-p177717.html",
                Xpath = "//*[@id=\"goods_price\"]",
                CheckerParam = new CheckerParam(CheckerType.IsLess, "{\"Reference\":900, \"OrEqual\":true}")
            });
            //  ret.Add(new WatchItem() { Name = "Saturn: PS4 Pro", Url = "https://www.saturn.de/de/product/_sony-playstation-4-pro-1tb-jet-black-g-eur-2495539.html", Xpath = "/html/body/div[1]/div[2]/div[2]/div[1]/div/div[4]/div/div/div[1]/div/div[1]/div/div/div/div[2]/div[2]/span[2]" });
            return ret;
        }

        static void Main(string[] args)
        {
            _log.Debug("starting...");
#if HTTP_DEBUG
            new EventSourceCreatedListener();
            new EventSourceListener("Microsoft-System-Net-Http");
#endif
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
            string filePath = DownloadToLocalFile(item);
            var truck = DataTruck.CreateTextFromFile(filePath);
            if (truck == null)
            {
                _log.Error("cannot load data");
                return;
            }
            var stubNotifier = new StubNotifier();
            var checker = CheckerFactory.Create(item.CheckerParam);
            var transformer = new TextToNumberTransformer();
            var rootRule = new XpathXtraxRule(item.Xpath);
            var sut = new SpectorPipeline(truck, rootRule, transformer, checker, stubNotifier) { Name = item.Name };
            bool isOk = sut.Process();
            _log.Debug($"isOk: {isOk}");
        }

        private static string DownloadToLocalFile(WatchItem item)
        {
            HttpResponse response;
            HttpTarget target = HttpTarget.Create(item.Url);
            var downloader = Downloader.Create();
            response = downloader.HttpRequest(target);
            string timeStamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            int rand = random.Next(1, 1000);
            string fileName = timeStamp + "__" + rand + "_dl.html";
            string directory = "Downloads";
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            string filePath = Path.Combine(directory, fileName);
            File.WriteAllText(filePath, response.Content);
            _log.Debug("File saved: " + fileName);
            if (response.HttpResponseCode != HttpStatusCode.OK)
            {
                _log.Debug("Error in download");
                return null;
            }
            return filePath;
        }
    }
}
