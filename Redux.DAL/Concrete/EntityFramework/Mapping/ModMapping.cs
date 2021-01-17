using Redux.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redux.DAL.Concrete.EntityFramework.Mapping
{
    public class ModMapping : EntityTypeConfiguration<Mod>
    {
        public ModMapping()
        {
            Property(x => x.Name).HasMaxLength(128).IsRequired();
            Property(x => x.Version).HasMaxLength(32).IsOptional();
            Property(x => x.Description).HasColumnName("text");
            Property(x => x.File).HasMaxLength(128);
        }
    }
}
