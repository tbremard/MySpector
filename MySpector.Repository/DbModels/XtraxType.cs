using System;
using System.Collections.Generic;

namespace MySpector.Repo.Model
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
