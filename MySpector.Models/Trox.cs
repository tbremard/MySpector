using System.Linq;

namespace MySpector.Models
{
    public class Trox
    {
        public IDownloader Downloader { get; set; }
        public bool Enabled { get; } = true;
        public string Name { get; }
        public IWebTarget Target { get; }
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

        public Trox(string name, IWebTarget target, bool enabled, Xtrax xtraxChain, IChecker checker, Notify notifyChain)
        {
            Name = name;
            Target = target;
            Enabled = enabled;
            XtraxChain = xtraxChain;
            Checker = checker;
            NotifyChain = notifyChain;
            Downloader = new HttpDownloader();// use instead ServiceLocator.Instance.CreateDownloader(target.Type)
        }
    }
}
