using System;
using System.Collections.Generic;

namespace MySpector.Repo.DbModel
{
    public partial class trox
    {
        public trox()
        {
            checker_def = new HashSet<checker_def>();
            notify_def = new HashSet<notify_def>();
            xtrax_def = new HashSet<xtrax_def>();
        }

        public int ID_TROX { get; set; }
        public string NAME { get; set; }
        public byte? ENABLED { get; set; }
        public byte? IS_DIRECTORY { get; set; }
        public int? ID_WEB_TARGET { get; set; }

        public virtual web_target ID_WEB_TARGETNavigation { get; set; }
        public virtual ICollection<checker_def> checker_def { get; set; }
        public virtual ICollection<notify_def> notify_def { get; set; }
        public virtual ICollection<xtrax_def> xtrax_def { get; set; }
    }
}
