using Ninject;
using Redux.BLL.Abstract;
using Redux.BLL.Concrete;
using Redux.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace MusicStore.UI.MVC.Tools
{
    public class UserUrlConstraint : IRouteConstraint
    {
        [Inject]
        public IUserService userService { get; set; }
        public UserUrlConstraint(IUserService userService)
        {
            this.userService = userService;
        }
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (values[parameterName] != null)
            {
                string[] permalink = values[parameterName].ToString().Split('/');
                if (permalink.Length > 2) return false;
                return userService.GetAll().Any(p => p.UserName.ToLower() == permalink[0].ToLower());
            }
            return false;
        }
    }
}