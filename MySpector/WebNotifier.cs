﻿using NLog;

namespace MySpector
{
    public class WebNotifier : Notify
    {
//        static Logger _log = LogManager.GetCurrentClassLogger();


        protected override bool NotifySingle(string message)
        {
            _log.Debug("Notification: " + message);
            return false;
        }

    }

}
