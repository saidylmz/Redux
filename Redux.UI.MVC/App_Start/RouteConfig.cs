using MusicStore.UI.MVC.Tools;
using Redux.BLL.Abstract;
using Redux.MODEL.Entities;
using Redux.UI.MVC.Tools.UrlConstraints;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Redux.UI.MVC
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute("Home", url: "", new { controller = "Home", action = "Index" });
            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
                 name: "User",
                 url: "kullanici/{*permalink}",
                 defaults: new { controller = "User", action = "Index" },
                 constraints: new { permalink = new UserUrlConstraint(DependencyResolver.Current.GetService<IUserService>()) }
            );
            routes.MapRoute("Ajax&Child", "{controller}/{action}", null, new { action = new AjaxUrlConstraint() });
            routes.MapRoute("Form", "{controller}/{action}", null, new { action = new HttpMethodConstraint("POST") });
            routes.MapRoute(
                 name: "Upload",
                 url: "{*upload}",
                 defaults: new { controller = "Upload", action = "Index" },
                 constraints: new { upload = "dosya-yukle|upload|yukle|yayinla" }
            );
            routes.MapRoute(
                 name: "Anasayfa",
                 url: "anasayfa",
                 defaults: new { controller = "Home", action = "Index" }
            );
            routes.MapRoute(
              name: "Register",
              url: "Kayit-ol",
              defaults: new { controller = "Account", action = "Register" }
            );
            routes.MapRoute(
               name: "Logout",
               url: "{*logout}",
               defaults: new { controller = "Account", action = "LogOut" },
               constraints: new { logout = "cikis-yap|cikis|çıkış|logout" }
           );
            routes.MapRoute(
              name: "Login",
              url: "Giris-yap",
              defaults: new { controller = "Account", action = "Login" }
          );
            routes.MapRoute(
               name: "Settings",
               url: "Ayarlar",
               defaults: new { controller = "Settings", action = "Index" }
           );
            routes.MapRoute(
                 name: "CategoryRoute",
                 url: "{*permalink}",
                 defaults: new { controller = "Category", action = "Index" },
                 constraints: new { permalink = new UrlConstraint(DependencyResolver.Current.GetService<ICategoryService>()) }
             );
            routes.MapRoute(
                 name: "FileRoute",
                 url: "{*permalink}",
                 defaults: new { controller = "File", action = "Index" },
                 constraints: new { permalink = new FileUrlConstraint(DependencyResolver.Current.GetService<IModService>()) }
             );
            //routes.MapRoute(
            //    name: "Default",
            //    url: "{controller}/{action}/{id}",
            //    defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            //);
        }
    }
}
