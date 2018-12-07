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
    /// 租户SQL执行
    /// </summary>
    public static class TenantSqlHelper
    {
        private static List<SqlParameter> sqlserverParamList;
        private static List<MySqlParameter> mysqlParamList;
        private static List<OracleParameter> oraclParamList;

        /// <summary>
        /// 获取参数
        /// </summary>
        /// <param name="spr"></param>
        public static void GetParameters(Dictionary<string, string> dic,List<SqlParameter> spr)
        {
            sqlserverParamList = new List<SqlParameter>();
            mysqlParamList = new List<MySqlParameter>();
            oraclParamList = new List<OracleParameter>();

            switch (dic["provider"])
            {
                case "System.Data.SqlClient":
                    sqlserverParamList = spr;
                    break;
                case "MySql.Data.MySqlClient":
                    foreach (var item in spr)
                    {
                        mysqlParamList.Add(new MySqlParameter(item.ParameterName, item.Value));
                    }
                    break;
                case "System.Data.OracleClient":
                    foreach (var item in spr)
                    {
                        oraclParamList.Add(new OracleParameter(item.ParameterName, item.Value));
                    }
                    break;
            }
        }
        
        /// <summary>
        /// 返回影响的行数
        /// </summary>
        /// <param name="commandType"></param>
        /// <param name="cmdText"></param>
        /// <param name="spr"></param>
        /// <returns></returns>
        public static  List<OrganizationExecuteResult> ExecuteNonQuery(CommandType commandType, string cmdText, List<SqlParameter> spr)
        {
            List<OrganizationExecuteResult> result = new List<OrganizationExecuteResult>();
            var organizationEntitys = new ConnectionHelper().GetOrganizationEntitys();
            foreach (var organizationEntity in organizationEntitys)
            {
                OrganizationExecuteResult organizationExecuteResult = new OrganizationExecuteResult();
                organizationExecuteResult.Code = organizationEntity.Code;
                organizationExecuteResult.Name = organizationEntity.Name;

                ISqlHelper sqlHelper = SqlConvertHelper.GetInstallSqlHelper(organizationEntity.Connectionstring);
                organizationExecuteResult.Data = sqlHelper.ExecuteNonQuery(commandType, cmdText, spr);

                result.Add(organizationExecuteResult);
            }
            return result;
        }        
        
        /// <summary>
        /// 获取数据
        /// </summary>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="spr"></param>
        /// <returns></returns>
        public static  List<OrganizationExecuteResult> GetDataSet(CommandType cmdType, string cmdText, List<SqlParameter> spr)
        {
            List<OrganizationExecuteResult> result = new List<OrganizationExecuteResult>();
            var organizationEntitys = new ConnectionHelper().GetOrganizationEntitys();
            foreach (var organizationEntity in organizationEntitys)
            {
                OrganizationExecuteResult organizationExecuteResult = new OrganizationExecuteResult();
                organizationExecuteResult.Code = organizationEntity.Code;
                organizationExecuteResult.Name = organizationEntity.Name;

                ISqlHelper sqlHelper = SqlConvertHelper.GetInstallSqlHelper(organizationEntity.Connectionstring);
                organizationExecuteResult.Data = sqlHelper.GetDataSet(cmdType,cmdText, spr);

                result.Add(organizationExecuteResult);
            }
            return result;
        }
        
        /// <summary>
        /// 数据阅读器
        /// </summary>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="spr"></param>
        /// <returns></returns>
        public static  List<OrganizationExecuteResult> ExecuteReader(CommandType cmdType, string cmdText, List<SqlParameter> spr)
        {
            List<OrganizationExecuteResult> result = new List<OrganizationExecuteResult>();
            var organizationEntitys = new ConnectionHelper().GetOrganizationEntitys();
            foreach (var organizationEntity in organizationEntitys)
            {
                OrganizationExecuteResult organizationExecuteResult = new OrganizationExecuteResult();
                organizationExecuteResult.Code = organizationEntity.Code;
                organizationExecuteResult.Name = organizationEntity.Name;

                ISqlHelper sqlHelper = SqlConvertHelper.GetInstallSqlHelper(organizationEntity.Connectionstring);
                organizationExecuteResult.Data = sqlHelper.ExecuteReader(cmdType,cmdText, spr);

                result.Add(organizationExecuteResult);
            }
            return result;
        }
        
        /// <summary>
        /// 返回第一行第一列的值
        /// </summary>
        /// <param name="cmdType"></param>
        /// <param name="cmdText"></param>
        /// <param name="spr"></param>
        /// <returns></returns>
        public static  List<OrganizationExecuteResult> ExecuteScalar(CommandType cmdType, string cmdText, List<SqlParameter> spr)
        {
            List<OrganizationExecuteResult> result = new List<OrganizationExecuteResult>();
            var organizationEntitys = new ConnectionHelper().GetOrganizationEntitys();
            foreach (var organizationEntity in organizationEntitys)
            {
                OrganizationExecuteResult organizationExecuteResult = new OrganizationExecuteResult();
                organizationExecuteResult.Code = organizationEntity.Code;
                organizationExecuteResult.Name = organizationEntity.Name;

                ISqlHelper sqlHelper = SqlConvertHelper.GetInstallSqlHelper(organizationEntity.Connectionstring);
                organizationExecuteResult.Data = sqlHelper.ExecuteScalar(cmdType, cmdText, spr);

                result.Add(organizationExecuteResult);
            }
            return result;
        }
        
        /// <summary>
        /// 大批量数据插入
        /// </summary>
        /// <param name="tableNamelist">表名数组</param>
        /// <param name="dtlist">数据表数组</param>
        /// <param name="isPkIdentitylist">是否保留标志源</param>
        public static List<OrganizationExecuteResult> BatchCopyInsert(string tableName, DataTable dt, bool isPkIdentity)
        {
            List<OrganizationExecuteResult> result = new List<OrganizationExecuteResult>();
            var organizationEntitys = new ConnectionHelper().GetOrganizationEntitys();
            foreach (var organizationEntity in organizationEntitys)
            {
                OrganizationExecuteResult organizationExecuteResult = new OrganizationExecuteResult();
                organizationExecuteResult.Code = organizationEntity.Code;
                organizationExecuteResult.Name = organizationEntity.Name;

                var sqlHelper = new SqlHelper();
                organizationExecuteResult.Data = sqlHelper.BatchCopyInsert(tableName,dt,isPkIdentity);
                result.Add(organizationExecuteResult);
            }
            return result;
        }

        /// <summary>
        /// 大批量数据插入
        /// </summary>
        /// <param name="tableNamelist">表名数组</param>
        /// <param name="dtlist">数据表数组</param>
        /// <param name="isPkIdentitylist">是否保留标志源</param>
        public static List<OrganizationExecuteResult> BatchCopyInsert(List<string> tableNameList, List<DataTable> dtList, List<bool> isPkIdentitylist)
        {
            List<OrganizationExecuteResult> result = new List<OrganizationExecuteResult>();
            var organizationEntitys = new ConnectionHelper().GetOrganizationEntitys();
            foreach (var organizationEntity in organizationEntitys)
            {
                OrganizationExecuteResult organizationExecuteResult = new OrganizationExecuteResult();
                organizationExecuteResult.Code = organizationEntity.Code;
                organizationExecuteResult.Name = organizationEntity.Name;

                var sqlHelper = new SqlHelper();
                organizationExecuteResult.Data = sqlHelper.BatchCopyInsert(tableNameList, dtList, isPkIdentitylist);
                result.Add(organizationExecuteResult);
            }
            return result;
        }
    }
}
