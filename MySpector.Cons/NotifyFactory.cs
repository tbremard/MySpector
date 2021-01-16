using MySpector.Objects;

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