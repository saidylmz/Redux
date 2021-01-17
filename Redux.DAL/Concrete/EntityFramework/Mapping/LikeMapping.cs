using Redux.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redux.DAL.Concrete.EntityFramework.Mapping
{
    public class LikeMapping : EntityTypeConfiguration<Like>
    {
        public LikeMapping()
        {
            HasKey(x => new { x.ModID, x.UserID });
            HasRequired(x => x.User).WithMany().WillCascadeOnDelete(false);
        }
    }
}
