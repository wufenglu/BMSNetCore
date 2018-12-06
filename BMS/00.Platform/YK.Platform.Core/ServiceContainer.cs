using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace YK.Platform.Core
{
    /// <summary>
    /// 服务容器
    /// </summary>
    public static class ServiceContainer
    {
        /// <summary>
        /// 容器实体列表
        /// </summary>
        private static List<ContainerEntity> ContainerList = new List<ContainerEntity>();
        /// <summary>
        /// 注册
        /// </summary>
        /// <typeparam name="Service"></typeparam>
        /// <typeparam name="Interface"></typeparam>
        /// <param name="alias"></param>
        public static void Register<Service, Interface>(string alias = null) where Interface : class
        {
            Type serviceType = typeof(Service);
            Type interfaceType = typeof(Interface);

            ContainerEntity entity = new ContainerEntity();
            entity.Alias = alias;
            entity.InterfaceAssemblyFullName = interfaceType.FullName;
            entity.ServiceAssembly = Assembly.GetAssembly(serviceType).FullName;
            entity.ServiceAssemblyFullName = serviceType.FullName;

            ContainerList.Add(entity);
        }

        /// <summary>
        /// 获取服务
        /// </summary>
        /// <typeparam name="Interface"></typeparam>
        /// <param name="alias"></param>
        /// <returns></returns>
        public static Interface GetService<Interface>(string alias = null) where Interface : class
        {
            Type interfaceType = typeof(Interface);
            List<ContainerEntity> list = ContainerList.Where(w => w.InterfaceAssemblyFullName == interfaceType.FullName && w.Alias == alias).ToList();
            return Assembly.Load(list.First().ServiceAssembly).CreateInstance(list.First().ServiceAssemblyFullName) as Interface;
        }
    }

    /// <summary>
    /// 容器实体
    /// </summary>
    internal class ContainerEntity
    {
        /// <summary>
        /// 别名，用于一个接口多个实现类
        /// </summary>
        public string Alias { get; set; }
        /// <summary>
        /// 接口程序集完整名称
        /// </summary>
        public string InterfaceAssemblyFullName { get; set; }
        /// <summary>
        /// 服务程序集
        /// </summary>
        public string ServiceAssembly { get; set; }
        /// <summary>
        /// 服务程序集完整名称
        /// </summary>
        public string ServiceAssemblyFullName { get; set; }
    }
}
