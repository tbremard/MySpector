using System;

namespace MySpector
{
    public class ExecutionPipeline
    {
        private IRump _rump;
        private XtraxRule _rule;
        private object _transformer;
        private IChecker _checker;
        private INotifier _notifier;

        public ExecutionPipeline(IRump rump, XtraxRule rule, object transformer, IChecker checker, INotifier notifier)
        {
            this._rump = rump;
            this._rule = rule;
            this._transformer = transformer;
            this._checker = checker;
            this._notifier = notifier;
        }

        public bool Process()
        {
            bool ret = false;
            try
            {
                var trox = new Trox();
                var data = trox.ExtractData(_rump, _rule);
                var number = trox.TransformTextToNumber(data.Value);
                bool isSignaled = _checker.Check();// BUG : should take as input data
                if (isSignaled)
                {
                    _notifier.Notify("Pipeline triggered alert");
                }
                ret = true;
            }
            catch (Exception e)
            {
                ret = false;
            }
            return ret;
        }
    }
}