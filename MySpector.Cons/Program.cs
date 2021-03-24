using System;
using NLog;
using MySpector.Core;
using System.Collections.Generic;
using MySpector.Objects;

namespace MySpector.Cons
{
    class Program
    {
        static Logger _log = LogManager.GetCurrentClassLogger();

        static void Main()
        {
            IList<Trox> watchList;
            _log.Debug("Starting MySpector...");
            bool offline = false;
            if(offline)
            {
                watchList = WatchList.CreateLocal(); // <<<<<<<<< OFFLINE MODE
                WatchList.SaveWatchList(watchList);                // <<<<<<<<<< used once to initiate db with content of OFFLINE mode
            }
            else
            {
                watchList = WatchList.LoadFromDB();    // <<<<<<<<< CONNECTED mode
            }
            //return;
            var job = new Job();
            bool isSuccess = job.Process(watchList);
            if (!isSuccess)
            {
                _log.Error($"isSuccess: {isSuccess}: at least one error occured");
            }
            _log.Debug("Press a key...");
            Console.ReadKey();
        }
    }
}
