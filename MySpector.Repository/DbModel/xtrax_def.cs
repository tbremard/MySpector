using System;
using System.Collections.Generic;

namespace MySpector.Repo.DbModel
{
    public partial class xtrax_def
    {
        public int ID_XTRAX_DEF { get; set; }
        public int ID_TROX { get; set; }
        public int? ORDER { get; set; }
        public int ID_XTRAX_TYPE { get; set; }
        public string ARG { get; set; }

        public virtual trox ID_TROXNavigation { get; set; }
    }
}
