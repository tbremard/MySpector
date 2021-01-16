using System;
using System.Collections.Generic;

namespace MySpector.Repo.DbModel
{
    public partial class WebTargetType
    {
        public WebTargetType()
        {
            WebTarget = new HashSet<WebTarget>();
        }

        public int IdWebTargetType { get; set; }
        public string Name { get; set; }

        public virtual ICollection<WebTarget> WebTarget { get; set; }
    }
}
