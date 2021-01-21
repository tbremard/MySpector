using System;
using System.Collections.Generic;

namespace MySpector.Repo.DbModel
{
    public partial class web_target_type
    {
        public web_target_type()
        {
            web_target = new HashSet<web_target>();
        }

        public int ID_WEB_TARGET_TYPE { get; set; }
        public string NAME { get; set; }

        public virtual ICollection<web_target> web_target { get; set; }
    }
}
