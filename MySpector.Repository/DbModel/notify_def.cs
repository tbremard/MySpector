using System;
using System.Collections.Generic;

namespace MySpector.Repo.DbModel
{
    public partial class notify_def
    {
        public int ID_NOTIFY_DEF { get; set; }
        public int? ID_TROX { get; set; }
        public int? ORDER { get; set; }
        public int? ID_NOTIFY_TYPE { get; set; }
        public string ARG { get; set; }

        public virtual notify_type ID_NOTIFY_TYPENavigation { get; set; }
        public virtual trox ID_TROXNavigation { get; set; }
    }
}
