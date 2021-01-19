﻿using NLog;

namespace MySpector.Objects
{
    public class WebNotifier : Notifier
    {
        //        static Logger _log = LogManager.GetCurrentClassLogger();


        protected override bool NotifySingle(string message)
        {
            _log.Debug("Notification: " + message);
            return false;
        }

    }

}
