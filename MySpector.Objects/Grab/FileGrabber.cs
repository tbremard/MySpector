﻿using NLog;
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
            if (target.TargetType != TargetType.FILE)
            {
                return new InvalidResponse(null, TimeSpan.Zero, "Target has invalid type:" + target.TargetType);
            }
            var fileTarget = target as FileTarget;
            if(!File.Exists(fileTarget.Path))
            {
                string message = "File not found: " + fileTarget.Path;
                _log.Error(message);
                return new InvalidResponse(null, TimeSpan.Zero, message);
            }
            var watch = new Stopwatch();
            watch.Start();
            string content = File.ReadAllText(fileTarget.Path);
            watch.Stop();
            var ret = new GrabResponse(content, true, watch.Elapsed, null);
            return ret;
        }
    }
}