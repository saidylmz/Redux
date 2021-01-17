using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redux.MODEL.Entities
{
    public class Like
    {
        public int ModID { get; set; }
        public int UserID { get; set; }
        public virtual Mod Mod { get; set; }
        public virtual User User { get; set; }
    }
}
