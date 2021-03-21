using System;
using System.IO;
using MySpector.Objects;
using NLog;

namespace MySpector.Core
{
    public class GenericDownloader
    {
        static Logger _log = LogManager.GetCurrentClassLogger();
        static Random random = new Random((int)DateTime.Now.Ticks);

        public static IDataTruck DownloadToLocalFile(Trox item)
        {
            IDataTruck ret;
            try
            {
                _log.Debug($"Downloading target of Trox('{item.Name}') ");
                var response = item.Downloader.Grab(item.Target);
                _log.Debug("Latency: " + Math.Floor( response.Latency.TotalMilliseconds) + "ms");
                string filePath = GenerateFilePath(item);
                File.WriteAllText(filePath, response.Content);
                _log.Debug("File saved: " + filePath);
                if (!response.Success)
                {
                    _log.Error("Error in download");
                    return null;
                }
                ret = DataTruck.CreateText(response.Content);
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                return null;
            }
            return ret;
        }

        private static string GenerateFilePath(Trox item)
        {
            string timeStamp = DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss");
            int rand = random.Next(1, 1000);
            string fileName = timeStamp + "__" + item.FileToken + "_" + rand + "_.dl";
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
