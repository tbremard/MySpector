using System;
using System.Collections.Generic;

namespace MySpector.Repo.DbModel
{
    public partial class CheckerType
    {
        public CheckerType()
        {
            CheckerDef = new HashSet<CheckerDef>();
        }

        public int IdCheckerType { get; set; }
        public string Name { get; set; }

        public virtual ICollection<CheckerDef> CheckerDef { get; set; }
    }
}
