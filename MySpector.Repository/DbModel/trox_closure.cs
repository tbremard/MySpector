using System;
using System.Collections.Generic;

namespace MySpector.Repo.DbModel
{
    public partial class trox_closure
    {
        public int? ID_PARENT { get; set; }
        public int? ID_CHILD { get; set; }

        public virtual trox ID_CHILDNavigation { get; set; }
        public virtual trox ID_PARENTNavigation { get; set; }
    }
}
