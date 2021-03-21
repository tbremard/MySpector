using System.Linq;
using System.Text;

namespace MySpector.Objects
{
    public class Trox
    {
        public IGrabber Downloader { get; set; }
        public bool Enabled { get; } = true;
        public string Name { get; }
        public IGrabTarget Target { get; }
        public Xtrax XtraxChain { get; }
        public IChecker Checker { get; }
        public Notifier NotifyChain { get; }
        public string FileToken
        {
            get
            {
                var alphas = Name.Where(x => char.IsLetterOrDigit(x)).ToList();
                string ret = new string(alphas.ToArray());
                return ret;
            }
        }

        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.AppendLine( $"Trox: Name: {Name}, enabled:{Enabled}");
            sb.AppendLine("WebTarget: " + Target?.ToString());
            sb.AppendLine("Xtrax:" + XtraxChain?.ToString());
            sb.AppendLine("Checker:" + Checker?.ToString());
            sb.AppendLine("Notify:" + NotifyChain?.ToString());
            return sb.ToString();
        }

        public Trox(string name, bool enabled, IGrabTarget target, Xtrax xtraxChain, IChecker checker, Notifier notifyChain)
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
