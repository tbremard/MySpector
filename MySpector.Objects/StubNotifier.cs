namespace MySpector.Objects
{
    public class StubNotifier : Notifier
    {
        protected override bool NotifySingle(string message)
        {
            _log.Debug("Notification: " + message);
            return true;
        }
    }
}
