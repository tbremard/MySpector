using MySpector.Models;

namespace MySpector.Cons
{
    internal class NotifyFactory
    {
        internal static Notify CreateChain()
        {
            var ret = new StubNotifier();
            return ret;
        }
    }
}