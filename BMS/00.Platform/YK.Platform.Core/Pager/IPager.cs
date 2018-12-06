using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web;

namespace YK.Platform.Core.Pager
{
    /// <summary>
    /// 分页
    /// </summary>
    public interface IPager
    {
        /// <summary>
        /// 传递参数分别是：表名，主键，页面大小，分页码，条件，查询总数,参数
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="selectValue"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <param name="recordCount"></param>
        /// <param name="spr"></param>
        /// <returns></returns>
        IDataReader GetPagerInfo(string tableName, string selectValue, int pageSize, int pageIndex, string where, string orderBy, ref int recordCount, List<SqlParameter> spr);
    }
}
