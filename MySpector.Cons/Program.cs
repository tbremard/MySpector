using System;
using System.Net;
using System.IO;
using MySpector.Core;
using NLog;

namespace MySpector.Cons
{
    class Program
    {
        static Logger _log = LogManager.GetCurrentClassLogger();
        static Random random;

        static void Main(string[] args)
        {
            _log.Debug("starting...");
#if HTTP_DEBUG
            new EventSourceCreatedListener();
            new EventSourceListener("Microsoft-System-Net-Http");
#endif
            random = new Random((int)DateTime.Now.Ticks);
            var watchList = WatchList.Create();
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
            var rootRule = item.XtraxChain;
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
