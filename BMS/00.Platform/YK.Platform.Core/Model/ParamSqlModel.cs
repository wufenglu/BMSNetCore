using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace YK.Platform.Core.Model
{
    /// <summary>
    /// 参数属性实体
    /// </summary>
    public class ParamSqlModel
    {
        /// <summary>
        /// 字段
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// 是否主键
        /// </summary>
        public bool IsPrimaryKey { get; set; }

        /// <summary>
        /// 是否自动生成
        /// </summary>
        public bool IsDbGenerated { get; set; }

        /// <summary>
        /// 返回SQL
        /// </summary>
        public string SQL { get; set; }

        /// <summary>
        /// 返回参数
        /// </summary>
        public SqlParameter Param { get; set; }
    }
}
