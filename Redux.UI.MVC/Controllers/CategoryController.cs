using Redux.BLL.Abstract;
using Redux.MODEL.Entities;
using Redux.UI.MVC.Models;
using Redux.UI.MVC.Tools;
using Redux.UI.MVC.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Redux.UI.MVC.Controllers
{
    [RoleControl(Roles="All")]
    public class CategoryController : Controller
    {
        readonly ICategoryService categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        public ActionResult Index(string permalink)
        {
            string[] attrs = permalink.Split('/');
            string catName = attrs[0];
            int l = attrs.Length;

            if (!int.TryParse(attrs[l - 1], out int page))
                page = -1;
            else
                l -= 1;

            Since snc = Since.Hepsi;
            Sort srt = Sort.Yeniler;
            for (int i = 1; i < l; i++)
            {
                if (snc == Since.Hepsi && !Enum.TryParse(attrs[i], true, out snc)) snc = Since.Hepsi;
                if (srt == Sort.Yeniler && !Enum.TryParse(attrs[i], true, out srt)) srt = Sort.Yeniler;
            }

            List<Mod> mods = FilterExtensions.Filter(categoryService.GetAll().ToList(), catName, snc, srt);

            ViewBag.Since = (int)snc;
            ViewBag.Sort = (int)srt;
            ViewBag.Page = page == -1 ? 1 : page;
            ViewBag.Cat = catName;
            ViewBag.CatName = categoryService.GetAll().First(x => x.ToUrlString().ToLower() == catName.ToLower()).Name;
            return View(model: mods);
        }

    }
}