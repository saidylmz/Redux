using Redux.CORE.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redux.MODEL.Entities
{
    public class User:BaseEntity
    {
        public User()
        {
            Mods = new HashSet<Mod>();
            IsActive = true;
            IsAdmin = false;
        }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public string Steam { get; set; }
        public string WebSite { get; set; }
        public string Instagram { get; set; }
        public bool IsAdmin { get; set; }
        public string Avatar { get; set; }

        public virtual ICollection<Mod> Mods { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        
    }
}
