using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Redux.UI.MVC.Tools
{
    public static class StringExtensions
    {
        public static bool ContainsAny(this string str, params string[] values)
        {
            if (!string.IsNullOrEmpty(str) || values.Length > 0)
            {
                foreach (string value in values)
                {
                    if (str.ToLower().Contains(value.ToLower()))
                        return true;
                }
            }
            return false;
        }
        public static bool EqualAny(this string str, params string[] values)
        {
            if (!string.IsNullOrEmpty(str) || values.Length > 0)
            {
                foreach (string value in values)
                {
                    if (str.ToLower() == value.ToLower())
                        return true;
                }
            }
            return false;
        }

        public static string Cut(this string str, int maxLength, string endstr = "...")
        {
            if (str.Length <= maxLength) return str;
            return str.Substring(0, maxLength) + endstr;
        }
    }
}