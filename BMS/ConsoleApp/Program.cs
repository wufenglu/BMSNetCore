﻿using ConsoleApp.属性注入;
using System;
using YK.Platform.Core;
using 设计模式;
using YK.Platform.SSO;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Jwt jwt = new Jwt();
            jwt.SecretKey = "SecretKey";
            string token = jwt.GetToken("yank", "doc");

            string json = jwt.GetJson(token);

            Injection();

            AttributeIocAppService service = ServiceContainer.InitService<AttributeIocAppService>();
            service.Save();

            Console.Read();
        }

        /// <summary>
        /// 注入
        /// </summary>
        static void Injection()
        {
            ServiceContainer.Register<IAttributeIocPublicService, AttributeIocPublicService>();
            ServiceContainer.Register<ILevelAttributeIocPublicService, LevelAttributeIocPublicService>();
        }
    }
}
