using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Redux.UI.MVC.Tools.UrlConstraints
{
    public class AjaxUrlConstraint : IRouteConstraint
    {
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            if (httpContext.Request.IsAjaxRequest()) return true;
            
            if (values[parameterName] != null)
            {
                string action = values["action"].ToString();
                Assembly asm = Assembly.GetExecutingAssembly();

                return asm.GetTypes()
                    .Where(type => typeof(Controller).IsAssignableFrom(type) && type.Name.ToLower() == values["controller"].ToString().ToLower() + "controller")
                    .SelectMany(type => type.GetMethods())
                    .Any(x => x.IsPublic && x.Name.ToLower() == action.ToLower() && x.GetCustomAttribute(typeof(ChildActionOnlyAttribute)) != null); ;
            }
            return false;
        }
    }
}