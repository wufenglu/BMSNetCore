using System;
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;
using System.Reflection;
using YK.Platform.Core.Model;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace YK.Platform.Core
{
    /// <summary>
    /// 描述属性帮助类
    /// </summary>
    public class AttributeHelper
    {
        /// <summary>
        /// 获取泛型的描述
        /// </summary>
        /// <returns></returns>
        public static List<string> GetTEntityDescriptions<TEntity>() where TEntity : class, new()
        {
            TEntity entity = new TEntity();
            List<string> list = new List<string>();
            object[] obj = entity.GetType().GetCustomAttributes(typeof(DescriptionAttribute), false);
            foreach (var s in obj)
            {
                DescriptionAttribute attr = s as DescriptionAttribute;
                list.Add(attr.Description);
            }
            return list;
        }

        /// <summary>
        /// 获取泛型属性的描述
        /// </summary>
        /// <param name="text">内容</param>
        public static IDictionary<string, string> GetPropertyDescriptions<TEntity>() where TEntity : class, new()
        {
            TEntity entity = new TEntity();
            IDictionary<string, string> idc = new Dictionary<string, string>();
            foreach (PropertyInfo prop in entity.GetType().GetProperties())
            {
                object[] objs = prop.GetCustomAttributes(typeof(DescriptionAttribute), false);
                string value = "";
                if (objs.Length > 0)
                {
                    DescriptionAttribute description = objs[0] as DescriptionAttribute;
                    value = description.Description;
                }
                idc.Add(prop.Name,value);
            }
            return idc;
        }

        /// <summary>
        /// 获取枚举的项和项的描述
        /// </summary>
        /// <returns></returns>
        public static IDictionary<string, string> GetEnumDescriptions<TEnum>()
        {
            IDictionary<string, string> idc = new Dictionary<string, string>();

            Type data = typeof(TEnum);
            //获取各项
            Array Arrays = Enum.GetValues(data);
            for (int i = 0; i < Arrays.LongLength; i++)
            {
                string key = Arrays.GetValue(i).ToString();
                //获取描述
                object[] objs = data.GetField(key).GetCustomAttributes(typeof(DescriptionAttribute), false);
                string value = "";
                if (objs.Length > 0)
                {
                    DescriptionAttribute attr = objs[0] as DescriptionAttribute;
                    value = attr.Description;
                }
                idc.Add(key,value);
            }
            return idc;
        }

        /// <summary>
        /// 获取实体的列特性
        /// </summary>
        /// <returns></returns>
        public static List<EntityPropColumnAttributes> GetEntityColumnAtrributes<TEntity>() where TEntity : class, new()
        {
            List<EntityPropColumnAttributes> list = new List<EntityPropColumnAttributes>();
            TEntity model = new TEntity();
            foreach (PropertyInfo prop in model.GetType().GetProperties())
            {
                EntityPropColumnAttributes entity = new EntityPropColumnAttributes();
                entity.propName = prop.Name;
                entity.fieldName = prop.Name;
                entity.isPrimaryKey = false;
                entity.isDbGenerated = false;
                Type type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                entity.typeName = type.FullName;

                ColumnAttribute columnAttribute = prop.GetCustomAttribute<ColumnAttribute>();
                if (columnAttribute != null) {
                    entity.fieldName = columnAttribute.Name;
                }

                KeyAttribute keyAttribute = prop.GetCustomAttribute<KeyAttribute>();
                if (keyAttribute != null)
                {
                    entity.isPrimaryKey = true;
                }

                DatabaseGeneratedAttribute databaseGeneratedAttribute = prop.GetCustomAttribute<DatabaseGeneratedAttribute>();
                if (databaseGeneratedAttribute != null)
                {
                    entity.isDbGenerated = databaseGeneratedAttribute.DatabaseGeneratedOption == DatabaseGeneratedOption.Identity ? true : false;
                }
                list.Add(entity);
            }
            return list;
        }

        /// <summary>
        /// 获取实体的特性
        /// </summary>
        /// <returns></returns>
        public static string GetEntityTableAtrributes<TEntity>() where TEntity : class, new()
        {
            TEntity model = new TEntity();
            object[] attrs = model.GetType().GetCustomAttributes(typeof(TableAttribute), false);
            if (attrs.Count() > 0)
            {
                TableAttribute attr = attrs[0] as TableAttribute;
                return attr.Name;
            }
            return model.GetType().Name;
        }
    }
}
