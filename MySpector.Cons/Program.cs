using System;
using NLog;
using MySpector.Core;
using System.Collections.Generic;
using MySpector.Objects;
using MySpector.Repo;

namespace MySpector.Cons
{
    class Program
    {
        static Logger _log = LogManager.GetCurrentClassLogger();

        static void Main()
        {
            _log.Debug("Starting MySpector...");
            var watchList = WatchList.CreateLocal();
//            SaveWatchList(watchList);
            var job = new Job();
            bool isSuccess = job.Process(watchList);
            if (!isSuccess)
            {
                _log.Error($"isSuccess: {isSuccess}: at least one error occured");
            }
            _log.Debug("Press a key...");
            Console.ReadKey();
        }

        private static void SaveWatchList(IList<Trox> watchList)
        {
            Repo.Repo repo;
            repo = new Repo.Repo();
            bool isConnected = repo.Connect();
            if(!isConnected)
            {
                _log.Error("cannot connect");
                return;
            }
            repo.BeginTransaction();
            foreach (var trox in watchList)
            {
                repo.SaveTrox(trox);
            }
            repo.Commit();
        }
    }
}
