using System;
using System.Collections.Generic;

namespace MySpector.Repo.DbModel
{
    public partial class XtraxType
    {
        public XtraxType()
        {
            XtraxDef = new HashSet<XtraxDef>();
        }

        public int IdXtraxType { get; set; }
        public string Name { get; set; }

        public virtual ICollection<XtraxDef> XtraxDef { get; set; }
    }
}
