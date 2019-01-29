using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YK.Platform.Utility
{
    /// <summary>
    /// 令牌生成器
    /// </summary>
    public class AccessTokenHelper
    {
        /// <summary>
        /// 加密键
        /// </summary>
        public static string key = "myAesKey";
        /// <summary>
        /// 有效期（分钟）
        /// </summary>
        public static int expiryDate = 60 * 60 * 2;

        /// <summary>
        /// 获取accessToken
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string GetAccessToken(string data) {
            data += ";" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            return AesHelper.Encrypt(data, key);
        }

        /// <summary>
        /// 验证accessToken
        /// </summary>
        /// <param name="accessToken"></param>
        /// <returns></returns>
        public static bool ValidateAccessToken(string accessToken)
        {
            string data = AesHelper.Decrypt(accessToken, key);
            string[] arr = data.Split(';');

            //判断长度
            if (arr.Length < 1)
            {
                return false;
            }

            //校验有效期
            DateTime startTime = Convert.ToDateTime(arr[arr.Length - 1]);
            if ((DateTime.Now - startTime).Seconds > expiryDate)
            {
                return false;
            }

            return true;
        }
    }
}
