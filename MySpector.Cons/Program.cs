﻿using System;
using MySpector.Core;
using NLog;

namespace MySpector.Cons
{
    class Program
    {
        static Logger _log = LogManager.GetCurrentClassLogger();

        static void Main()
        {
            _log.Debug("Starting MySpector...");
            var watchList = WatchList.Create();
            foreach (var item in watchList)
            {
                Process(item);
            }
            Console.ReadKey();
        }

        private static void Process(WatchItem item)
        {
            _log.Debug($"--------------------");
            _log.Debug($"Process: {item.Name}");
            if (!item.Enabled)
            {
                _log.Debug($"{item.Name} is disabled");
                return;
            }
            string filePath = GenericDownloader.DownloadToLocalFile(item);
            if (filePath == null)
            {
                _log.Error("Error in Download: Aborting processing");
                return;
            }
            var truck = DataTruck.CreateTextFromFile(filePath);
            if (truck == null)
            {
                _log.Error("Cannot load data");
                return;
            }
            var sut = new SpectorPipeline(item.Name, truck, item.XtraxChain, item.Checker, item.NotifyChain);
            bool isOk = sut.Process();
            _log.Debug($"isOk: {isOk}");
        }
    }
}
