using ConsoleApp.属性注入;
using System;
using YK.Platform.Core;
using 设计模式;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
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
