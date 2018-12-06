using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace YK.Platform.Core.Model
{

    /// <summary>
    /// 公共实体
    /// </summary>
    public class CoreFrameworkEntity
    {
        /// <summary>
        /// 参数列表
        /// </summary>
        public List<SqlParameter> ParaList { get; set; }
        /// <summary>
        /// 条件字符串
        /// </summary>
        public string Where { get; set; }
    }
}
