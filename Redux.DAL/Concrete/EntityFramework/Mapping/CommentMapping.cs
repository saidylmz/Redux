using Redux.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redux.DAL.Concrete.EntityFramework.Mapping
{
    public class CommentMapping:EntityTypeConfiguration<Comment>
    {
        public CommentMapping()
        {
            Property(x => x.Content).HasColumnType("text").IsRequired();
            HasRequired(x => x.User).WithMany(x => x.Comments).HasForeignKey(x => x.UserID).WillCascadeOnDelete(false);
        }
    }
}
