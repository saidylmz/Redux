using Redux.BLL.Abstract;
using Redux.MODEL.Entities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

namespace Redux.UI.MVC.Tools.Extensions
{
    public static class EntityHelpers
    {
        public static string ToUrlString(this Mod mod)
        {
            string nm = mod.Name;
            char[] tr = "ığüşöçĞÜŞİÖÇI".ToCharArray();
            char[] en = "igusocgusioci".ToCharArray();
            for (int i = 0; i < tr.Length; i++)
            {
                nm = nm.Replace(tr[i], en[i]);
            }
            nm = nm.ToLower();
            const string chars = "!@#\"&()-_[{}]:;',?/*\\`~$^+=<>“|";
            for (int i = 0; i < chars.Length; i++)
            {
                nm = nm.Replace(chars[i] + "", string.Empty);
            }
            nm = nm.Replace(" ", "-");
            nm = nm.Replace("--", "-");
            return mod.Category.ToUrlString() + "-" + nm + "-" + mod.ID;
        }
        public static string ToUrlString(this Category mod)
        {
            string nm = mod.Name;
            char[] tr = "ığüşöçĞÜŞİÖÇI".ToCharArray();
            char[] en = "igusocgusioci".ToCharArray();
            for (int i = 0; i < tr.Length; i++)
            {
                nm = nm.Replace(tr[i], en[i]);
            }
            nm = nm.ToLower();
            const string chars = "!@#\"&()-[{}]:;',?/*\\`~$^+=<>“|";
            for (int i = 0; i < chars.Length; i++)
            {
                nm = nm.Replace(chars[i] + "", string.Empty);
            }
            nm = nm.Replace(" ", "-");
            nm = nm.Replace("--", "-");
            return nm;
        }
        public static bool AttachFile(this Mod mod, HttpPostedFileBase file, string filepath)
        {
            try
            {
                string filename = mod.ID + "_" + Path.GetFileName(file.FileName);

                if (mod.File != null && mod.File.Contains("..") && File.Exists(Path.Combine(filepath, mod.File.Split('/').Last())))
                    System.IO.File.Delete(Path.Combine(filepath, mod.File.Split('/').Last()));

                file.SaveAs(Path.Combine(filepath, filename));
                mod.File = "../Files/" + mod.ToUrlString() + "/" + filename;
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public static void AttachImages(this Mod mod, string[] images, string filepath, IMediaService mediaService = null)
        {
            if(mediaService != null)
            {
                foreach (Media item in mod.Medias.ToList())
                {
                    if (!images.Contains(item.URL))
                    {
                        mediaService.Delete(item);
                        mod.Medias.Remove(item);
                        string fp = Path.Combine(filepath, item.URL.Split('/').Last());
                        if (File.Exists(fp))
                            System.IO.File.Delete(fp);
                    }
                }
            }
            foreach (string item in images.Where(x => x.Contains("base64")))
            {
                int bin = item.IndexOf("base64");
                string bs64 = item.Remove(0, bin + ("base64,").Length);
                Image img = ImageExtensions.Base64ToImage(bs64);
                const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
                Random random = new Random();
                string uniqueid = new string(Enumerable.Repeat(chars, new Random().Next(6, 10)).Select(s => s[random.Next(s.Length)]).ToArray()).ToLower();

                string filename = mod.ID + "_" + uniqueid + "." + new ImageFormatConverter().ConvertToString(img.RawFormat);
                string path = Path.Combine(filepath, filename);
                if (img.Width > 2048 || img.Height > 2048)
                {
                    if (img.Width > img.Height)
                    {
                        float perc = 2048 / (float)img.Width;
                        int ss = (int)Math.Floor(img.Height * perc);
                        img = img.GetThumbnailImage(2048, ss, null, IntPtr.Zero);
                    }
                    else
                    {
                        float perc = 2048 / (float)img.Height;
                        int ss = (int)Math.Floor(img.Width * perc);
                        img = img.GetThumbnailImage(ss, 2048, null, IntPtr.Zero);
                    }
                }
                else img = img.GetThumbnailImage(img.Width, img.Height, null, IntPtr.Zero);

                img.Save(path);
                mod.Medias.Add(new Media() { IsPhoto = true, URL = "../Files/" + mod.ToUrlString() + "/Images/" + filename });
            }
        }
        public static string AvatarSource(this User user)
        {
            return user is null || user.Avatar is null ? "../Content/Images/noavatar.png" : user.Avatar;
        }
    }
}