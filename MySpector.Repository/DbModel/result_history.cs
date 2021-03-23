using System;
using System.Collections.Generic;

namespace MySpector.Repo.DbModel
{
    public partial class result_history
    {
        public int ID_RESULT { get; set; }
        public int ID_TROX { get; set; }
        public int LATENCY_MS { get; set; }
        public string IN_DATA { get; set; }
        public string OUT_TEXT { get; set; }
        public decimal? OUT_NUMBER { get; set; }

        public virtual trox ID_TROXNavigation { get; set; }
    }
}
