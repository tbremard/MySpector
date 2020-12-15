using NLog;

namespace MySpector
{
    public class WebCallbackNotifier : INotifier
    {
        static Logger _log = LogManager.GetCurrentClassLogger();

        public bool Notify(string msg)
        {
            _log.Debug($"Preparing to callback with message: '{msg}'");
            return true;
        }

    }
}
