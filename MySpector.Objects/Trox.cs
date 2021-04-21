using System.Linq;
using System.Text;

namespace MySpector.Objects
{
    public class Trox
    {
        public IGrabber Grabber { get; set; }
        public bool Enabled { get; } = true;
        public string Name { get; }
        public IGrabTarget Target { get; }
        public Xtrax XtraxChain { get; }
        public IChecker Checker { get; }
        public Notifier NotifyChainStandard { get; }
        public Notifier NotifyChainError { get; }
        public int? DbId { get; set; }

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
            sb.AppendLine( $"Trox.DbId: {DbId}");
            sb.AppendLine( $"Trox.Name: {Name}");
            sb.AppendLine( $"Trox.Enabled:{Enabled}");
            sb.AppendLine("WebTarget: " + Target?.ToString());
            sb.AppendLine("Xtrax:" + XtraxChain?.ToString());
            sb.AppendLine("Checker:" + Checker?.ToString());
            sb.AppendLine("Notify:" + NotifyChainStandard?.ToString());
            return sb.ToString();
        }

        public Trox(string name, bool enabled, IGrabTarget target, Xtrax xtraxChain, IChecker checker, Notifier notifyChainStandard, Notifier notifyChainError)
        {
            Name = name;
            Target = target;
            Enabled = enabled;
            XtraxChain = xtraxChain;
            Checker = checker;
            NotifyChainStandard = notifyChainStandard;
            NotifyChainError = notifyChainError;
            Grabber = new HttpGrabber();// use instead ServiceLocator.Instance.CreateDownloader(target.Type)
        }
    }
}
