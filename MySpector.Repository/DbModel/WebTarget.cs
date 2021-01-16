using System;
using System.Collections.Generic;

namespace MySpector.Repo.DbModel
{
    public partial class WebTarget
    {
        public int IdWebTarget { get; set; }
        public int? IdTrox { get; set; }
        public int? IdWebTargetType { get; set; }

        public virtual Trox IdTroxNavigation { get; set; }
        public virtual WebTargetType IdWebTargetTypeNavigation { get; set; }
        public virtual WebTargetHttp WebTargetHttp { get; set; }
        public virtual WebTargetSql WebTargetSql { get; set; }
    }
}
