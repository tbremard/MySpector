using MySpector.Objects;
using NLog;
using System;

namespace MySpector.Core
{
    public class SpectorPipeline
    {
        static Logger _log = LogManager.GetCurrentClassLogger();
        public string Name => _trox?.Name;
        private Xtrax _xtrax;
        private IChecker _checker;
        private Notifier _notifier;
        private Trox _trox;

        public SpectorPipeline(Trox trox)
        {
            _trox = trox;
            _xtrax = trox.XtraxChain;
            _checker = trox.Checker;
            _notifier = trox.NotifyChain;
        }

        public bool Process()
        {
            bool ret;
            try
            {
                if (!_trox.Enabled)
                {
                    _log.Debug($"{_trox.Name} is disabled");
                    return false;
                }
                var truck = GenericDownloader.DownloadToLocalFile(_trox);
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
                Result result = new Result(_trox.DbId, data, TimeSpan.Zero);
                ServiceLocator.Instance.Repo.BeginTransaction();
                ServiceLocator.Instance.Repo.SaveResult(result);
                ServiceLocator.Instance.Repo.Commit();
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