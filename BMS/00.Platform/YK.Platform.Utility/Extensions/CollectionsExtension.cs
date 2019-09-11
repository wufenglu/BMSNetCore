using System;
using System.Collections.Generic;

using System.Data;
using System.Linq;
using System.Reflection;

using Newtonsoft.Json;

namespace YK.Platform.Utility.Extensions
{
    /// <summary>
    /// 扩展方法
    /// </summary>
    public static class CollectionsExtension
    {
        /// <summary>
        /// 判断对象是否为空
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> list)
        {
            return list == null || list.Count() <= 0;
        }

        /// <summary>
        /// 分页
        /// </summary>
        /// <param name="list">Guid集合</param>
        /// <param name="pageSize">分页条数</param>
        /// <returns>返回string集合</returns>
        public static List<List<TEntity>> Page<TEntity>(this IEnumerable<TEntity> list, int pageSize = 1000)
        {
            List<List<TEntity>> result = new List<List<TEntity>>();
            int page = list.Count() % pageSize > 0 ? (list.Count() / pageSize + 1) : (list.Count() / pageSize);
            for (int i = 0; i < page; i++)
            {
                List<TEntity> objects = list.Skip(pageSize * i).Take(pageSize).ToList();
                result.Add(objects);
            }
            return result;
        }
    }
}
