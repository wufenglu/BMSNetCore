using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

using Oracle.DataAccess.Client;
using MySql.Data.MySqlClient;
using System.Configuration;

namespace YK.Platform.Core.SqlHelper
{
    /// <summary>
    /// SQL转换
    /// </summary>
    public class SqlConvertHelper
    {
        /// <summary>
        /// 获取mysql参数
        /// </summary>
        /// <param name="spr"></param>
        /// <returns></returns>
        public static List<MySqlParameter> GetMySqlParams(List<SqlParameter> spr) {
            List<MySqlParameter> list = new List<MySqlParameter>();
            if (spr != null)
            {
                foreach (var item in spr)
                {
                    list.Add(new MySqlParameter(item.ParameterName, item.Value));
                }
            }
            return list;
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
        /// 获取实例
        /// </summary>
        /// <returns></returns>
        public static ISqlHelper GetInstallSqlHelper(string orgCode=null, string connectionString=null)
        {
            var connDic = new ConnectionHelper().GetConnectionDic(orgCode);
            switch (connDic["provider"])
            {
                case "System.Data.SqlClient":
                    return new SqlHelper(orgCode, connectionString);
                    break;
                case "MySql.Data.MySqlClient":
                    return new MySqlHelper(orgCode, connectionString);
                    break;
                case "System.Data.OracleClient":
                    return new OracleHelper(orgCode, connectionString);
                    break;
            }
            return new SqlHelper();
        }     
    }
}
