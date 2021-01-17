using Redux.BLL.Abstract;
using Redux.MODEL.Entities;
using Redux.UI.MVC.Tools;
using Redux.UI.MVC.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace Redux.UI.MVC.Controllers
{
    public class AccountController : Controller
    {
        readonly IUserService userService;
        public AccountController(IUserService userService)
        {
            this.userService = userService;
        }
        
        [RoleControl(Roles="All")]
        public ActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }
        [HttpPost, RoleControl(Roles = "All")]
        public bool Login(string username, string password, bool rememberme)
        {
            if (ModelState.IsValid)
            {
                User currentUser = userService.GetUserByLogin(username, password);

                if (currentUser != null && currentUser.IsActive)
                {
                    FormsAuthentication.SetAuthCookie(currentUser.UserName, rememberme);
                    return true;
                }
            }
            return false;
        }
        public ActionResult LogOut()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        [RoleControl(Roles="All")]
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost, RoleControl(Roles = "All"), ValidateAntiForgeryToken]
        public ActionResult Register(string username, string password, string email)
        {
            userService.Insert(new User() { UserName = username, Password = password, Email = email });
            return RedirectToAction("Login");
        }

        public bool UsernameCheck(string username)
        {
            return userService.GetAll().Any(x => x.UserName.ToLower() == username.ToLower());
        }
        public bool EMailCheck(string email)
        {
            return userService.GetAll().Any(x => x.Email.ToLower() == email.ToLower());
        }
        public bool RoleControl()
        {
            return User.Identity.IsAuthenticated &&
                userService.GetAll().FirstOrDefault(x => x.UserName.Equals(User.Identity.Name, StringComparison.OrdinalIgnoreCase)).IsAdmin;
        }
        public string GetAvatar()
        {
            return userService.GetAll().FirstOrDefault(x => x.UserName.EqualAny(User.Identity.Name)).AvatarSource();
        }
        public bool ResetPassword(string email)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";

            Random random = new Random();
            string password = new string(Enumerable.Repeat(chars, 6).Select(s => s[random.Next(s.Length)]).ToArray());

            User user = userService.GetUserByEmail(email);
            if (MailExtensions.SendNewPassword(user.UserName, email, password))
            {
                user.Password = password;
                userService.Update(user);
                return true;
            }
            return false;
        }
    }
}