using NLog;

namespace MySpector
{
    public class SpectorPipeline
    {
        static Logger _log = LogManager.GetCurrentClassLogger();
        public string Name;
        private IDataTruck _data;
        private XtraxRule _rule;
        private ITransformer _transformer;
        private IChecker _checker;
        private INotifier _notifier;

        public SpectorPipeline(IDataTruck data, XtraxRule rule, ITransformer transformer, IChecker checker, INotifier notifier)
        {
            _data = data;
            _rule = rule;
            _transformer = transformer;
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
                    return false;
                _log.Debug(Name + ": " + data.GetText());
                var dataNumber = _transformer.Transform(data);
                bool isSignaled = _checker.Check(dataNumber);
                if (isSignaled)
                {
                    _notifier.Notify("Pipeline triggered alert");
                    ret = true;
                }
            }
            catch
            {
                ret = false;
            }
            return ret;
        }
    }
}