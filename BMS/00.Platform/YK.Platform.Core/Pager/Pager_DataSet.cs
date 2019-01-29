using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using YK.Platform.Core.SqlHelper;
using YK.Platform.Utility.Extensions;

namespace YK.Platform.Core
{
    /// <summary>
    /// 分页
    /// </summary>
    public class Pager_DataSet
    {
        /// <summary>
        /// 传递参数分别是：表名，主键，页面大小，分页码，条件，查询总数,参数
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="primaryKey"></param>
        /// <param name="selectValue"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <param name="recordCount"></param>
        /// <param name="spr"></param>
        /// <returns></returns>
        public DataSet GetPagerInfo(string tableName, string primaryKey, string selectValue, int pageSize, int pageIndex, string where, string orderBy, ref int recordCount, List<SqlParameter> spr)
        {
            where = string.IsNullOrEmpty(where) ? "1=1" : where;
            orderBy = string.IsNullOrEmpty(orderBy) ? (" Order By " + primaryKey + " ASC") : (" Order By " + orderBy);

            string cmdText = "select count(" + primaryKey + ") from " + tableName + " where " + where;
            recordCount = SqlConvertHelper.GetInstallSqlHelper().ExecuteScalar(cmdText, spr).ToInt();

            //当页码为1，或小于1时
            if (pageIndex <= 1)
            {
                cmdText = "select top " + pageSize + " " + selectValue + " from " + tableName + " where " + where + " " + orderBy;
            }
            else
            {
                string pageCmdText = @"
                            SELECT {0} FROM (
	                            SELECT ROW_NUMBER() OVER ( {5} ) rowindex,{0}
                                    FROM {1} 
                                      WHERE {2} 
                            )_t WHERE rowindex BETWEEN {3} AND {4} ORDER BY rowindex
                            ";
                int StartIndex = pageSize * (pageIndex - 1);
                int EndIndex = pageSize * pageIndex;
                cmdText = string.Format(pageCmdText, selectValue, tableName, where, StartIndex, EndIndex, orderBy);
            }
            return SqlConvertHelper.GetInstallSqlHelper().GetDataSet(cmdText, spr);
        }

        /// <summary>
        /// 传递参数分别是：表名，主键，页面大小，分页码，条件，查询总数，参数
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="primaryKey"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <param name="recordCount"></param>
        /// <param name="spr"></param>
        /// <returns></returns>
        public DataSet GetPagerInfo(string tableName, string primaryKey, int pageSize, int pageIndex, string where, string orderBy, ref int recordCount, List<SqlParameter> spr)
        {
            return GetPagerInfo(tableName, primaryKey, "*", pageSize, pageIndex, where, orderBy, ref recordCount, spr);
        }

        /// <summary>
        /// 传递参数分别是：表名，主键，页面大小，分页码，条件，查询总数
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="primaryKey"></param>
        /// <param name="selectValue"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public DataSet GetPagerInfo(string tableName, string primaryKey, string selectValue, int pageSize, int pageIndex, string where, string orderBy, ref int recordCount)
        {
            return GetPagerInfo(tableName, primaryKey, selectValue, pageSize, pageIndex, where, orderBy, ref recordCount, null);
        }

        /// <summary>
        /// 传递参数分别是：表名，主键，页面大小，分页码，条件，查询总数
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="primaryKey"></param>
        /// <param name="pageSize"></param>
        /// <param name="pageIndex"></param>
        /// <param name="where"></param>
        /// <param name="orderBy"></param>
        /// <param name="recordCount"></param>
        /// <returns></returns>
        public DataSet GetPagerInfo(string tableName, string primaryKey, int pageSize, int pageIndex, string where, string orderBy, ref int recordCount)
        {
            return GetPagerInfo(tableName, primaryKey, "*", pageSize, pageIndex, where, orderBy, ref recordCount);
        }
    }
}
