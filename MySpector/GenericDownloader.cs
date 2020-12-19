using System;
using System.IO;
using NLog;

namespace MySpector.Core
{
    public class GenericDownloader
    {
        static Logger _log = LogManager.GetCurrentClassLogger();
        static Random random = new Random((int)DateTime.Now.Ticks);

        public static string DownloadToLocalFile(WatchItem item)
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
            string month = DateTime.Now.ToString("yyyy-MM");
            string day = DateTime.Now.ToString("dd");
            const string ROOT_DIR = "Downloads";
            string directory = Path.Combine(ROOT_DIR, month, day);
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);
            string filePath = Path.Combine(directory, fileName);
            return filePath;
        }

        public static void SocketLog()
        {
#if HTTP_DEBUG
            new EventSourceCreatedListener();
            new EventSourceListener("Microsoft-System-Net-Http");
#endif
        }
    }
}
