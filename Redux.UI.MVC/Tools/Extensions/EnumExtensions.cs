using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Redux.UI.MVC.Tools
{
    public class EnumExtensions
    {
        public static string GetDescriptionFromEnumValue(Enum value)
        {
            return !(value.GetType()
                .GetField(value.ToString())
                .GetCustomAttributes(typeof(DescriptionAttribute), false)
                .SingleOrDefault() is DescriptionAttribute attribute) ? value.ToString() : attribute.Description;
        }
    }
}