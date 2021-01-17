using Redux.BLL.Abstract;
using Redux.MODEL.Entities;
using Redux.UI.MVC.Tools;
using Redux.UI.MVC.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Redux.UI.MVC.Controllers
{
    public class UploadController : Controller
    {
        ICategoryService categoryService;
        IUserService userService;
        IModService modService;
        public UploadController(ICategoryService categoryService, IUserService userService, IModService modService)
        {
            this.categoryService = categoryService;
            this.userService = userService;
            this.modService = modService;
        }
        [RoleControl(Roles ="User")]
        public ActionResult Index()
        {
            return View(categoryService.GetAll());
        }


        [HttpPost, ValidateAntiForgeryToken, ValidateInput(false)]
        public ActionResult Index(string name, string author, int category, string description, string[] images, string version = null, HttpPostedFileBase file = null, string url = null)
        {
            Mod mod = new Mod()
            {
                Name = name,
                UserID = userService.GetAll().Where(x => x.UserName == author).FirstOrDefault().ID,
                Version = version ?? "",
                Description = description,
                CategoryID = category
            };
            modService.Insert(mod);
            DirectoryInfo di = Directory.CreateDirectory(Path.Combine(Server.MapPath("~/Files"), mod.ToUrlString()));
            if (file != null && file.ContentLength > 0)
                mod.AttachFile(file, di.FullName);
            else
                mod.File = url;

            di = Directory.CreateDirectory(Path.Combine(di.FullName, "Images"));
            mod.AttachImages(images, di.FullName);
            modService.Update(mod);
            return Redirect("/"+mod.ToUrlString());
        }
        [HttpPost, ValidateAntiForgeryToken, ValidateInput(false)]
        public ActionResult Edit(int id, string name, string description, string[] images, string version = null, HttpPostedFileBase file = null, string url = null)
        {
            Mod mod = modService.Get(id);
            mod.Name = name;
            mod.Version = version ?? "";
            mod.Description = description;

            string di = Path.Combine(Server.MapPath("~/Files"), mod.ToUrlString());
            if (file != null && file.ContentLength > 0)
                mod.AttachFile(file, di);
            else if (!string.IsNullOrEmpty(url))
                mod.File = url;

            modService.Update(mod);
            di = Path.Combine(di, "Images");
            mod.AttachImages(images, di, DependencyResolver.Current.GetService<IMediaService>());
            modService.Update(mod);
            return Redirect("/"+mod.ToUrlString());
        }
    }
}