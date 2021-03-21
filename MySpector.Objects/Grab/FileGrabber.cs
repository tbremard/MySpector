using NLog;
using System;
using System.Diagnostics;
using System.IO;

namespace MySpector.Objects
{
    public class FileGrabber : IGrabber
    {
        static Logger _log = LogManager.GetCurrentClassLogger();

        public GrabResponse Grab(IGrabTarget target)
        {
            if (target.TargetType != GrabTargetType.FILE)
            {
                return new InvalidResponse("target has invalid type:" + target.TargetType, TimeSpan.Zero);
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
            var ret = new GrabResponse(content, true, watch.Elapsed);
            return ret;
        }
    }
}