using System.Collections.Generic;
using NLog;

namespace MySpector.Core
{
    public class Job
    {
        static Logger _log = LogManager.GetCurrentClassLogger();

        public bool Process(WatchItem item)
        {
            _log.Debug($"--------------------");
            _log.Debug($"Process: {item.Name}");
            var pipeline = new SpectorPipeline(item);
            bool isDone = pipeline.Process();
            _log.Debug($"isDone: {isDone}");
            return isDone;
        }

        public bool Process(IList<WatchItem> watchList)
        {
            bool ret = true;
            _log.Debug("############################");
            _log.Debug("Processing " + watchList.Count + " items");
            foreach (var item in watchList)
            {
                ret &= Process(item);
            }
            _log.Debug("############################");
            return ret;
        }
    }
}
