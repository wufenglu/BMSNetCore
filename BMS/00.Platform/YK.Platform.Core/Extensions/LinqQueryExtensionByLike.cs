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
    public static class LinqQueryExtensionByLike
    {
        /// <summary>
        /// Like
        /// </summary>
        /// <param name="value"></param>
        /// <param name="toValue"></param>
        /// <returns></returns>
        public static bool Like(this string value, string toValue)
        {
            return true;
        }

        /// <summary>
        /// NotLike
        /// </summary>
        /// <param name="value"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool NotLike(this string value, string toValue)
        {
            return true;
        }

        /// <summary>
        /// LeftLike
        /// </summary>
        /// <param name="value"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool LeftLike(this string value, string toValue)
        {
            return true;
        }

        /// <summary>
        /// RightLike
        /// </summary>
        /// <param name="value"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool RightLike(this string value, string toValue)
        {
            return true;
        }
    }
}
