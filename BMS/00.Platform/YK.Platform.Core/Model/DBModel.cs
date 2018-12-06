using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace YK.Platform.Core.Model
{
    /// <summary>
    /// 数据库对象实体
    /// </summary>
    public class DBModel
    {
        /// <summary>
        /// 返回SQL
        /// </summary>
        public string SQL { get; set; }

        /// <summary>
        /// 返回参数
        /// </summary>
        public List<SqlParameter> Params { get; set; }
    }
}
