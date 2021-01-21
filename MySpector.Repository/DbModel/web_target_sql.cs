using System;
using System.Collections.Generic;

namespace MySpector.Repo.DbModel
{
    public partial class web_target_sql
    {
        public int ID_WEB_TARGET { get; set; }
        public string CONNECTION_STRING { get; set; }
        public string QUERY { get; set; }

        public virtual web_target ID_WEB_TARGETNavigation { get; set; }
    }
}
