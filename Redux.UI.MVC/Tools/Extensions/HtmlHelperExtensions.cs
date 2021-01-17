using Redux.BLL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Redux.UI.MVC.Tools.Extensions
{
    public static class HtmlHelperExtensions
    {
        public static bool UserIsBanned<T>(this HtmlHelper<T> helper, int id)
        {
            IUserService userService = DependencyResolver.Current.GetService<IUserService>();
            return userService.Get(id) != null && !userService.Get(id).IsActive;
        }

        public static bool UserIsAdmin<T>(this HtmlHelper<T> helper, string name)
        {
            IUserService userService = DependencyResolver.Current.GetService<IUserService>();
            var user = userService.GetUserByName(name);
            return user != null && user.IsAdmin;
        }

        public static string QueryOutput<T>(this HtmlHelper<T> helper, bool query, string trueout, string falseout = "")
        {
            return query ? trueout : falseout;
        }
    }
}