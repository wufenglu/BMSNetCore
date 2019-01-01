using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

using Oracle.DataAccess.Client;
using MySql.Data.MySqlClient;
using System.Configuration;
using YK.Platform.Core.Model;

namespace YK.Platform.Core.SqlHelper
{
    /// <summary>
    /// SQL转换
    /// </summary>
    public class SqlConvertHelper
    {
        /// <summary>
        /// 获取实例
        /// </summary>
        /// <returns></returns>
        public static ISqlHelper GetInstallSqlHelper(string orgCode = null, string connectionString = null)
        {
            OrganizationEntity organizationEntity = new ConnectionHelper().GetConnectionDic(orgCode);
            switch (organizationEntity.Provider)
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
