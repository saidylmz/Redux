using Redux.BLL.Abstract;
using Redux.MODEL.Entities;
using Redux.UI.MVC.Models;
using Redux.UI.MVC.Tools.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Redux.UI.MVC.Tools
{
    public class FilterExtensions
    {
        public static List<Mod> Filter(List<Category> categories, string catName, Since since, Sort sort)
        {
            List<Mod> files = new List<Mod>();
            foreach (Category item in categories)
            {
                if (item.ToUrlString().ToLower() == catName.ToLower() || catName.EqualAny("all", "hepsi"))
                    files.AddRange(item.Mods.Where(x=>x.IsActive && x.IsConfirm));
            }
            switch (since)
            {
                case Since.Dun:
                    files = files.Where(x => (DateTime.Now - x.CreatedDate).Days < 2).ToList();
                    break;
                case Since.Hafta:
                    files = files.Where(x => (DateTime.Now - x.CreatedDate).Days < 8).ToList();
                    break;
                case Since.Ay:
                    files = files.Where(x => (DateTime.Now - x.CreatedDate).Days < 31).ToList();
                    break;
            }
            switch (sort)
            {
                case Sort.Begenilenler:
                    files = files.OrderByDescending(x => x.Likes.Count).ToList();
                    break;
                case Sort.Indirilenler:
                    files = files.OrderByDescending(x => x.Download).ToList();
                    break;
                default:
                    files = files.OrderByDescending(x => x.CreatedDate).ToList();
                    break;
            }
            return files;
        }
        
    }
}