using Redux.BLL.Abstract;
using Redux.MODEL.Entities;
using Redux.UI.MVC.Models;
using Redux.UI.MVC.Tools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Redux.UI.MVC.Controllers
{
    public class SharedController : Controller
    {
        readonly ICategoryService categoryService;

        public SharedController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [ChildActionOnly]
        public PartialViewResult _CatList()
        {
            return PartialView(categoryService.GetAll());
        }
    }
}