using System;
using System.Collections.Generic;

namespace MySpector.Repo.DbModel
{
    public partial class target_sql
    {
        public int ID_TARGET { get; set; }
        public string CONNECTION_STRING { get; set; }
        public string QUERY { get; set; }
        public string PROVIDER { get; set; }

        public virtual target ID_TARGETNavigation { get; set; }
    }
}
