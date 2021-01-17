using Redux.BLL.Abstract;
using Redux.MODEL.Entities;
using Redux.UI.MVC.Tools;
using Redux.UI.MVC.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Redux.UI.MVC.Controllers
{
    public class FileController : Controller
    {
        IModService modService;
        ICommentService commentService;
        public FileController(IModService modService, ICommentService commentService)
        {
            this.modService = modService;
            this.commentService = commentService;
        }
        public ActionResult Index(string permalink)
        {
            string[] links = permalink.Split('/');
            Mod file = modService.GetAll().FirstOrDefault(x => x.ToUrlString() == links[0].ToLower());
            bool isAdmin = User.Identity.IsAuthenticated && DependencyResolver.Current.GetService<IUserService>().GetUserByName(User.Identity.Name).IsAdmin;
            if (file == null) return RedirectToRoute("Anasayfa");
            if (!file.IsConfirm)
            {
                if (!User.Identity.IsAuthenticated || (!file.User.UserName.EqualAny(User.Identity.Name) && !isAdmin)) return RedirectToRoute("Anasayfa");
            }
            if (links.Length > 1)
            {
                if (links[1].EqualAny("indir", "download"))
                {
                    file.Download++;
                    modService.Update(file);
                    if (file.File.StartsWith(".."))
                        return File(System.IO.File.ReadAllBytes(Server.MapPath(file.File)), System.Net.Mime.MediaTypeNames.Application.Zip, fileDownloadName: file.File.Split('/').Last());
                    else
                        return Redirect(file.File);
                }
                else if (User.Identity.IsAuthenticated && file.User.UserName.EqualAny(User.Identity.Name))
                    if (links[1].EqualAny("edit", "duzenle"))
                        return View(viewName: "EditFile", file);
            }
            return View(file);
        }
        public bool DeleteFile(int id)
        {
            try
            {
                Mod file = modService.Get(id);
                if (DependencyResolver.Current.GetService<IUserService>().GetUserByName(User.Identity.Name).IsAdmin)
                {
                    string pth = Path.Combine(Server.MapPath("~/Files"), file.ToUrlString());
                    if (Directory.Exists(pth))
                        Directory.Delete(pth,true);
                    IMediaService mediaService = DependencyResolver.Current.GetService<IMediaService>();
                    foreach (var item in file.Medias.ToList())
                    {
                        mediaService.Delete(item);
                    }
                    file.Medias.Clear();
                    ICommentService commentService = DependencyResolver.Current.GetService<ICommentService>();
                    foreach (var item in file.Comments.ToList())
                    {
                        commentService.Delete(item);
                    }
                    file.Comments.Clear();
                    file.Likes.Clear();
                    modService.Delete(file);
                }
                else
                {
                    file.IsConfirm = file.IsActive = false;
                    modService.Update(file);
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult AddComment(int file, string comment)
        {
            Mod mod = modService.Get(file);
            mod.Comments.Add(new Comment() { Content = comment, UserID = DependencyResolver.Current.GetService<IUserService>().GetUserByName(User.Identity.Name).ID });
            modService.Update(mod);
            return Redirect("/" + mod.ToUrlString());
        }

        public bool DeleteComment(int id)
        {
            try
            {
                commentService.DeleteByID(id);
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public void Like(int id)
        {
            Mod file = modService.Get(id);
            User user = DependencyResolver.Current.GetService<IUserService>().GetUserByName(User.Identity.Name);
            if (file.Likes.Any(x => x.UserID == user.ID))
                file.Likes.Remove(file.Likes.Single(x => x.UserID == user.ID));
            else
                file.Likes.Add(new Like() { UserID = DependencyResolver.Current.GetService<IUserService>().GetUserByName(User.Identity.Name).ID });
            modService.Update(file);
        }
    }
}