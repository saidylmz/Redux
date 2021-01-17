using Redux.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redux.DAL.Concrete.EntityFramework.Mapping
{
    public class CategoryMapping : EntityTypeConfiguration<Category>
    {
        public CategoryMapping()
        {
            Property(x => x.Name).HasMaxLength(128).IsRequired();
            Property(x => x.Image).HasMaxLength(128).IsRequired();
        }
    }
}
