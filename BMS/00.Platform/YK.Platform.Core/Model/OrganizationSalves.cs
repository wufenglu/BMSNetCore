using System;
using System.Collections.Generic;
using System.Text;

namespace YK.Platform.Core.Model
{
    /// <summary>
    /// 从库
    /// </summary>
    public class OrganizationSalves
    {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string Connectionstring { get; set; }
        /// <summary>
        /// 比重
        /// </summary>
        public decimal Proportion { get; set; }
        /// <summary>
        /// 状态：0启用，1禁用
        /// </summary>
        public int State { get; set; }
    }
}
