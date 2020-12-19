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
            if (!item.Enabled)
            {
                _log.Debug($"{item.Name} is disabled");
                return false;
            }
            string filePath = GenericDownloader.DownloadToLocalFile(item);
            if (filePath == null)
            {
                _log.Error("Error in Download: Aborting processing");
                return false;
            }
            var truck = DataTruck.CreateTextFromFile(filePath);
            if (truck == null)
            {
                _log.Error("Cannot load data");
                return false;
            }
            var sut = new SpectorPipeline(item.Name, truck, item.XtraxChain, item.Checker, item.NotifyChain);
            bool isOk = sut.Process();
            _log.Debug($"isOk: {isOk}");
            return true;
        }

        public bool Process(IList<WatchItem> watchList)
        {
            bool ret = true;
            foreach (var item in watchList)
            {
                ret &= Process(item);
            }
            return ret;
        }
    }
}
