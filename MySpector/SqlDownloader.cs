using System;
using NLog;
using System.Diagnostics;

namespace MySpector.Core
{
    public class SqlDownloader : IDownloader
    {
        static Logger _log = LogManager.GetCurrentClassLogger();

        public DownloadResponse Download(Trox item)
        {
            DownloadResponse ret;
            if (item.Target == null)
            {
                _log.Error($"no target is set for item {item.Name}");
            }
            try
            {
                var watch = new Stopwatch();
                watch.Start();
                SqlResponse response = SqlRequest(item);
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

        private SqlResponse SqlRequest(Trox item)
        {
            if (item.Target.WebTargetType != WebTargetType.SQL)
                return null;
             var sqlTarget = item.Target as SqlTarget;
            _log.Debug(sqlTarget.SqlQuery);
            var ret = new SqlResponse(true, "789");
            return ret;
        }
    }
}