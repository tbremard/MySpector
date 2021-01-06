using NLog;
using System;

namespace MySpector.Core
{
    public class SpectorPipeline
    {
        static Logger _log = LogManager.GetCurrentClassLogger();
        public string Name => _item?.Name;
        private Xtrax _xtrax;
        private IChecker _checker;
        private Notify _notifier;
        private Trox _item;

        public SpectorPipeline(Trox item)
        {
            _item = item;
            _xtrax = item.XtraxChain;
            _checker = item.Checker;
            _notifier = item.NotifyChain;
        }

        public bool Process()
        {
            bool ret;
            try
            {
                if (!_item.Enabled)
                {
                    _log.Debug($"{_item.Name} is disabled");
                    return false;
                }
                var truck = GenericDownloader.DownloadToLocalFile(_item);
                if (truck == null)
                {
                    _log.Error("Error in Download: Aborting processing");
                    return false;
                }
                var data = _xtrax.GetOutputChained(truck);
                if (data.GetText() == XtraxConst.NOT_FOUND)
                {
                    _log.Error($"Data extraction failed for item: '{Name}'");
                    return false;
                }
                _log.Debug($"Extraction of '{Name}' = " + data.GetText());
                bool isSignaled = _checker.Check(data);
                if (isSignaled)
                {
                    _notifier.NotifyChained("Pipeline triggered alert");
                }
                ret = true;
            }
            catch (Exception ex)
            {
                _log.Error($"Data extraction failed for item: '{Name}'");
                _log.Error(ex);
                ret = false;
            }
            return ret;
        }
    }
}