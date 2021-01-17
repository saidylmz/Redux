using Redux.CORE.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redux.MODEL.Entities
{
    public class Media:BaseEntity
    {
        public bool IsPhoto { get; set; }
        public string URL { get; set; }
        public int ModID { get; set; }
        public virtual Mod Mod { get; set; }
    }
}
