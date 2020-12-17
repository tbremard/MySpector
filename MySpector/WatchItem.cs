using System.Linq;

namespace MySpector.Core
{
    public class WatchItem
    {
        public bool Enabled = true;
        public string Name { get; set; }
        public string Url { get; set; }
        public XtraxRule XtraxChain { get; set; }
        public IChecker Checker => CheckerFactory.Create(CheckerParam);
        public INotifier NotifyChain => new StubNotifier();
        public CheckerParam CheckerParam{ get; set; }
        public string Token
        {
            get
            {
                var alphas = Name.Where(x => char.IsLetterOrDigit(x)).ToList();
                string ret = new string(alphas.ToArray());
                return ret;
            }
        }
    }
}
