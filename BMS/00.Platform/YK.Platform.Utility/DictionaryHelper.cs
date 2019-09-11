using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace YK.Platform.Utility
{
    /// <summary>
    /// 字典帮助类
    /// </summary>
    public class DictionaryHelper
    {
        /// <summary>
        /// 实体转换为字典
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static Dictionary<string, object> GetDictionaryByEntity<TEntity>(TEntity entity) where TEntity : class
        {
            var dict = new Dictionary<string, object>();
            foreach (PropertyInfo prop in entity.GetType().GetProperties()) {
                dict.Add(prop.Name,prop.GetValue(entity));
            }
            return dict;
        }
    }
}
