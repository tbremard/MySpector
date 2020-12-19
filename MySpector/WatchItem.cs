using System.Linq;

namespace MySpector.Core
{
    public class WatchItem
    {
        public bool Enabled = true;
        public string Name { get; set; }
        public string Url { get; set; }
        public XtraxRule XtraxChain { get; }
        public IChecker Checker { get; }
        public INotifier NotifyChain { get; }
        public string FileToken
        {
            get
            {
                var alphas = Name.Where(x => char.IsLetterOrDigit(x)).ToList();
                string ret = new string(alphas.ToArray());
                return ret;
            }
        }

        public WatchItem(XtraxRule xtraxChain, IChecker checker, INotifier notifyChain)
        {
            XtraxChain = xtraxChain;
            Checker = checker;
            NotifyChain = notifyChain;
        }
    }
}
