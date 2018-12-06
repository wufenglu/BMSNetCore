using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Xml;
using YK.Platform.Cache;
using YK.Platform.Entitys;
using YK.Platform.Unity.Extensions;
using Newtonsoft.Json;

namespace YK.Platform.Core.SqlHelper
{
    /// <summary>
    /// 链接帮助
    /// </summary>
    internal class ConnectionHelper
    {
        /// <summary>
        /// 获取链接字符串
        /// </summary>
        /// <returns></returns>
        public string GetConnectionString(string orgCode,bool isMaster = true)
        {
            var dic = GetConnectionDic(orgCode,isMaster);
            if (dic != null)
            {
                return dic["connectionstring"];
            }
            return null;
        }
        /// <summary>
        /// 获取链接字符串
        /// </summary>
        /// <returns></returns>
        public string GetConnectionString(bool isMaster = true)
        {
            return GetConnectionString(null,isMaster);
        }
        /// <summary>
        /// 获取当前租户链接字典
        /// </summary>
        /// <returns></returns>
        internal Dictionary<string, string> GetConnectionDic(string orgCode, bool isMaster = true)
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            if (!string.IsNullOrEmpty(orgCode))
            {
                List<OrganizationEntity> list = GetOrganizationEntitys();
                List<OrganizationEntity> entityList = list.Where(w => w.code.ToLower() == orgCode.ToLower()).ToList();
                if (entityList.Count() > 0)
                {
                    OrganizationEntity entity = entityList.First();
                    //使用从库，并且从库存在启用的，默认取第一个
                    if (isMaster == false && entity.slaves != null && entity.slaves.Where(w => w.state == 0).Count() > 0)
                    {
                        dic.Add("connectionstring", entity.slaves.First().connectionstring);
                        dic.Add("provider", entity.provider);
                    }
                    else
                    {
                        dic.Add("connectionstring", entity.connectionstring);
                        dic.Add("provider", entity.provider);
                    }
                    return dic;
                }
            }
            else
            {
                string configUrl = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
                string text = System.IO.File.ReadAllText(configUrl);
                ConnectionString connectionString = JsonConvert.DeserializeObject<ConnectionString>(text);
                dic.Add("connectionstring", connectionString.ConnectionStrings["DefaultConnection"].ToStr());
                dic.Add("provider", "System.Data.SqlClient");
                return dic;
            }
            return null;
        }

        /// <summary>
        /// 获取租户链接字典
        /// </summary>
        /// <returns></returns>
        internal List<Dictionary<string, string>> GetOrgConnDic()
        {
            List<Dictionary<string, string>> dicList = new List<Dictionary<string, string>>();

            List<OrganizationEntity> list = GetOrganizationEntitys();
            foreach (var item in list)
            {
                Dictionary<string, string> dic = new Dictionary<string, string>();
                dic.Add("connectionstring", item.connectionstring);
                dic.Add("provider", item.provider);
                dic.Add("code", item.code);
                dic.Add("name", item.name);
                dicList.Add(dic);
            }
            return dicList;
        }

        /// <summary>
        /// 租户实体对象
        /// </summary>
        /// <returns></returns>
        internal List<OrganizationEntity> GetOrganizationEntitys()
        {
            List<OrganizationEntity> list = (List<OrganizationEntity>)CachesHelper.Get("OrganizationsEntitys");
            //存在缓存则直接返回，否则实例化对象
            if (list == null)
            {
                list = new List<OrganizationEntity>();
            }
            else {
                return list;
            }

            string fileUrl = Path.Combine(Directory.GetCurrentDirectory() , "/App_Data/Organization.xml");
            XmlDocument xd = new XmlDocument();
            xd.Load(fileUrl);
            XmlNodeList xmlNodeList = xd.SelectSingleNode("Organizations").ChildNodes;
            //循环遍历租户
            foreach (XmlNode item in xmlNodeList)
            {
                //如果是注释节点则跳出
                if (item.NodeType == XmlNodeType.Comment)
                {
                    continue;
                }
                OrganizationEntity entity = new OrganizationEntity();

                #region 获取节点实体
                foreach (XmlNode childItem in item.ChildNodes)
                {
                    #region Tenants对象赋值
                    string value = childItem.InnerText;
                    switch (childItem.Name.ToLower())
                    {
                        case "name":
                            entity.name = value;
                            break;
                        case "code":
                            entity.code = value;
                            break;
                        case "provider":
                            entity.provider = value;
                            break;
                        case "connectionstring":
                            entity.connectionstring = value;
                            break;
                        case "state":
                            entity.state = value.ToInt();
                            break;
                        case "slaves":
                            entity.slaves = new List<OrganizationSalves>();
                            foreach (XmlNode interfaceItem in childItem.ChildNodes)
                            {
                                #region InterfacePostData实体赋值
                                OrganizationSalves model = new OrganizationSalves();
                                foreach (XmlNode interfaceChildItem in interfaceItem)
                                {
                                    switch (interfaceChildItem.Name.ToLower())
                                    {
                                        case "connectionstring":
                                            model.connectionstring = interfaceChildItem.ChildNodes[0].InnerText;
                                            break;
                                        case "proportion":
                                            model.proportion = interfaceChildItem.ChildNodes[0].InnerText.ToDecimal();
                                            break;
                                        case "state":
                                            model.state = value.ToInt();
                                            break;
                                    }
                                }
                                entity.slaves.Add(model);
                                #endregion
                            }
                            break;
                    }
                    #endregion
                }
                #endregion

                list.Add(entity);
            }
            CachesHelper.Set("OrganizationsEntitys", list);
            return list;
        }
    }

    /// <summary>
    /// 租户实体
    /// </summary>
    public class OrganizationEntity {
        /// <summary>
        /// 名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 驱动
        /// </summary>
        public string provider { get; set; }
        /// <summary>
        /// 编码
        /// </summary>
        public string code { get; set; }
        /// <summary>
        /// 数据库连接
        /// </summary>
        public string connectionstring { get; set; }
        /// <summary>
        /// 状态：0启用，1禁用
        /// </summary>
        public int state { get; set; }
        /// <summary>
        /// 从库
        /// </summary>
        public List<OrganizationSalves> slaves { get; set; }
    }
    /// <summary>
    /// 从库
    /// </summary>
    public class OrganizationSalves {
        /// <summary>
        /// 连接字符串
        /// </summary>
        public string connectionstring { get; set; }
        /// <summary>
        /// 比重
        /// </summary>
        public decimal proportion { get; set; }
        /// <summary>
        /// 状态：0启用，1禁用
        /// </summary>
        public int state { get; set; }
    }
}
