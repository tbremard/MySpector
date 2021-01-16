using System;
using System.Collections.Generic;

namespace MySpector.Repo.Model
{
    public partial class TroxClosure
    {
        public int? IdParent { get; set; }
        public int? IdChild { get; set; }

        public virtual Trox IdChildNavigation { get; set; }
        public virtual Trox IdParentNavigation { get; set; }
    }
}
