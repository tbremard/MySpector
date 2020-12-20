namespace MySpector.Cons
{
    internal class NotifyFactory
    {
        internal static INotifier CreateChain()
        {
            var ret = new StubNotifier();
            return ret;
        }
    }
}