using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Web;

namespace YK.Platform.Cache
{
    /// <summary>
    /// 业务缓存处理类
    /// </summary>
    public class BusinessCachesHelper<T> 
    {
        /// <summary>
        /// 缓存列表名称
        /// </summary>
        private static string cacheListName {
            get {
                Type type = typeof(T);
                return type.Name + "_list";
            }
        }

        /// <summary>
        /// 缓存名称
        /// </summary>
        private static string cacheName
        {
            get
            {
                Type type = typeof(T);
                return type.Name;
            }
        }

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="cacheName">缓存名称</param>
        /// <param name="entity">缓存值</param>
        /// <returns></returns>
        public static void AddEntityCache(object id,T entity)
        {
            string thisCacheName = cacheName + "_" + id.ToString();
            CachesHelper.Set(thisCacheName, entity);

            AddCacheNames(thisCacheName);
        }        

        /// <summary>
        /// 添加缓存
        /// </summary>
        /// <param name="cacheName">缓存名称</param>
        /// <param name="entity">缓存值</param>
        /// <returns></returns>
        public static void RemoveEntityCache(object id)
        {
            string thisCacheName = cacheName + "_" + id.ToString();
            CachesHelper.Remove(thisCacheName);

            AddCacheNames(thisCacheName, false);
        }

        /// <summary>
        /// 添加缓存名称至缓存名称列表
        /// </summary>
        /// <param name="thisCacheName"></param>
        /// <param name="isAdd">是否为添加缓存值，true为添加，false为移除</param>
        private static void AddCacheNames(string thisCacheName, bool isAdd = true)
        {
            List<string> list = CachesHelper.Get(cacheListName) as List<string>;
            if (list == null)
            {
                list = new List<string>();
            }
            if (list.Contains(thisCacheName))
            {
                if (isAdd == false)
                    list.Remove(thisCacheName);
            }
            else
            {
                if (isAdd)
                    list.Add(thisCacheName);
            }
            CachesHelper.Set(cacheListName, list);
        }

        /// <summary>
        /// 获取所有实体
        /// </summary>
        /// <returns></returns>
        public static List<T> GetAllEntityCache()
        {
            List<string> names = CachesHelper.Get(cacheListName) as List<string>;
            if (names == null) {
                return null;
            }
            List<T> list = new List<T>();
            foreach (string name in names)
            {
                var value = CachesHelper.Get(name);
                T entity = (T)value;
                list.Add(entity);
            }
            return list;
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static T GetEntityCache(object id)
        {
            string thisCacheName = cacheName + "_" + id.ToString();
            return (T)CachesHelper.Get(thisCacheName);
        }
    }
}