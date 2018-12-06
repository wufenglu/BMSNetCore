using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YK.Platform.Core.Model
{
    /// <summary>
    /// 获取实体的属性的Column特性
    /// </summary>
    public class EntityPropColumnAttributes
    {
        /// <summary>
        /// 属性名称
        /// </summary>
        public string propName { get; set; }
        /// <summary>
        /// 字段名称
        /// </summary>
        public string fieldName { get; set; }
        /// <summary>
        /// 是否主键
        /// </summary>
        public bool isPrimaryKey { get; set; }
        /// <summary>
        /// 是否数据库生成
        /// </summary>
        public bool isDbGenerated { get; set; }
        /// <summary>
        /// 属性的类型名称
        /// </summary>
        public string typeName { get; set; }
    }
}
