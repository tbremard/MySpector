using System;
using System.Collections.Generic;

namespace MySpector.Repo.Model
{
    public partial class WebTargetSql
    {
        public int IdWebTarget { get; set; }
        public string ConnectionString { get; set; }
        public string Query { get; set; }

        public virtual WebTarget IdWebTargetNavigation { get; set; }
    }
}
