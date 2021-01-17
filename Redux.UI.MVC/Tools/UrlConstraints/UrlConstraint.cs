using Ninject;
using Redux.BLL.Abstract;
using Redux.BLL.Concrete;
using Redux.MODEL.Entities;
using Redux.UI.MVC.Tools;
using Redux.UI.MVC.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace MusicStore.UI.MVC.Tools
{
    public class UrlConstraint : IRouteConstraint
    {
        [Inject]
        public ICategoryService CategoryService { get; set; }
        public UrlConstraint(ICategoryService catsr)
        {
            CategoryService = catsr;
        }
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection)
        {
            var db = CategoryService.GetAll();
            if (values[parameterName] != null)
            {
                string[] permalink = values[parameterName].ToString().Split('/');
                if (permalink.Length > 4) return false;
                if (permalink[0].EqualAny("all","hepsi")) return true;
                return db.Any(p => p.ToUrlString().ToLower() == permalink[0].ToLower());
            }
            return false;
        }
    }
}