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
    /// ��ҳ
    /// </summary>
    public class Pager
    {
        public static IPager getInstance(string orgCode = null, string connectionString = null) { 
            var organizationEntity = new ConnectionHelper().GetConnectionDic(orgCode);
            switch (organizationEntity.Provider)
            {
                case "System.Data.SqlClient":
                    return new SqlPager();
                    break;
                case "MySql.Data.MySqlClient":
                    return new MySqlPager();
                    break;
                case "System.Data.OracleClient":
                    return new OraclPager();
                    break;
            }
            return null;
        }        
    }
}
