using Redux.BLL.Abstract;
using Redux.MODEL.Entities;
using Redux.UI.MVC.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Redux.UI.MVC.Controllers
{
    [RoleControl(Roles = "User")]
    public class SettingsController : Controller
    {
        IUserService userService;
        public SettingsController(IUserService userService)
        {
            this.userService = userService;
        }
        public ActionResult Index()
        {
            return View(userService.GetUserByName(User.Identity.Name));
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Account(User postedUser, HttpPostedFileBase file)
        {
            User user = userService.Get(postedUser.ID);
            user.Email = postedUser.Email;
            user.Steam = postedUser.Steam;
            user.Instagram = postedUser.Instagram;
            user.WebSite = postedUser.WebSite;
            if (file != null)
            {
                WebImage image = new WebImage(file.InputStream);
                DirectoryInfo di = new DirectoryInfo(Server.MapPath("~/Files/Avatar"));
                if (!di.Exists)
                    di.Create();
                else if(user.Avatar != null)
                {
                    string pt = Path.Combine(di.FullName, user.Avatar.Split('/').Last());
                    if (System.IO.File.Exists(pt))
                        System.IO.File.Delete(pt);
                }
                string filename = user.ID + "_" + file.FileName;
                image.Save(Path.Combine(di.FullName, filename));
                user.Avatar = "../Files/Avatar/" + filename;
            }
            userService.Update(user);
            return Redirect("/Ayarlar");
        }


        public bool Password(string current, string newp)
        {
            User user = userService.GetUserByLogin(User.Identity.Name, current);
            if (user is null)
                return false;
            user.Password = newp;
            userService.Update(user);
            return true;
        }
    }
}