using System;
using System.Collections.Generic;
using System.Text;

namespace YK.Platform.Core.Model
{
    /// <summary>
    /// 租户实体
    /// </summary>
    public class OrganizationEntity
    {
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 驱动
        /// </summary>
        public string Provider { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        public string Code { get; set; }
        /// <summary>
        /// 数据库连接
        /// </summary>
        public string Connectionstring { get; set; }
        /// <summary>
        /// 状态：0启用，1禁用
        /// </summary>
        public int State { get; set; }
        /// <summary>
        /// 从库
        /// </summary>
        public List<OrganizationSalves> Slaves { get; set; }
    }
}
