using System;
using NLog;
using System.Diagnostics;
using MySpector.Objects;

namespace MySpector.Core
{
    public class SqlDownloader : IGrabber
    {
        static Logger _log = LogManager.GetCurrentClassLogger();

        public GrabResponse Grab(IGrabTarget target)
        {
            if (target == null)
            {
                _log.Error($"no target is set!");
            }
            if (target.TargetType != GrabTargetType.SQL)
                return null;
            GrabResponse ret;
            try
            {
                var watch = new Stopwatch();
                watch.Start();
                SqlResponse response = SqlRequest(target);
                watch.Stop();
                ret = new GrabResponse(response.Content, response.IsOk, watch.Elapsed);
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                ret = new GrabResponse(string.Empty, false, TimeSpan.Zero);
            }
            return ret;
        }

        private SqlResponse SqlRequest(IGrabTarget target)
        {
             var sqlTarget = target as SqlTarget;
            _log.Debug(sqlTarget.SqlQuery);
            var ret = new SqlResponse(true, "789");
            return ret;
        }
    }
}