using System;

namespace MySpector
{
    public class ExecutionPipeline
    {
        private IDataTruck _data;
        private XtraxRule _rule;
        private ITransformer _transformer;
        private IChecker _checker;
        private INotifier _notifier;

        public ExecutionPipeline(IDataTruck data, XtraxRule rule, ITransformer transformer, IChecker checker, INotifier notifier)
        {
            _data = data;
            _rule = rule;
            _transformer = transformer;
            _checker = checker;
            _notifier = notifier;
        }

        public bool Process()
        {
            bool ret;
            try
            {
                var data = _rule.GetOutputChained(_data);
                var dataNumber = _transformer.Transform(data);
                bool isSignaled = _checker.Check(dataNumber);
                if (isSignaled)
                {
                    _notifier.Notify("Pipeline triggered alert");
                }
                ret = true;
            }
            catch
            {
                ret = false;
            }
            return ret;
        }
    }
}