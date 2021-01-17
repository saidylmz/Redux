using System.Web.Mvc;

namespace Redux.UI.MVC.Areas.Admin
{
    public class AdminAreaRegistration : AreaRegistration 
    {
        public override string AreaName 
        {
            get 
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context) 
        {
            context.MapRoute(
                "Yönetim",
                "Yönetim/{controller}/{action}/{id}",
                new { action = "Index", controller = "AdminHome", Area = "Admin", id = UrlParameter.Optional }
            );
        }
    }
}