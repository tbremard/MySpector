using System;
using System.Collections.Generic;

namespace MySpector.Repo.Model
{
    public partial class CheckerDef
    {
        public int IdCheckerDef { get; set; }
        public int? IdTrox { get; set; }
        public int? Order { get; set; }
        public int? IdCheckerType { get; set; }
        public string Arg { get; set; }

        public virtual CheckerType IdCheckerTypeNavigation { get; set; }
        public virtual Trox IdTroxNavigation { get; set; }
    }
}
