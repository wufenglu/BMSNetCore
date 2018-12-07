using System;
using System.Collections.Generic;
using System.Text;

namespace YK.Platform.Core.Model
{
    /// <summary>
    /// 组织执行结果
    /// </summary>
    public class OrganizationExecuteResult
    {
        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 数据
        /// </summary>
        public object Data { get; set; }
    }
}
