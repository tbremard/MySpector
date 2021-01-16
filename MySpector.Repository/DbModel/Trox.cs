using System;
using System.Collections.Generic;

namespace MySpector.Repo.DbModel
{
    public partial class Trox
    {
        public Trox()
        {
            CheckerDef = new HashSet<CheckerDef>();
            NotifyDef = new HashSet<NotifyDef>();
            WebTarget = new HashSet<WebTarget>();
            XtraxDef = new HashSet<XtraxDef>();
        }

        public int IdTrox { get; set; }
        public string Name { get; set; }
        public byte? Enabled { get; set; }
        public byte? IsDirectory { get; set; }

        public virtual ICollection<CheckerDef> CheckerDef { get; set; }
        public virtual ICollection<NotifyDef> NotifyDef { get; set; }
        public virtual ICollection<WebTarget> WebTarget { get; set; }
        public virtual ICollection<XtraxDef> XtraxDef { get; set; }
    }
}
