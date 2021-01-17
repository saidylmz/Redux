using Redux.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Redux.DAL.Concrete
{
    public class MyStrategy : DropCreateDatabaseIfModelChanges<ReduxDbContext>
    {
        protected override void Seed(ReduxDbContext context)
        {
            User user1 = context.Users.Add(new MODEL.Entities.User
            {
                UserName = "Admin",
                Password = "QWERTY",
                Email = "ylmazsaid@gmail.com",
                IsActive = true,
                IsAdmin = true
            });
            #region DEMO
            User user2 = context.Users.Add(new MODEL.Entities.User
            {
                UserName = "Saidylmz",
                Password = "159951",
                Email = "said_ylmz96@hotmail.com",
                IsActive = true,
                IsAdmin = false,
                WebSite = "www.theekip.com",
                Steam = "zQrba"
            });
            context.SaveChanges();
            Category cat1 = context.Categories.Add(new Category()
            {
                Name = "Arabalar",
                Image = "../Files/Category/1_Arabalar.png"
            });
            context.Categories.Add(new Category()
            {
                Name = "Scriptler",
                Image = "../Files/Category/2_Scriptler.png"
            });
            Category cat2 = context.Categories.Add(new Category()
            {
                Name = "Araçlar",
                Image = "../Files/Category/3_Araçlar.png"
            });
            context.SaveChanges();
            context.Mods.Add(new Mod
            {
                Name = "Seat Ibiza Cupra [Eklenti - Değişim]",
                Version = "0.6 Beta",
                Description = "<b> Lorem </b> Lorem ipsum dolor sit amet, consectetur adipiscing elit.Phasellus elementum risus at pulvinar dapibus.Vivamus viverra arcu eu urna facilisis tempor.Donec sed nulla eu risus mattis auctor.Nullam dictum interdum luctus.Suspendisse scelerisque lacinia gravida.Nullam sit amet congue sapien, in viverra risus.Vestibulum hendrerit, est ac fermentum luctus, tortor orci hendrerit libero, non mollis lectus augue et eros.",
                Download = 122,
                File = "../Files/arabalar-seat-ibiza-cupra-eklenti-degisim-1/1_Seat İbiza Cupra 0.5.rar",
                UserID = user1.ID,
                CategoryID = cat1.ID,
                IsActive = true,
                IsConfirm = true,
                IsFeatured = true
            });
            context.Mods.Add(new Mod
            {
                Name = "Kolay Dosya Düzenleyici",
                Version = "1.0",
                Description = "<b> Lorem </b> Lorem ipsum dolor sit amet, consectetur adipiscing elit.Phasellus elementum risus at pulvinar dapibus.Vivamus viverra arcu eu urna facilisis tempor.Donec sed nulla eu risus mattis auctor.Nullam dictum interdum luctus.Suspendisse scelerisque lacinia gravida.Nullam sit amet congue sapien, in viverra risus.Vestibulum hendrerit, est ac fermentum luctus, tortor orci hendrerit libero, non mollis lectus augue et eros.",
                Download = 23,
                File = "../Files/araclar-kolay-dosya-duzenleyici-2/2_Kolay Dosya Düzenleyici.rar",
                UserID = user2.ID,
                CategoryID = cat2.ID,
                IsActive = true,
                IsConfirm = true,
                IsFeatured = true
            }); 
            context.SaveChanges();
            context.Medias.Add(new Media{IsPhoto = true,ModID = 1,URL = "../Files/arabalar-seat-ibiza-cupra-eklenti-degisim-1/Images/1_tmdnvwow.Jpeg" });
            context.Medias.Add(new Media{IsPhoto = true,ModID = 1,URL = "../Files/arabalar-seat-ibiza-cupra-eklenti-degisim-1/Images/1_bz9122.Jpeg" });
            context.Medias.Add(new Media{IsPhoto = true,ModID = 1,URL = "../Files/arabalar-seat-ibiza-cupra-eklenti-degisim-1/Images/1_81d3dsqlf.Jpeg" });
            context.Medias.Add(new Media{IsPhoto = true,ModID = 1,URL = "../Files/arabalar-seat-ibiza-cupra-eklenti-degisim-1/Images/1_31oevrleb.Jpeg" });
            context.Medias.Add(new Media{IsPhoto = true,ModID = 1,URL = "../Files/arabalar-seat-ibiza-cupra-eklenti-degisim-1/Images/1_ol98g7q.Jpeg" });
            context.Medias.Add(new Media{IsPhoto = true,ModID = 1,URL = "../Files/arabalar-seat-ibiza-cupra-eklenti-degisim-1/Images/1_h65ıfl.Jpeg" });

            context.Medias.Add(new Media{IsPhoto = true,ModID = 2,URL = "../Files/araclar-kolay-dosya-duzenleyici-2/Images/2_1g8ırh4n2.Jpeg" });
            context.Medias.Add(new Media{IsPhoto = true,ModID = 2,URL = "../Files/araclar-kolay-dosya-duzenleyici-2/Images/2_5l162a1ba.Jpeg" });

            context.Comments.Add(new Comment { ModID = 1, UserID = user2.ID, Content = "Teşekkürler" });
            context.Comments.Add(new Comment { ModID = 1, UserID = user1.ID, Content = "Yorumlarınızı bekliyorum" });
            context.Comments.Add(new Comment { ModID = 2, UserID = user2.ID, Content = "Yorumlarınızı bekliyorum" });
            context.Comments.Add(new Comment { ModID = 2, UserID = user1.ID, Content = "Teşekkürler" });


        }
        #endregion
    }
}
