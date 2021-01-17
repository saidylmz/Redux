using Redux.CORE.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redux.MODEL.Entities
{
    public class Category:BaseEntity
    {
        public Category()
        {
            Mods = new HashSet<Mod>();
        }
        public string Name { get; set; }
        public string Image { get; set; }

        public virtual ICollection<Mod> Mods { get; set; }
    }
}
