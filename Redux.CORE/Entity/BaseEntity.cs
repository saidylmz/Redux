using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redux.CORE.Entity
{
    public abstract class BaseEntity
    {
        public BaseEntity()
        {
            CreatedDate = DateTime.Now;
        }

        public int ID { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
