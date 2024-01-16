using System;
using System.Collections.Generic;

namespace mongodb_dotnet_example
{
    public static class  Extensions
    {
        public static string RemovePostFix(this string str, params string[] postFixes)
        {
            return str.RemovePostFix(StringComparison.Ordinal, postFixes);
        }
        public static bool IsNullOrEmpty<T>(this ICollection<T>? source)
        {
            return source == null || source.Count <= 0;
        }

        public static string Left(this string str, int len)
        {
           
            if (str == null)
            {
                throw new ArgumentNullException(str);
            }

            if (str.Length < len)
            {
                throw new ArgumentException("len argument can not be bigger than given string's length!");
            }

            return str.Substring(0, len);
        }
        public static string RemovePostFix(this string str, StringComparison comparisonType, params string[] postFixes)
        {
            if (string.IsNullOrEmpty(str))
            {
                return str;
            }

            if (postFixes.IsNullOrEmpty())
            {
                return str;
            }

            foreach (var postFix in postFixes)
            {
                if (str.EndsWith(postFix, comparisonType))
                {
                    return str.Left(str.Length - postFix.Length);
                }
            }

            return str;
        }
    }
}
