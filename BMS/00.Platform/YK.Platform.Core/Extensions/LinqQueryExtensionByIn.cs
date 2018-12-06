using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Text.RegularExpressions;

using YK.Platform.Core;
using System.Data;
using System.Reflection;
using YK.Platform.Core.Model;
using System.Data.SqlClient;

namespace YK.Platform.Core.Extensions
{
    /// <summary>
    /// 扩展方法
    /// </summary>
    public static class LinqQueryExtensionByIn
    {
        /// <summary>
        /// In
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static bool In<T>(this T value,IEnumerable<T> values)
        {
            return true;
        }

        /// <summary>
        /// Not In
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="values"></param>
        /// <returns></returns>
        public static bool NotIn<T>(this T value, IEnumerable<T> values)
        {
            return true;
        }        
    }
}
