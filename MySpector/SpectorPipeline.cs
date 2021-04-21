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
        private MemoryLogger _memoryLogger;

        public SpectorPipeline(Trox trox)
        {
            _trox = trox;
            _xtrax = trox.XtraxChain;
            _checker = trox.Checker;
            _notifier = trox.NotifyChainStandard;
            _memoryLogger = new MemoryLogger();
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
                _memoryLogger.Start();
                IDataTruck data = null;
                var file = GenericDownloader.DownloadToLocalFile(_trox);
                if (!file.GrabSuccess)
                {
                    string msg = "Error in Download: Aborting processing";
                    _log.Error(msg);
                    file.ErrorMessage.AppendLine(msg);
                }
                else
                {
                    data = _xtrax.GetOutputChained(file.Truck);
                    if (data.GetText() == XtraxConst.NOT_FOUND)
                    {
                        var xtraxError = _xtrax.GetErrorChained();
                        file.ErrorMessage.Append(xtraxError);
                        string msg = $"Data extraction failed for item: '{Name}'";
                        _log.Error(msg);
                        file.ErrorMessage.AppendLine(msg);
                        file.XtraxSuccess = false;
                    }
                    else
                    {
                        file.XtraxSuccess = true;
                    }
                    _log.Debug($"Extraction of '{Name}' = " + data.GetText());
                    file.IsSignaled = _checker.Check(data);
                }
                if (file.IsSignaled)
                {
                    _notifier.NotifyChained("Pipeline triggered alert");
                }

                _log.Debug("this is dumped in memory");
                var memoryLog = _memoryLogger.Stop();
                _log.Debug("no more in memory");
                ResultStorage result = new ResultStorage(_trox.DbId, data, file, memoryLog);
                ServiceLocator.Instance.Repo.BeginTransaction();
                ServiceLocator.Instance.Repo.SaveResult(result);
                ServiceLocator.Instance.Repo.Commit();
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