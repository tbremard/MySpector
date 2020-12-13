using System.Linq;

namespace MySpector.Core
{
    public class WatchItem
    {
        public string Name;
        public string Url;
        public bool Enabled = true;
        public XtraxRule XtraxChain;
        public CheckerParam CheckerParam { get; set; }
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
