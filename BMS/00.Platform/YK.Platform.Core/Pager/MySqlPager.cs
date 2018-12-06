using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using YK.Platform.Core.SqlHelper;

namespace YK.Platform.Core.Pager
{
    /// <summary>
    /// 分页
    /// </summary>
    internal class MySqlPager : PagerBase, IPager
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
        /// <returns></returns>
        public override string GetPagerSql(string tableName, string selectValue, int pageSize, int pageIndex, string where, string orderBy)
        {
            string cmdText = "select " + selectValue + " from " + tableName + " where " + where + orderBy + " limit " + pageSize * (pageIndex - 1) + "," + pageSize;
            return cmdText;
        }
    }
}
