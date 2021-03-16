using System.ComponentModel;

namespace MySpector.Objects
{
    public class NotifyFactory
    {
        public static Notifier CreateChain()
        {
            var ret = new StubNotifier();
            return ret;
        }

        public static Notifier Create(NotifierParam param)
        {
            Notifier ret;
            switch (param.Type)
            {
                case NotifierType.Stub:
                    ret = new StubNotifier();
                    break;
                case NotifierType.Mail:
                    ret = new MailNotifier();
                    break;
                case NotifierType.WebCallBack:
                    ret = new WebCallbackNotifier();
                    break;
                default:
                    throw new InvalidEnumArgumentException(" type is not handled");
            }
            return ret;
        }

    }
}