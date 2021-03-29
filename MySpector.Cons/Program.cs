using System;
using NLog;
using MySpector.Core;
using System.Collections.Generic;
using MySpector.Objects;
using NLog.Config;

namespace MySpector.Cons
{
    class Program
    {
        static Logger _log = LogManager.GetCurrentClassLogger();

        static void Main()
        {
            LogManager.ConfigurationChanged += ConfigurationChanged;
            LogManager.ConfigurationReloaded += ConfigurationReloaded;
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
            LogManager.Shutdown();
            _log.Debug("Press a key...");
            Console.ReadKey();
        }

        private static void ConfigurationReloaded(object sender, LoggingConfigurationReloadedEventArgs e)
        {
            _log.Debug("Nlog::ConfigurationReloaded !");
        }

        private static void ConfigurationChanged(object sender, LoggingConfigurationChangedEventArgs e)
        {
            _log.Debug("Nlog::ConfigurationChanged !");
        }
    }
}
