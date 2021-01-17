using Redux.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redux.DAL.Concrete.EntityFramework.Mapping
{
    public class UserMapping : EntityTypeConfiguration<User>
    {
        public UserMapping()
        {
            Property(x => x.Email).HasMaxLength(64).IsRequired();
            Property(x => x.Instagram).HasMaxLength(32).IsOptional();
            Property(x=>x.Steam).HasMaxLength(64).IsOptional();
            Property(x => x.UserName).HasMaxLength(64).IsRequired();
            Property(x => x.WebSite).HasMaxLength(64).IsOptional();
            Property(x => x.Password).HasMaxLength(32).IsRequired();
            HasIndex(x => x.UserName).IsUnique();
            Property(x => x.Avatar).HasMaxLength(128).IsOptional();
        }
    }
}
