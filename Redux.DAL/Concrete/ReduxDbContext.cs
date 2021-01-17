using Redux.DAL.Concrete.EntityFramework.Mapping;
using Redux.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redux.DAL.Concrete
{
    public class ReduxDbContext:DbContext
    {
        public ReduxDbContext():base("Server=.; Database=ReduxDb; uid=sa; pwd=123;")
        {
            Database.SetInitializer<ReduxDbContext>(new MyStrategy());
        }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Media> Medias { get; set; }
        public DbSet<Mod> Mods { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Like> Likes { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties().Where(a => a.PropertyType == typeof(DateTime)).Configure(a => a.HasColumnType("datetime2"));
            modelBuilder.Configurations.Add(new CategoryMapping());
            modelBuilder.Configurations.Add(new CommentMapping());
            modelBuilder.Configurations.Add(new MediaMapping());
            modelBuilder.Configurations.Add(new ModMapping());
            modelBuilder.Configurations.Add(new UserMapping());
            modelBuilder.Configurations.Add(new LikeMapping());
        }
    }
}
