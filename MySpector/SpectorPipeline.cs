using NLog;
using System;

namespace MySpector
{
    public class SpectorPipeline
    {
        static Logger _log = LogManager.GetCurrentClassLogger();
        public string Name{ get; private set; }
        private IDataTruck _data;
        private Xtrax _rule;
        private IChecker _checker;
        private Notify _notifier;

        public SpectorPipeline(string name, IDataTruck data, Xtrax rule, IChecker checker, Notify notifier)
        {
            Name = name;
            _data = data;
            _rule = rule;
            _checker = checker;
            _notifier = notifier;
        }

        public bool Process()
        {
            bool ret;
            try
            {
                var data = _rule.GetOutputChained(_data);
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