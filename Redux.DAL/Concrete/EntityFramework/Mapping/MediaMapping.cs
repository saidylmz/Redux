using Redux.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redux.DAL.Concrete.EntityFramework.Mapping
{
    public class MediaMapping: EntityTypeConfiguration<Media>
    {
        public MediaMapping()
        {
            Property(x => x.ModID).IsRequired();
            Property(x => x.IsPhoto).IsRequired();
            Property(x => x.URL).HasMaxLength(128);
        }
    }
}
