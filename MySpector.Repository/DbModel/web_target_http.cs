using System;
using System.Collections.Generic;

namespace MySpector.Repo.DbModel
{
    public partial class web_target_http
    {
        public int ID_WEB_TARGET { get; set; }
        public string METHOD { get; set; }
        public string URI { get; set; }
        public string VERSION { get; set; }
        public string HEADERS { get; set; }
        public string CONTENT { get; set; }

        public virtual web_target ID_WEB_TARGETNavigation { get; set; }
    }
}
