using NLog;
using System;
using System.Diagnostics;
using System.IO;

namespace MySpector.Objects
{
    public class FileLoader : IDownloader
    {
        static Logger _log = LogManager.GetCurrentClassLogger();

        public DownloadResponse Download(IWebTarget target)
        {
            if (target.WebTargetType != WebTargetType.FILE)
            {
                return new InvalidResponse("target has invalid type:" + target.WebTargetType, TimeSpan.Zero);
            }
            var fileTarget = target as FileTarget;
            if(!File.Exists(fileTarget.Path))
            {
                string message = "File not found: " + fileTarget.Path;
                _log.Error(message);
                return new InvalidResponse(message, TimeSpan.Zero);
            }
            var watch = new Stopwatch();
            watch.Start();
            string content = File.ReadAllText(fileTarget.Path);
            watch.Stop();
            var ret = new DownloadResponse(content, true, watch.Elapsed);
            return ret;
        }
    }
}