using System;
using System.Collections.Generic;

namespace MySpector.Repo.DbModel
{
    public partial class checker_type
    {
        public checker_type()
        {
            checker_def = new HashSet<checker_def>();
        }

        public int ID_CHECKER_TYPE { get; set; }
        public string NAME { get; set; }

        public virtual ICollection<checker_def> checker_def { get; set; }
    }
}
