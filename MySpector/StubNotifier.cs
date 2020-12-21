namespace MySpector
{
    public class StubNotifier: Notify
    {

        protected override bool NotifySingle(string message)
        {
            _log.Debug("Notification: " + message);
            return true;
        }
    }
}
