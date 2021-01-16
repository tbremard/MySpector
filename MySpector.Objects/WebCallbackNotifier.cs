using NLog;

namespace MySpector.Objects
{
    public class WebCallbackNotifier : Notify
    {
        //        static Logger _log = LogManager.GetCurrentClassLogger();

        protected override bool NotifySingle(string message)
        {
            _log.Debug($"Preparing to callback with message: '{message}'");
            return false;
        }

    }
}
