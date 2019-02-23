using ConsoleApp.属性注入;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace 设计模式
{
    public class AttributeIocAppService
    {
        /// <summary>
        /// 接口服务
        /// </summary>
        public IAttributeIocPublicService iAttributeIocPublicService { get; set; }

        /// <summary>
        /// 保存
        /// </summary>
        public void Save()
        {
            iAttributeIocPublicService.Save();
        }
    }
}
