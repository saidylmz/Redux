using Redux.BLL.Abstract;
using Redux.MODEL.Entities;
using Redux.UI.MVC.Models;
using Redux.UI.MVC.Tools;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Redux.UI.MVC.Areas.Admin.Controllers
{

    [RouteArea("Admin"), RoleControl(Roles = "Admin")]
    public class AdminHomeController : Controller
    {
        readonly ICategoryService categoryService;
        readonly IUserService userService;
        readonly IModService modService;

        public AdminHomeController(ICategoryService categoryService, IUserService userService, IModService modService)
        {
            this.categoryService = categoryService;
            this.userService = userService;
            this.modService = modService;
        }
        public ActionResult Index()
        {
            ViewBag.Error = ViewBag.Error;
            ViewBag.NotConfirm = modService.GetAll().Count(x => x.IsActive && x.IsConfirm is false);
            return View();
        }

        #region PartialViews
        public PartialViewResult _CategoryMenu()
        {
            return PartialView(categoryService.GetAll());
        }
        public PartialViewResult _UserMenu()
        {
            return PartialView(userService.GetAll());
        }
        public PartialViewResult _PermissionMenu()
        {
            return PartialView(modService.GetAll());
        }
        #endregion
        public ActionResult Category(Options option, HttpPostedFileBase file = null, int id = -1, string name = null)
        {
            if (categoryService.GetByName(name) != null && option != Options.Edit) ViewBag.Error = "Aynı isimde kategori mevcut.";
            else
            {
                switch (option)
                {
                    case Options.Add:
                        Category ncat = new Category() { Name = name, Image = "" };
                        categoryService.Insert(ncat);
                        WebImage img = new WebImage(file.InputStream);
                        string filename = ncat.ID + "_" + ncat.Name + "."+img.ImageFormat;
                        string path = Path.Combine(Server.MapPath("~/Files/Category"), filename);
                        if (img.Width > 1024 || img.Height > 1024)
                        {
                            if (img.Width > img.Height)
                            {
                                img = img.Resize(1024, img.Height, true);
                            }
                            else
                            {
                                img = img.Resize(img.Width, 1024, true);
                            }
                        }
                        ncat.Image = "../Files/Category/" + filename;
                        img.Save(path);
                        categoryService.Update(ncat)
                        break;
                    case Options.Edit:
                        if (categoryService.GetAll().Any(x => x.ID != id && x.Name.ToLower() == name)) ViewBag.Error = "Aynı isimde farklı kategori mevcut.";
                        else
                        {
                            Category cat = categoryService.Get(id);
                            cat.Name = name;
                            if (file != null)
                            {
                                path = Path.Combine(Server.MapPath("~/Files/Category"), cat.Image.Split('/').Last());
                                if (System.IO.File.Exists(path))
                                    System.IO.File.Delete(path);

                                img = new WebImage(file.InputStream);
                                filename = cat.ID + "_" + name + "." + img.ImageFormat;
                                path = Path.Combine(Server.MapPath("~/Files/Category"), filename);
                                if (img.Width > 1024 || img.Height > 1024)
                                {
                                    if (img.Width > img.Height)
                                    {
                                        img = img.Resize(1024, img.Height, true);
                                    }
                                    else
                                    {
                                        img = img.Resize(img.Width, 1024, true);
                                    }
                                }
                                img.Save(path);
                                cat.Image = "../Files/Category/" + filename;
                            }
                            categoryService.Update(cat);
                        }
                        break;
                    case Options.Delete:
                        Category catd = categoryService.Get(id);
                        if (catd != null)
                        {
                            if (catd.Mods.Count > 0) ViewBag.Error = "Seçtiğiniz kategoriye ait dosyalar olduğu için silinemez.";
                            else
                            {
                                categoryService.Delete(catd);
                                path = Path.Combine(Server.MapPath("~/Files/Category"), catd.Image.Split('/').Last());
                                if (System.IO.File.Exists(path))
                                    System.IO.File.Delete(path);
                            }
                        }
                        break;
                }
            }
            return RedirectToAction("Index");
        }
        public void FileConfirm(bool confirm, int id)
        {
            Mod file = modService.Get(id);
            file.IsConfirm = confirm;
        }
        public void Featured(int id)
        {
            Mod file = modService.Get(id);
            if (file.IsFeatured)
                file.IsFeatured = false;
            else
                file.IsFeatured = true;
            modService.Update(file);
        }
    }
}
