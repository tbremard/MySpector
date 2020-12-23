using System.Linq;

namespace MySpector.Core
{
    public class WatchItem
    {
        public bool Enabled { get; } = true;
        public string Name { get; }
        public HttpTarget Target { get; }
        public Xtrax XtraxChain { get; }
        public IChecker Checker { get; }
        public Notify NotifyChain { get; }
        public string FileToken
        {
            get
            {
                var alphas = Name.Where(x => char.IsLetterOrDigit(x)).ToList();
                string ret = new string(alphas.ToArray());
                return ret;
            }
        }

        public WatchItem(string name, HttpTarget target, bool enabled, Xtrax xtraxChain, IChecker checker, Notify notifyChain)
        {
            Name = name;
            Target = target;
            Enabled = enabled;
            XtraxChain = xtraxChain;
            Checker = checker;
            NotifyChain = notifyChain;
        }
    }
}
