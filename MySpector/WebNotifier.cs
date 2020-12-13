using NLog;

namespace MySpector
{
    public class WebNotifier : INotifier
    {
        static Logger _log = LogManager.GetCurrentClassLogger();
        public void Notify(string message)
        {
            _log.Debug("Notification: " + message);
        }
    }

}
