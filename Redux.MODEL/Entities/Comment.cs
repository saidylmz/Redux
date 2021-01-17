using Redux.CORE.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redux.MODEL.Entities
{
    public class Comment:BaseEntity
    {
        public int UserID { get; set; }
        public int ModID { get; set; }
        public string Content { get; set; }

        public virtual User User { get; set; }
        public virtual Mod Mod { get; set; }
    }
}
