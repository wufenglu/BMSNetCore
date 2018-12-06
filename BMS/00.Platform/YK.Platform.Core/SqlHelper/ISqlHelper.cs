using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace YK.Platform.Core.SqlHelper
{
    /// <summary>
    /// 数据库操作帮助接口
    /// </summary>
    public interface ISqlHelper
    {
        /// <summary>
        /// 返回影响的行数
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="cmdText"></param>
        /// <param name="spr"></param>
        /// <returns></returns>
        int ExecuteNonQuery(CommandType commandType, string cmdText, List<SqlParameter> spr);

        /// <summary>
        /// 带参数的存储过程
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="spr"></param>
        /// <returns></returns>
        int ExecuteNonQueryPro(string cmdText, List<SqlParameter> spr);

        /// <summary>
        /// 不带参数的存储过程
        /// </summary>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        int ExecuteNonQueryPro(string cmdText);

        /// <summary>
        /// 带参数的操作语句
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="spr"></param>
        /// <returns></returns>
        int ExecuteNonQuery(string cmdText, List<SqlParameter> spr);

        /// <summary>
        /// 不带参数的操作语句
        /// </summary>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        int ExecuteNonQuery(string cmdText);

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="spr"></param>
        /// <returns></returns>
        DataSet GetDataSet(CommandType cmdType, string cmdText, List<SqlParameter> spr);

        /// <summary>
        /// 带参数的存储过程
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="spr"></param>
        /// <returns></returns>
        DataSet GetDataSetPro(string cmdText, List<SqlParameter> spr);

        /// <summary>
        /// 不带参数的存储过程
        /// </summary>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        DataSet GetDataSetPro(string cmdText);

        /// <summary>
        /// 带参数的文本查询
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="spr"></param>
        /// <returns></returns>
        DataSet GetDataSet(string cmdText, List<SqlParameter> spr);

        /// <summary>
        /// 不带参数的文本查询
        /// </summary>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        DataSet GetDataSet(string cmdText);

        /// <summary>
        /// 数据阅读器
        /// </summary>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="spr"></param>
        /// <returns></returns>
        IDataReader ExecuteReader(CommandType cmdType, string cmdText, List<SqlParameter> spr);

        /// <summary>
        /// 带参数的存储过程
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="spr"></param>
        /// <returns></returns>
        IDataReader ExecuteReaderPro(string cmdText, List<SqlParameter> spr);

        /// <summary>
        /// 不带带参数的存储过程
        /// </summary>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        IDataReader ExecuteReaderPro(string cmdText);


        /// <summary>
        /// 带参数的文本
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="spr"></param>
        /// <returns></returns>
        IDataReader ExecuteReader(string cmdText, List<SqlParameter> spr);

        /// <summary>
        /// 不带参数的文本
        /// </summary>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        IDataReader ExecuteReader(string cmdText);

        /// <summary>
        /// 返回第一行第一列的值
        /// </summary>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="spr"></param>
        /// <returns></returns>
        string ExecuteScalar(CommandType cmdType, string cmdText, List<SqlParameter> spr);

        /// <summary>
        /// 带参数的存储过程
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="spr"></param>
        /// <returns></returns>
        string ExecuteScalarPro(string cmdText, List<SqlParameter> spr);

        /// <summary>
        /// 不带参数的存储过程
        /// </summary>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        string ExecuteScalarPro(string cmdText);

        /// <summary>
        /// 带参数的文本
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="spr"></param>
        /// <returns></returns>
        string ExecuteScalar(string cmdText, List<SqlParameter> spr);

        /// <summary>
        /// 不带参数的文本
        /// </summary>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        string ExecuteScalar(string cmdText);

        /// <summary>
        /// 大批量数据插入
        /// </summary>
        /// <param name="tableNamelist">表名数组</param>
        /// <param name="dtlist">数据表数组</param>
        /// <param name="isPkIdentitylist">是否保留标志源</param>
        bool BatchCopyInsert(string tableName, DataTable dt, bool isPkIdentity);

       /// <summary>
       /// 大批量数据插入
       /// </summary>
       /// <param name="tableNamelist">表名数组</param>
       /// <param name="dtlist">数据表数组</param>
       /// <param name="isPkIdentitylist">是否保留标志源</param>
        bool BatchCopyInsert(List<string> tableNameList, List<DataTable> dtList, List<bool> isPkIdentitylist);
    }
}
