using Redux.BLL.Abstract;
using Redux.UI.MVC.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Redux.UI.MVC.Controllers
{
    [RoleControl(Roles = "All")]
    public class HomeController : Controller
    {
        IModService modService;
        public HomeController(IModService modService)
        {
            this.modService = modService;
        }
        public ActionResult Index()
        {
            return View(modService.GetAll().Where(x=>x.IsConfirm && x.IsActive));
        }
        
    }
}