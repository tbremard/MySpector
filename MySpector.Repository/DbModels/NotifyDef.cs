using System;
using System.Collections.Generic;

namespace MySpector.Repo.Model
{
    public partial class NotifyDef
    {
        public int IdNotifyDef { get; set; }
        public int? IdTrox { get; set; }
        public int? Order { get; set; }
        public int? IdNotifyType { get; set; }
        public string Arg { get; set; }

        public virtual NotifyType IdNotifyTypeNavigation { get; set; }
        public virtual Trox IdTroxNavigation { get; set; }
    }
}
