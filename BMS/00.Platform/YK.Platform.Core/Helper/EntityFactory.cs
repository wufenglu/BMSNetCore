using System;
using System.Linq;
using System.Text;
using System.IO;
using System.Reflection;
using YK.Platform.Cache;
using System.CodeDom.Compiler;
using Microsoft.CodeAnalysis;
using System.Collections.Generic;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Emit;
using System.Runtime.Loader;

namespace YK.Platform.Core.Helper
{
    /// <summary>
    /// AOP代理类
    /// </summary>
    public class EntityFactory
    {
        /// <summary>
        /// 代理类模板地址
        /// </summary>
        private static string TemplateUrl = Directory.GetCurrentDirectory() + @"\Proxy\ProxyTemp.txt";
        /// <summary>
        /// 代理类内容
        /// </summary>
        private static string Content { get; set; }
        /// <summary>
        /// 获取模板内容
        /// </summary>
        /// <returns></returns>
        private static string GetTemplate()
        {
            if (!string.IsNullOrEmpty(Content))
            {
                return Content;
            }

            Object thisLock = new Object();
            lock (thisLock)
            {
                if (!string.IsNullOrEmpty(Content))
                {
                    return Content;
                }
                Content = File.ReadAllText(TemplateUrl);
            }

            return Content;
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <typeparam name="Tentity"></typeparam>
        /// <returns></returns>
        public static Tentity New<Tentity>() where Tentity : class, new()
        {
            Tentity result = new Tentity();
            Type entityType = typeof(Tentity);

            //文件名为源类的命名空间+类名
            string fileName = (entityType.Namespace + "." + entityType.Name).Replace(".", "_");
            //代理类的全称
            string namespaceName = "Proxy.GenerateClass." + entityType.Namespace.Replace(".", "_");
            string loadClassFullName = namespaceName + "." + entityType.Name;
            object cacheValue = CachesHelper.Get(loadClassFullName);
            if (cacheValue != null)
            {
                return (Tentity)System.Activator.CreateInstance(cacheValue.GetType());
            }

            string generateCode = GenerateCode<Tentity>(fileName, namespaceName);
            result = GetTentity<Tentity>(generateCode, loadClassFullName);
            CachesHelper.Set(loadClassFullName, result);

            return (Tentity)System.Activator.CreateInstance(result.GetType());
        }

        /// <summary>
        /// 生成代码
        /// </summary>
        /// <typeparam name="Tentity"></typeparam>
        /// <param name="fileName"></param>
        /// <param name="namespaceName"></param>
        /// <returns></returns>
        private static string GenerateCode<Tentity>(string fileName,string namespaceName) where Tentity : class, new()
        {
            Type entityType = typeof(Tentity);
            string fullClassName = entityType.Namespace + "." + entityType.Name;
            string filePath = Directory.GetCurrentDirectory() + @"\Proxy\ProxyClass\" + fileName + ".cs";
            if (File.Exists(filePath))
            {
                return File.ReadAllText(filePath);
            }

            //替换模板
            string templateContent = GetTemplate();
            templateContent = templateContent.Replace("#namespace", namespaceName);
            templateContent = templateContent.Replace("#class", entityType.Name + ":" + fullClassName);

            //重写属性
            StringBuilder content = new StringBuilder();
            PropertyInfo[] propertyInfos = entityType.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.Default);
            foreach (PropertyInfo prop in propertyInfos)
            {
                // The the type of the property
                string propertyType = prop.PropertyType.Name;

                //https://blog.csdn.net/apollokk/article/details/76708225
                // We need to check whether the property is NULLABLE
                if (prop.PropertyType.IsGenericType && prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    // If it is NULLABLE, then get the underlying type. eg if "Nullable<int>" then this will return just "int"
                    Type columnType = prop.PropertyType.GetGenericArguments()[0];
                    propertyType = string.Format("Nullable<{0}>", columnType.Name);
                }

                content.Append(string.Format("public override {0} {1} ", propertyType, prop.Name));
                content.Append(Environment.NewLine);
                content.Append("{");
                content.Append(Environment.NewLine);
                content.Append("get { if (ChanageProperty.ContainsKey(\"" + prop.Name + "\") == false) { return default(" + propertyType + "); } else { return (" + propertyType + ")ChanageProperty[\"" + prop.Name + "\"]; } } ");
                content.Append(Environment.NewLine);
                content.Append("set { ChanageProperty.Add(\"" + prop.Name + "\",value); } ");
                content.Append(Environment.NewLine);
                content.Append("}");
                content.Append(Environment.NewLine);
            }

            //替换写入内容
            templateContent = templateContent.Replace("#content", content.ToString());

            Object thisLock = new Object();
            lock (thisLock)
            {
                File.WriteAllText(filePath, templateContent);
            }

            return templateContent;
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <typeparam name="Tentity"></typeparam>
        /// <param name="code"></param>
        /// <param name="loadClassFullName"></param>
        /// <returns></returns>
        private static Tentity GetTentity<Tentity>(string code, string loadClassFullName) where Tentity : class, new()
        {
            Tentity entity = new Tentity();

            //缓存程序集依赖
            var references = new List<MetadataReference>();
            var refAsmFiles = new List<string>();
            var refAssemblyList = entity.GetType().Assembly.GetReferencedAssemblies();

            //系统依赖
            var sysRefLocation = typeof(Enumerable).GetTypeInfo().Assembly.Location;
            refAsmFiles.Add(sysRefLocation);

            //refAsmFiles原本缓存的程序集依赖
            refAsmFiles.Add(typeof(object).GetTypeInfo().Assembly.Location);
            refAsmFiles.AddRange(refAssemblyList.Select(t => Assembly.Load(t).Location).Distinct().ToList());

            //传统.NetFramework 需要添加mscorlib.dll
            var coreDir = Directory.GetParent(sysRefLocation);
            var mscorlibFile = coreDir.FullName + Path.DirectorySeparatorChar + "mscorlib.dll";
            if (File.Exists(mscorlibFile))
            {
                references.Add(MetadataReference.CreateFromFile(mscorlibFile));
            }

            var apiAsms = refAsmFiles.Select(t => MetadataReference.CreateFromFile(t)).ToList();
            references.AddRange(apiAsms);

            //当前程序集依赖
            var thisAssembly = Assembly.GetEntryAssembly();
            if (thisAssembly != null)
            {
                var referencedAssemblies = thisAssembly.GetReferencedAssemblies();
                foreach (var referencedAssembly in referencedAssemblies)
                {
                    var loadedAssembly = Assembly.Load(referencedAssembly);
                    references.Add(MetadataReference.CreateFromFile(loadedAssembly.Location));
                }
            }

            var tree = SyntaxFactory.ParseSyntaxTree(code);
            var compilation = CSharpCompilation.Create(loadClassFullName)
              .WithOptions(new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary))
              .AddReferences(references)
              .AddSyntaxTrees(tree);

            //定义编译后文件名
            var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Proxy");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var apiRemoteProxyDllFile = Path.Combine(path, loadClassFullName + DateTime.Now.ToString("yyyyMMddHHmmssfff") + ".dll");

            //执行编译
            EmitResult compilationResult = compilation.Emit(apiRemoteProxyDllFile);
            if (compilationResult.Success)
            {
                Assembly apiRemoteAsm = AssemblyLoadContext.Default.LoadFromAssemblyPath(apiRemoteProxyDllFile);
                return (Tentity)apiRemoteAsm.CreateInstance(loadClassFullName);
            }
            return null;
        }
    }
}
