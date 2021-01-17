using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Redux.UI.MVC.Models
{
    public enum Sort
    {
        [Description("En Yeniler")]
        Yeniler,
        [Description("En Beğenilenler")]
        Begenilenler,
        [Description("En Çok İndirilenler")]
        Indirilenler
    }

    public enum Since
    {
        [Description("Dün")]
        Dun,
        [Description("Bu Hafta")]
        Hafta,
        [Description("Bu Ay")]
        Ay,
        [Description("En Baştan")]
        Hepsi
    }

    public enum Options
    {
        Add,
        Edit,
        Delete
    }
}