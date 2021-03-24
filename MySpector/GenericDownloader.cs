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

        public static LocalFile DownloadToLocalFile(Trox trox)
        {
            var ret = new LocalFile();
            try
            {
                _log.Debug($"Downloading target: [{trox.Name}/{trox.Target.Name}]) ");
                GrabResponse response = trox.Grabber.Grab(trox.Target);
                _log.Debug("Latency: " + Math.Floor( response.Latency.TotalMilliseconds) + "ms");
                string filePath = GenerateFilePath(trox);
                File.WriteAllText(filePath, response.Content);// should put size limit to avoid DOS
                _log.Debug("File saved: " + filePath);
                ret.FilePath = filePath;
                ret.Truck = DataTruck.CreateText(response.Content);
                ret.Latency = response.Latency;
                ret.GrabSuccess = response.Success;
                if(!response.Success)
                {
                    ret.ErrorMessage.AppendLine(response.ErrorMessage);
                }
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                ret.Truck = DataTruck.Empty;
                ret.GrabSuccess = false;
                ret.FilePath = null;
                ret.Latency = TimeSpan.Zero;
                ret.ErrorMessage.AppendLine(ex.Message);
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
