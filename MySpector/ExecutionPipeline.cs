﻿using System;

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
            bool ret;
            try
            {
                var trox = new Trox();
                var data = trox.ExtractData(_rump, _rule);
                var dataNumber = trox.TransformTextToNumber(data);
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