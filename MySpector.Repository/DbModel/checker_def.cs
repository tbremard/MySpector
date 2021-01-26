using System;
using System.Collections.Generic;

namespace MySpector.Repo.DbModel
{
    public partial class checker_def
    {
        public int ID_CHECKER_DEF { get; set; }
        public int ID_TROX { get; set; }
        public int? ORDER { get; set; }
        public int ID_CHECKER_TYPE { get; set; }
        public string ARG { get; set; }

        public virtual trox ID_TROXNavigation { get; set; }
    }
}
