using System;
using System.Xml.Serialization;
using System.Text;
using System.Xml;
using System.IO;

namespace YK.Platform.Unity
{

    /// <summary>
    ///XmlSerializer 的摘要说明
    /// </summary>
    public class MyXmlSerializer<TEntity>
    {
        public MyXmlSerializer()
        {
        }
        //
        //TODO: 在此处添加构造函数逻辑
        //
        private static TEntity model = default(TEntity);

        /// <summary>
        /// 反序列化
        /// </summary>
        /// <param name="url">文件路径</param>
        /// <returns></returns>
        public static TEntity Get(string url)
        {
            if (model == null)
            {
                FileStream fs = null;
                try
                {
                    XmlSerializer xs = new XmlSerializer(typeof(TEntity));
                    fs = new FileStream(url, FileMode.Open, FileAccess.Read);
                    model = (TEntity)xs.Deserialize(fs);
                    fs.Close();
                    return model;
                }
                catch
                {
                    if (fs != null)
                        fs.Close();
                    throw new Exception("Xml deserialization failed!");
                }

            }
            else
            {
                return model;
            }
        }

        /// <summary>
        /// 序列化
        /// </summary>
        /// <param name="entity">文件路径</param>
        public static void Set(TEntity entity, string url)
        {
            if (entity == null)
                throw new Exception("Parameter humanResource is null!");

            FileStream fs = null;
            try
            {
                XmlSerializer xs = new XmlSerializer(typeof(TEntity));
                fs = new FileStream(url, FileMode.Create, FileAccess.Write);
                xs.Serialize(fs, entity);
                model = default(TEntity);
                fs.Close();
            }
            catch
            {
                if (fs != null)
                    fs.Close();
                throw new Exception("Xml serialization failed!");
            }
        }

        /// <summary>
        /// 序列化对象
        /// </summary>
        /// <typeparam name="T">对象类型</typeparam>
        /// <param name="t">对象</param>
        /// <returns></returns>
        public static string Serialize<T>(T t)
        {
            using (StringWriter sw = new StringWriter())
            {
                XmlSerializer xz = new XmlSerializer(t.GetType());
                xz.Serialize(sw, t);
                return sw.ToString();
            }
        }

        /// <summary>
        /// 反序列化为对象
        /// </summary>
        /// <param name="type">对象类型</param>
        /// <param name="s">对象序列化后的Xml字符串</param>
        /// <returns></returns>
        public static object Deserialize(Type type, string s)
        {
            using (StringReader sr = new StringReader(s))
            {
                XmlSerializer xz = new XmlSerializer(type);
                return xz.Deserialize(sr);
            }
        }

        /// <summary>
        /// 反序列化为对象
        /// </summary>
        /// <param name="xmlDoc">XmlDocument类型</param>
        /// <param name="t">对象</param>
        /// <returns></returns>
        public static T Deserialize<T>(XmlDocument xmlDoc)
        {
            StringReader reader = new StringReader(xmlDoc.OuterXml);
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            T t = (T)serializer.Deserialize(reader);
            return t;
        }

        /// <summary>
        /// 反序列化为对象
        /// </summary>
        /// <param name="xmlNode">XmlDocument类型</param>
        /// <param name="t">对象</param>
        /// <returns></returns>
        public static T Deserialize<T>(XmlNode xmlNode)
        {
            StringReader reader = new StringReader(xmlNode.OuterXml);
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            T t = (T)serializer.Deserialize(reader);
            return t;
        }
        /// <summary>
        /// 序列化为对象
        /// </summary>
        /// <param name="xmlDoc">XmlDocument类型</param>
        /// <param name="t">对象</param>
        /// <returns>XmlDocument对象</returns>
        public static XmlDocument GetXmlDocument<T>(T t)
        {
            XmlDocument xmlDoc = new XmlDocument();

            XmlSerializer serializer = new XmlSerializer(typeof(T));
            StringBuilder output = new StringBuilder();
            XmlWriterSettings ws = new XmlWriterSettings();
            ws.Indent = true;
            XmlWriter writer = XmlWriter.Create(output, ws);
            serializer.Serialize(writer, t);
            xmlDoc.LoadXml(output.ToString());

            return xmlDoc;
        }
    }
}