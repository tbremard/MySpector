namespace MySpector.Objects
{
    public class WebCallbackNotifier : Notifier
    {
        public override NotifyType Type => NotifyType.WebCallBack;

        protected override bool NotifySingle(string message)
        {
            _log.Debug($"Preparing to callback with message: '{message}'");
            return false;
        }
    }
}
