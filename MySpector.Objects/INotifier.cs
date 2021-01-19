using NLog;
namespace MySpector.Objects
{
    public abstract class Notifier
    {
        protected Logger _log = LogManager.GetCurrentClassLogger();
        protected abstract bool NotifySingle(string message);
        protected Notifier Next;    // pointer to next action to perform
                                  // if null the current element is the last element of chain
        /// <summary>
        /// set new action at end of chain
        /// </summary>
        public void SetNext(Notifier next)
        {
            if (Next == null)
            {
                Next = next;
            }
            else
            {
                Next.SetNext(next); // put at the end of chain
            }
        }

        public bool NotifyChained(string message)
        {
            bool ret = NotifySingle(message);
            if (Next != null)
            {
                ret = Next.NotifyChained(message);
            }
            return ret;
        }
    }
}