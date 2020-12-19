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
            var watchList = WatchList.Create();
            var job = new Job();
            bool isSuccess = job.Process(watchList);
            _log.Debug($"isSuccess: {isSuccess}");
            Console.ReadKey();
        }
    }
}
