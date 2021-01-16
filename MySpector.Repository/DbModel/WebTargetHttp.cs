using System;
using System.Collections.Generic;

namespace MySpector.Repo.DbModel
{
    public partial class WebTargetHttp
    {
        public int IdWebTarget { get; set; }
        public string Method { get; set; }
        public string Uri { get; set; }
        public string Version { get; set; }
        public string Headers { get; set; }
        public string Content { get; set; }

        public virtual WebTarget IdWebTargetNavigation { get; set; }
    }
}
