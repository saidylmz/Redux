using Redux.CORE.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redux.MODEL.Entities
{
    public class Mod:BaseEntity
    {
        public Mod()
        {
            Likes = new HashSet<Like>();
            Comments = new HashSet<Comment>();
            Medias = new HashSet<Media>();
            IsActive = true;
            IsConfirm = false;
            IsFeatured = false;
        }
        public string Name { get; set; }
        public string Version { get; set; }
        public string Description { get; set; }
        public int Download { get; set; }
        public string File { get; set; }
        public int UserID { get; set; }
        public int CategoryID { get; set; }
        public bool IsActive { get; set; }
        public bool IsConfirm { get; set; }
        public bool IsFeatured { get; set; }

        public virtual User User { get; set; }
        public virtual Category Category { get; set; }
        public virtual ICollection<Like> Likes { get; set; }
        public virtual ICollection<Comment> Comments { get; set; }
        public virtual ICollection<Media> Medias { get; set; }
    }
}
