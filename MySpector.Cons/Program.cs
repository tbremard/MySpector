using System;
using NLog;
using MySpector.Core;

namespace MySpector.Cons
{
    class Program
    {
        static Logger _log = LogManager.GetCurrentClassLogger();

        static void Main()
        {
            _log.Debug("Starting MySpector...");
//            var watchList = WatchList.CreateLocal(); // <<<<<<<<< OFFLINE MODE
//            WatchList.SaveWatchList(watchList);                // <<<<<<<<<< used once to initiate db with content of OFFLINE mode
            var watchList = WatchList.LoadFromDB();    // <<<<<<<<< CONNECTED mode
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
