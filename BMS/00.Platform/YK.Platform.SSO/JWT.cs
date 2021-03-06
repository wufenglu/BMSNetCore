﻿using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using System;
using System.Collections.Generic;
using System.Text;
using YK.Platform.Utility;

namespace YK.Platform.SSO
{
    /// <summary>
    /// Jwt
    /// https://blog.csdn.net/weixin_44308006/article/details/90459801
    /// </summary>
    public class Jwt
    {
        /// <summary>
        /// 私匙
        /// </summary>
        public string SecretKey { get; set; }

        /// <summary>
        /// 获取令牌
        /// </summary>
        /// <returns></returns>
        public string GetToken(string userName, string targetAppName)
        {
            //第一部分我们称它为头部（header)
            //第二部分我们称其为载荷（payload)
            //第三部分是签证（signature)
            // javascript
            //var encodedString = base64UrlEncode(header) + '.' + base64UrlEncode(payload);
            //var signature = HMACSHA256(encodedString, 'secret'); 

            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);

            //头部、载荷（Playload）与签名。
            Playload playload = new Playload(userName, targetAppName);
            var token = encoder.Encode(playload, SecretKey);
            return token;
        }

        public string CreateToken(string userName, string targetAppName) {
            //第一部分我们称它为头部（header)
            //第二部分我们称其为载荷（payload)
            //第三部分是签证（signature)
            // javascript
            //var encodedString = base64UrlEncode(header) + '.' + base64UrlEncode(payload);
            //var signature = HMACSHA256(encodedString, 'secret'); 

            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();

            //头部
            Dictionary<string, string> headDic = new Dictionary<string, string>();
            headDic.Add("typ", "JWT");
            headDic.Add("alg", "HS256");
            byte[] headBytes = Encoding.UTF8.GetBytes(serializer.Serialize(headDic));
            string head = urlEncoder.Encode(headBytes);

            //载荷
            Playload playload = new Playload(userName, targetAppName);
            byte[] pyloadBytes = Encoding.UTF8.GetBytes(serializer.Serialize(playload));
            string pyload = urlEncoder.Encode(pyloadBytes);

            List<string> list = new List<string>(3);
            list.Add(head);
            list.Add(pyload);

            //签名
            byte[] mergeBytes = Encoding.UTF8.GetBytes(string.Join(".", list.ToArray()));
            string sign = urlEncoder.Encode(algorithm.Sign(Encoding.UTF8.GetBytes(SecretKey), mergeBytes));
            list.Add(sign);

            return string.Join(".", list.ToArray());
        }

        /// <summary>
        /// 获取JSON串
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public string GetJson(string token)
        {
            IJsonSerializer serializer = new JsonNetSerializer();
            IDateTimeProvider provider = new UtcDateTimeProvider();
            IJwtValidator validator = new JwtValidator(serializer, provider);
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);

            string[] arr = token.Split('.');
            byte[] headerBytes = urlEncoder.Decode(arr[0]);
            string header = System.Text.Encoding.UTF8.GetString(headerBytes);
            byte[] claimBytes = urlEncoder.Decode(arr[1]);
            string claim = System.Text.Encoding.UTF8.GetString(claimBytes);
            string sign = arr[2];

            var json = decoder.Decode(token, SecretKey, true);
            return json;
        }
    }

    /// <summary>
    /// Token基本信息
    /// </summary>
    public class Playload
    {

        /// <summary>
        /// 获取Token基本信息
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="appName"></param>
        public Playload(string userName, string appName)
        {
            iss = "BMS";
            sub = userName;
            aud = appName;
            iat = (DateTime.UtcNow - DateTime.MinValue).TotalSeconds;
            exp = iat + 60 * 10;//十分钟
            jti = Guid.NewGuid().ToString();
        }

        /// <summary>
        /// jwt签发者
        /// </summary>
        public string iss { get; set; }

        /// <summary>
        /// jwt面向的用户
        /// </summary>
        public string sub { get; set; }

        /// <summary>
        /// 接收jwt的一方
        /// </summary>
        public string aud { get; set; }

        /// <summary>
        /// jwt的签发时间
        /// </summary>
        public double iat { get; set; }

        /// <summary>
        /// jwt过期时间，这个过期时间必须要大于签发时间
        /// </summary>
        public double exp { get; set; }

        /// <summary>
        /// 定义在什么时间之前，该jwt都是不可用的.
        /// </summary>
        //public double nbf { get; set; }

        /// <summary>
        ///  jwt的唯一身份标识，主要用来作为一次性token,从而回避重放攻击。
        /// </summary>
        public string jti { get; set; }
    }
}
