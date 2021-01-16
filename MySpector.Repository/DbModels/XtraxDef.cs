using System;
using System.Collections.Generic;

namespace MySpector.Repo.Model
{
    public partial class XtraxDef
    {
        public int IdXtraxDef { get; set; }
        public int? IdTrox { get; set; }
        public int? Order { get; set; }
        public int? IdXtraxType { get; set; }
        public string Arg { get; set; }

        public virtual Trox IdTroxNavigation { get; set; }
        public virtual XtraxType IdXtraxTypeNavigation { get; set; }
    }
}
