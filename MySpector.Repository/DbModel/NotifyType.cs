using System;
using System.Collections.Generic;

namespace MySpector.Repo.DbModel
{
    public partial class NotifyType
    {
        public NotifyType()
        {
            NotifyDef = new HashSet<NotifyDef>();
        }

        public int IdNotifyType { get; set; }
        public string Name { get; set; }

        public virtual ICollection<NotifyDef> NotifyDef { get; set; }
    }
}
