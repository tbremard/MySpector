using System;
using System.Collections.Generic;

namespace MySpector.Repo.DbModel
{
    public partial class target
    {
        public target()
        {
            trox = new HashSet<trox>();
        }

        public int ID_TARGET { get; set; }
        public int ID_TARGET_TYPE { get; set; }

        public virtual target_http target_http { get; set; }
        public virtual target_sql target_sql { get; set; }
        public virtual ICollection<trox> trox { get; set; }
    }
}
