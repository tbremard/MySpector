using System;
using System.Collections.Generic;

namespace MySpector.Repo.DbModel
{
    public partial class target_http
    {
        public int ID_TARGET { get; set; }
        public string METHOD { get; set; }
        public string URI { get; set; }
        public string VERSION { get; set; }
        public string HEADERS { get; set; }
        public string CONTENT { get; set; }

        public virtual target ID_TARGETNavigation { get; set; }
    }
}
