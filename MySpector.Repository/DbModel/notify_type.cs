using System;
using System.Collections.Generic;

namespace MySpector.Repo.DbModel
{
    public partial class notify_type
    {
        public notify_type()
        {
            notify_def = new HashSet<notify_def>();
        }

        public int ID_NOTIFY_TYPE { get; set; }
        public string NAME { get; set; }

        public virtual ICollection<notify_def> notify_def { get; set; }
    }
}
