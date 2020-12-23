﻿using System.Linq;

namespace MySpector.Core
{
    public class WatchItem
    {
        public bool Enabled { get; } = true;
        public string Name { get; }
        public string Url { get; }
        public XtraxRule XtraxChain { get; }
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

        public HttpTarget Target { get; internal set; }

        public WatchItem(string name, HttpTarget target, bool enabled, XtraxRule xtraxChain, IChecker checker, Notify notifyChain)
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
