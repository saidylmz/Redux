using Redux.BLL.Abstract;
using Redux.MODEL.Entities;
using Redux.UI.MVC.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Redux.UI.MVC.Controllers
{
    [RoleControl(Roles = "All")]
    public class UserController : Controller
    {
        IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }
        public ActionResult Index(string permalink)
        {
            string[] links = permalink.Split('/');
            User user = userService.GetUserByName(links[0]);

            if(links.Length > 1 && !string.IsNullOrEmpty(links[1]))
            {
                if (links[1].EqualAny("dosyalar", "files") && user.Mods.Count > 0)
                    if (User.Identity.IsAuthenticated && user.UserName == User.Identity.Name)
                        return View("Files", user.Mods.Where(x=>x.IsActive));
                    else
                        return View("Files", user.Mods.Where(x => x.IsConfirm && x.IsActive));
            }
            return View(user);
        }
    }
}