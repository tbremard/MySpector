using System;
using System.Net;
using System.IO;
using MySpector.Core;
using MySpector;
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
            if(!item.Enabled)
            {
                _log.Debug($"{item.Name} is disabled");
                return;
            }
            string filePath = DownloadToLocalFile(item);
            if (filePath == null)
            {
                _log.Error("Error in Download: Aborting processing");
                return;
            }
            var truck = DataTruck.CreateTextFromFile(filePath);
            if (truck == null)
            {
                _log.Error("Cannot load data");
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
            var downloader = HttpDownloader.Create();// protocol to be at the control of user
            var response = downloader.Download(item);
            string filePath = GenerateFilePath(item);
            File.WriteAllText(filePath, response.Content);
            if (!response.Success)
            {
                _log.Error("Error in download");
                return null;
            }
            return filePath;
        }

        private static string GenerateFilePath(WatchItem item)
        {
            string timeStamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            int rand = random.Next(1, 1000);
            string fileName = timeStamp + "__" + item.Token + "_" + rand + "_dl.html";
            _log.Debug("File saved: " + fileName);
            string directory = "Downloads";
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            string filePath = Path.Combine(directory, fileName);
            return filePath;
        }
    }
}
