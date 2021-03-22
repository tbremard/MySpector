﻿using System.Collections.Generic;
using MySpector.Objects;
using NLog;

namespace MySpector.Core
{
    public class Job
    {
        static Logger _log = LogManager.GetCurrentClassLogger();

        public bool Process(Trox trox)
        {
            _log.Debug($"--------------------");
            _log.Debug($"Process: {trox.Name} TroxId: {trox.DbId}");
            var pipeline = new SpectorPipeline(trox);
            bool isDone = pipeline.Process();
            _log.Debug($"isDone: {isDone}");
            return isDone;
        }

        public bool Process(IList<Trox> troxList)
        {
            bool ret = true;
            _log.Debug("############################");
            _log.Debug("Processing " + troxList.Count + " items");
            foreach (var trox in troxList)
            {
                ret &= Process(trox);
            }
            _log.Debug("############################");
            return ret;
        }
    }
}
