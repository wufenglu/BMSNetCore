using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp.属性注入
{
    public class AttributeIocPublicService : IAttributeIocPublicService
    {
        public ILevelAttributeIocPublicService iLevelAttributeIocPublicService { get; set; }

        public void Save()
        {
            Console.WriteLine("Save");

            iLevelAttributeIocPublicService.Save();
        }
    }
}
