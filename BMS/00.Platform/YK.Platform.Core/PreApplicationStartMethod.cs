using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace YK.Platform.Core
{
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    public sealed class PreApplicationStartMethodAttribute : Attribute
    {
        /// <summary>
        /// 应用程序开始时执行属性方法
        /// </summary>
        /// <param name="type"></param>
        /// <param name="methodName"></param>
        public PreApplicationStartMethodAttribute(Type type, string methodName) {
            if (type == null)
            {
                throw new ArgumentNullException("type");
            }
            if (string.IsNullOrEmpty(methodName))
            {
                throw new ArgumentNullException("methodName");
            }

            MethodInfo method = type.GetMethod(methodName);
            method.Invoke(null,null);
        }
    }

}
