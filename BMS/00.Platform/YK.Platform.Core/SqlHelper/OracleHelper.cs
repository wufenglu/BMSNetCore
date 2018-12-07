using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace YK.Platform.Core.SqlHelper
{
    /// <summary>
    /// 数据库操作帮助类
    /// </summary>
    internal class OracleHelper : ISqlHelper
    {

        /// <summary>
        /// 链接字符串
        /// </summary>
        public string ConnectionString;

        /// <summary>
        /// 租户编码
        /// </summary>
        public string OrgCode { get; set; }

        public OracleHelper()
        {
        }

        /// <summary>
        /// 初始化设置连接字符串
        /// </summary>
        /// <param name="connectionString"></param>
        public OracleHelper(string orgCode, string connectionString)
        {
            OrgCode = orgCode;
            ConnectionString = connectionString;
        }

        /// <summary>
        /// 获取oracle参数
        /// </summary>
        /// <param name="spr"></param>
        /// <returns></returns>
        public static List<OracleParameter> GetOracleParams(List<SqlParameter> spr)
        {
            List<OracleParameter> list = new List<OracleParameter>();
            if (spr != null)
            {
                foreach (var item in spr)
                {
                    list.Add(new OracleParameter(item.ParameterName, item.Value));
                }
            }
            return list;
        }

        /// <summary>
        /// 返回数据库连接对象
        /// </summary>
        /// <returns></returns>
        public OracleConnection GetConnection(bool isMaster = true)
        {
            OracleConnection conn = new OracleConnection();
            if (conn.State == ConnectionState.Open || conn.State == ConnectionState.Broken)
            {
                conn.Close();
            }
            if (!string.IsNullOrEmpty(ConnectionString))
            {
                return new OracleConnection(ConnectionString);
            }
            else
            {
                return new OracleConnection(new ConnectionHelper().GetConnectionString(isMaster));
            }
        }

        /// <summary>
        /// 返回影响的行数
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="cmdText"></param>
        /// <param name="spr"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(CommandType commandType, string cmdText, List<SqlParameter> spr)
        {
            int i = 0;
            List<OracleParameter> listParam = GetOracleParams(spr);
            using (OracleConnection conn = GetConnection())
            {
                conn.Open();
                OracleCommand cmd = new OracleCommand(cmdText, conn);
                try
                {
                    cmd.Parameters.Clear();
                    cmd.CommandType = commandType;
                    cmd.CommandTimeout = 300;
                    if (spr != null)
                    {
                        foreach (OracleParameter s in listParam)
                        {
                            cmd.Parameters.Add(s);
                        };
                    }
                    i = cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    conn.Close();
                    cmd.Parameters.Clear();
                }
                return i;
            }
        }

        /// <summary>
        /// 带参数的存储过程
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="spr"></param>
        /// <returns></returns>
        public int ExecuteNonQueryPro(string cmdText, List<SqlParameter> spr)
        {
            return ExecuteNonQuery(CommandType.StoredProcedure, cmdText, spr);
        }

        /// <summary>
        /// 不带参数的存储过程
        /// </summary>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        public int ExecuteNonQueryPro(string cmdText)
        {
            return ExecuteNonQueryPro(cmdText, null);
        }

        /// <summary>
        /// 带参数的操作语句
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="spr"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string cmdText, List<SqlParameter> spr)
        {
            return ExecuteNonQuery(CommandType.Text, cmdText, spr);
        }

        /// <summary>
        /// 不带参数的操作语句
        /// </summary>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        public int ExecuteNonQuery(string cmdText)
        {
            return ExecuteNonQuery(cmdText, null);
        }

        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="spr"></param>
        /// <returns></returns>
        public DataSet GetDataSet(CommandType cmdType, string cmdText, List<SqlParameter> spr)
        {
            DataSet ds = new DataSet();
            List<OracleParameter> listParam = GetOracleParams(spr);
            using (OracleConnection conn = GetConnection(false))
            {
                conn.Open();
                OracleDataAdapter oda = new OracleDataAdapter(cmdText, conn);
                try
                {
                    oda.SelectCommand.CommandType = cmdType;
                    oda.SelectCommand.CommandTimeout = 300;
                    if (spr != null)
                    {
                        foreach (OracleParameter s in listParam)
                        {
                            oda.SelectCommand.Parameters.Add(s);
                        }
                    }
                    oda.Fill(ds);
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    oda.SelectCommand.Parameters.Clear();
                    conn.Close();
                }
                return ds;
            }
        }

        /// <summary>
        /// 带参数的存储过程
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="spr"></param>
        /// <returns></returns>
        public DataSet GetDataSetPro(string cmdText, List<SqlParameter> spr)
        {
            return GetDataSet(CommandType.StoredProcedure, cmdText, spr);
        }

        /// <summary>
        /// 不带参数的存储过程
        /// </summary>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        public DataSet GetDataSetPro(string cmdText)
        {
            return GetDataSetPro(cmdText, null);
        }

        /// <summary>
        /// 带参数的文本查询
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="spr"></param>
        /// <returns></returns>
        public DataSet GetDataSet(string cmdText, List<SqlParameter> spr)
        {
            return GetDataSet(CommandType.Text, cmdText, spr);
        }

        /// <summary>
        /// 不带参数的文本查询
        /// </summary>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        public DataSet GetDataSet(string cmdText)
        {
            return GetDataSet(CommandType.Text, cmdText, null);
        }

        /// <summary>
        /// 数据阅读器
        /// </summary>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="spr"></param>
        /// <returns></returns>
        public IDataReader ExecuteReader(CommandType cmdType, string cmdText, List<SqlParameter> spr)
        {
            List<OracleParameter> listParam = GetOracleParams(spr);
            OracleConnection conn = GetConnection(false);
            conn.Open();
            OracleCommand cmd = new OracleCommand(cmdText, conn);
            try
            {
                cmd.CommandType = cmdType;
                cmd.CommandTimeout = 300;
                if (spr != null)
                {
                    foreach (OracleParameter s in listParam)
                    {
                        cmd.Parameters.Add(s);
                    }
                }
                OracleDataReader dr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                return dr;
            }
            catch (Exception ex)
            {
            }
            finally
            {
                cmd.Parameters.Clear();
            }
            return null;
        }

        /// <summary>
        /// 带参数的存储过程
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="spr"></param>
        /// <returns></returns>
        public IDataReader ExecuteReaderPro(string cmdText, List<SqlParameter> spr)
        {
            return ExecuteReader(CommandType.StoredProcedure, cmdText, spr);
        }

        /// <summary>
        /// 不带带参数的存储过程
        /// </summary>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        public IDataReader ExecuteReaderPro(string cmdText)
        {
            return ExecuteReaderPro(cmdText, null);
        }


        /// <summary>
        /// 带参数的文本
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="spr"></param>
        /// <returns></returns>
        public IDataReader ExecuteReader(string cmdText, List<SqlParameter> spr)
        {
            return ExecuteReader(CommandType.Text, cmdText, spr);
        }

        /// <summary>
        /// 不带参数的文本
        /// </summary>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        public IDataReader ExecuteReader(string cmdText)
        {
            return ExecuteReader(cmdText, null);
        }

        /// <summary>
        /// 返回第一行第一列的值
        /// </summary>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="spr"></param>
        /// <returns></returns>
        public string ExecuteScalar(CommandType cmdType, string cmdText, List<SqlParameter> spr)
        {
            List<OracleParameter> listParam = GetOracleParams(spr);
            using (OracleConnection conn = GetConnection(false))
            {
                conn.Open();
                OracleCommand cmd = new OracleCommand(cmdText, conn);
                try
                {
                    cmd.CommandType = cmdType;
                    cmd.CommandTimeout = 300;
                    if (spr != null)
                    {
                        foreach (OracleParameter s in listParam)
                        {
                            cmd.Parameters.Add(s);
                        }
                    }
                    if (cmd.ExecuteScalar() != null)
                    {
                        return cmd.ExecuteScalar().ToString();
                    }
                }
                catch (Exception ex)
                {
                }
                finally
                {
                    cmd.Parameters.Clear();
                    conn.Close();
                }
                return null;
            }
        }

        /// <summary>
        /// 带参数的存储过程
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="spr"></param>
        /// <returns></returns>
        public string ExecuteScalarPro(string cmdText, List<SqlParameter> spr)
        {
            return ExecuteScalar(CommandType.StoredProcedure, cmdText, spr);
        }

        /// <summary>
        /// 不带参数的存储过程
        /// </summary>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        public string ExecuteScalarPro(string cmdText)
        {
            return ExecuteScalarPro(cmdText, null);
        }

        /// <summary>
        /// 带参数的文本
        /// </summary>
        /// <param name="cmdText"></param>
        /// <param name="spr"></param>
        /// <returns></returns>
        public string ExecuteScalar(string cmdText, List<SqlParameter> spr)
        {
            return ExecuteScalar(CommandType.Text, cmdText, spr);
        }

        /// <summary>
        /// 不带参数的文本
        /// </summary>
        /// <param name="cmdText"></param>
        /// <returns></returns>
        public string ExecuteScalar(string cmdText)
        {
            return ExecuteScalar(cmdText, null);
        }

        /// <summary>
        /// 大批量数据插入
        /// </summary>
        /// <param name="tableNamelist">表名数组</param>
        /// <param name="dtlist">数据表数组</param>
        /// <param name="isPkIdentitylist">是否保留标志源</param>
        public bool BatchCopyInsert(string tableName, DataTable dt, bool isPkIdentity)
        {
            return false;
        }
        /// <summary>
        /// 大批量数据插入
        /// </summary>
        /// <param name="tableNamelist">表名数组</param>
        /// <param name="dtlist">数据表数组</param>
        /// <param name="isPkIdentitylist">是否保留标志源</param>
        public bool BatchCopyInsert(List<string> tableNameList, List<DataTable> dtList, List<bool> isPkIdentitylist)
        {
            return false;
        }
    }
}
