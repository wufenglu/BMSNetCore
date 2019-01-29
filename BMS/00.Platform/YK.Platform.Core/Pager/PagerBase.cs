using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using YK.Platform.Core.SqlHelper;
using YK.Platform.Utility.Extensions;

namespace YK.Platform.Core.Pager
{
    /// <summary>
    /// 分页基类
    /// </summary>
    public abstract class PagerBase
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
        public IDataReader GetPagerInfo(string tableName, string selectValue, int pageSize, int pageIndex, string where, string orderBy, ref int recordCount, List<SqlParameter> spr)
        {
            where = string.IsNullOrEmpty(where) ? "1=1" : where;

            string cmdText = "select count(1) from " + tableName + " where " + where;
            recordCount = SqlConvertHelper.GetInstallSqlHelper().ExecuteScalar(cmdText, spr).ToInt();

            //当页码为1，或小于1时
            if (pageIndex <= 1)
            {
                cmdText = "select top " + pageSize + " " + selectValue + " from " + tableName + " where " + where + " " + orderBy;
            }
            else
            {
                cmdText = GetPagerSql(tableName,selectValue,pageSize,pageIndex,where,orderBy);
            }
            return SqlConvertHelper.GetInstallSqlHelper().ExecuteReader(cmdText, spr);
        }

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
        public abstract string GetPagerSql(string tableName, string selectValue, int pageSize, int pageIndex, string where, string orderBy);
    }
}
