using System;
using System.Collections.Generic;

namespace MySpector.Repo.DbModel
{
    public partial class web_target
    {
        public web_target()
        {
            trox = new HashSet<trox>();
        }

        public int ID_WEB_TARGET { get; set; }
        public int ID_WEB_TARGET_TYPE { get; set; }

        public virtual web_target_http web_target_http { get; set; }
        public virtual web_target_sql web_target_sql { get; set; }
        public virtual ICollection<trox> trox { get; set; }
    }
}
