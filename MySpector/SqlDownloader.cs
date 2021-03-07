using System;
using NLog;
using System.Diagnostics;
using MySpector.Objects;

namespace MySpector.Core
{
    public class SqlDownloader : IDownloader
    {
        static Logger _log = LogManager.GetCurrentClassLogger();

        public DownloadResponse Download(IWebTarget target)
        {
            if (target == null)
            {
                _log.Error($"no target is set!");
            }
            if (target.WebTargetType != WebTargetType.SQL)
                return null;
            DownloadResponse ret;
            try
            {
                var watch = new Stopwatch();
                watch.Start();
                SqlResponse response = SqlRequest(target);
                watch.Stop();
                ret = new DownloadResponse(response.Content, response.IsOk, watch.Elapsed);
            }
            catch (Exception ex)
            {
                _log.Error(ex);
                ret = new DownloadResponse(string.Empty, false, TimeSpan.Zero);
            }
            return ret;
        }

        private SqlResponse SqlRequest(IWebTarget target)
        {
             var sqlTarget = target as SqlTarget;
            _log.Debug(sqlTarget.SqlQuery);
            var ret = new SqlResponse(true, "789");
            return ret;
        }
    }
}