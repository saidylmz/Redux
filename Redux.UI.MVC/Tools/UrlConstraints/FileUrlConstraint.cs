using Ninject;
using Redux.BLL.Abstract;
using Redux.BLL.Concrete;
using Redux.MODEL.Entities;
using Redux.UI.MVC.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace MusicStore.UI.MVC.Tools
{
    public class FileUrlConstraint : IRouteConstraint
    {
        public IModService modService { get; set; }
        public FileUrlConstraint(IModService modsr)
        {
            modService = modsr;
        }
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            var db = modService.GetAll();
            if (values[parameterName] != null)
            {
                string[] permalink = values[parameterName].ToString().Split('/');
                if (permalink.Length > 2 || permalink.Length < 1) return false;
                return modService.GetAll().Any(p => p.ToUrlString() == permalink[0].ToLower());
            }
            return false;
        }
    }
}