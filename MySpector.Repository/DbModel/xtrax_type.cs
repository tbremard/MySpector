using System;
using System.Collections.Generic;

namespace MySpector.Repo.DbModel
{
    public partial class xtrax_type
    {
        public xtrax_type()
        {
            xtrax_def = new HashSet<xtrax_def>();
        }

        public int ID_XTRAX_TYPE { get; set; }
        public string NAME { get; set; }

        public virtual ICollection<xtrax_def> xtrax_def { get; set; }
    }
}
