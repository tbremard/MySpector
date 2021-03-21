using System;
using NLog;
using System.Diagnostics;
using MySpector.Objects;

namespace MySpector.Core
{
    public class SqlGrabber : IGrabber
    {
        static Logger _log = LogManager.GetCurrentClassLogger();

        public GrabResponse Grab(IGrabTarget target)
        {
            if (target == null)
            {
                _log.Error($"no target is set!");
            }
            if (target.TargetType != TargetType.SQL)
                return null;
            GrabResponse ret;
            try
            {
                var watch = new Stopwatch();
                watch.Start();
                var sqlTarget = target as SqlTarget;
                _log.Debug(sqlTarget.SqlQuery);
                watch.Stop();
                ret = new GrabResponse("sql returned value", true, watch.Elapsed);
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                ret = new GrabResponse(string.Empty, false, TimeSpan.Zero);
            }
            return ret;
        }
    }
}