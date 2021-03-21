namespace MySpector.Objects
{
    public class StubNotifier : Notifier
    {
        public override NotifyType Type => NotifyType.Stub;

        protected override bool NotifySingle(string message)
        {
            _log.Debug("Notification: " + message);
            return true;
        }
    }
}
