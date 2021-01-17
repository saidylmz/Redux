using Redux.BLL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Redux.UI.MVC.Tools
{
    public class RoleControl : AuthorizeAttribute
    {
        IUserService userService = DependencyResolver.Current.GetService<IUserService>();
        protected override bool AuthorizeCore(HttpContextBase httpContext)
        {
            if (Roles.Contains("All")) return true;
            if (!HttpContext.Current.Request.IsAuthenticated)
            {
                httpContext.Response.RedirectToRoute("Home");
                return false;
            }

            var user = userService.GetAll().FirstOrDefault(x => x.UserName.ToLower() == httpContext.User.Identity.Name.ToLower());
            var roles = Roles.Split(',');

            if (user.IsAdmin)
            {
                return true;
            }
            else
            {
                if (roles.Contains("User"))
                    return true;
            }
            return base.AuthorizeCore(httpContext);
        }
    }
}