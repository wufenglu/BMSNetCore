using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Xml;
using YK.Platform.Cache;
using YK.Platform.Entitys;
using YK.Platform.Unity.Extensions;
using Newtonsoft.Json;
using YK.Platform.Core.Model;

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
            var organization = GetConnectionDic(orgCode,isMaster);
            if (organization != null)
            {
                return organization.Connectionstring;
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
        internal OrganizationEntity GetConnectionDic(string orgCode, bool isMaster = true)
        {
            OrganizationEntity organization = new OrganizationEntity();
            if (!string.IsNullOrEmpty(orgCode))
            {
                List<OrganizationEntity> list = GetOrganizationEntitys();
                List<OrganizationEntity> entityList = list.Where(w => w.Code.ToLower() == orgCode.ToLower()).ToList();
                if (entityList.Count() > 0)
                {
                    OrganizationEntity entity = entityList.First();
                    //使用从库，并且从库存在启用的，默认取第一个
                    if (isMaster == false && entity.Slaves != null && entity.Slaves.Where(w => w.State == 0).Count() > 0)
                    {
                        organization.Connectionstring = entity.Slaves.First().Connectionstring;
                        organization.Provider = entity.Provider;
                    }
                    else
                    {
                        organization.Connectionstring = entity.Connectionstring;
                        organization.Provider = entity.Provider;
                    }
                    return organization;
                }
            }
            else
            {
                string configUrl = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
                string text = System.IO.File.ReadAllText(configUrl);
                ConnectionString connectionString = JsonConvert.DeserializeObject<ConnectionString>(text);
                if (connectionString == null)
                {
                    return null;
                }

                organization.Connectionstring = connectionString.ConnectionStrings["DefaultConnection"];
                organization.Provider = "System.Data.SqlClient";
                if (organization.Connectionstring == null)
                {
                    return null;
                }

                return organization;
            }
            return null;
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
                            entity.Name = value;
                            break;
                        case "code":
                            entity.Code = value;
                            break;
                        case "provider":
                            entity.Provider = value;
                            break;
                        case "connectionstring":
                            entity.Connectionstring = value;
                            break;
                        case "state":
                            entity.State = value.ToInt();
                            break;
                        case "slaves":
                            entity.Slaves = new List<OrganizationSalves>();
                            foreach (XmlNode interfaceItem in childItem.ChildNodes)
                            {
                                #region InterfacePostData实体赋值
                                OrganizationSalves model = new OrganizationSalves();
                                foreach (XmlNode interfaceChildItem in interfaceItem)
                                {
                                    switch (interfaceChildItem.Name.ToLower())
                                    {
                                        case "connectionstring":
                                            model.Connectionstring = interfaceChildItem.ChildNodes[0].InnerText;
                                            break;
                                        case "proportion":
                                            model.Proportion = interfaceChildItem.ChildNodes[0].InnerText.ToDecimal();
                                            break;
                                        case "state":
                                            model.State = value.ToInt();
                                            break;
                                    }
                                }
                                entity.Slaves.Add(model);
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
}
