using NLog;
using System;

namespace MySpector
{
    public class SpectorPipeline
    {
        static Logger _log = LogManager.GetCurrentClassLogger();
        public string Name{ get; private set; }
        private IDataTruck _data;
        private XtraxRule _rule;
        private IChecker _checker;
        private INotifier _notifier;

        public SpectorPipeline(string name, IDataTruck data, XtraxRule rule, IChecker checker, INotifier notifier)
        {
            Name = name;
            _data = data;
            _rule = rule;
            _checker = checker;
            _notifier = notifier;
        }

        public bool Process()
        {
            bool ret = false;
            try
            {
                var data = _rule.GetOutputChained(_data);
                if (data.GetText() == XtraxRuleConst.NOT_FOUND)
                {
                    _log.Error($"Data extraction failed for item: '{Name}'");
                    return false;
                }
                _log.Debug($"Extraction of '{Name}' = " + data.GetText());
                bool isSignaled = _checker.Check(data);
                if (isSignaled)
                {
                    _notifier.Notify("Pipeline triggered alert");
                    ret = true;
                }
            }
            catch(Exception ex)
            {
                _log.Error($"Data extraction failed for item: '{Name}'");
                _log.Error(ex);
                ret = false;
            }
            return ret;
        }
    }
}