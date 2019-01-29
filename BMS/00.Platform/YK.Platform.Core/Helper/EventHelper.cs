using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using YK.Platform.Core.Model;
using YK.Platform.Utility;

namespace YK.Platform.Core
{
    /// <summary>
    /// 事件
    /// </summary>
    public class EventHelper
    {
        public List<Event> eventEntitys;

        /// <summary>
        ///  定义一个静态变量来保存类的实例
        /// </summary>
        private static EventHelper eventHelper { get; set; }

        /// <summary>
        /// 定义一个标识确保线程同步
        /// </summary>
        private static readonly object locker = new object();

        /// <summary>
        /// 实例化
        /// </summary>
        public static EventHelper Instance {
            get {
                lock (locker)
                {
                    if (eventHelper == null)
                    {
                        eventHelper = new EventHelper();
                        eventHelper.eventEntitys = eventHelper.GetConfig();
                    }
                }
                return eventHelper;
            }
        }

        /// <summary>
        /// 执行
        /// </summary>
        /// <param name="key"></param>
        /// <param name="data"></param>
        public void Execute(string key, object data)
        {
            Event eventEntity = eventEntitys.Where(w => w.Key == key).FirstOrDefault();
            if (eventEntity != null)
            {
                foreach (var item in eventEntity.Subscribers)
                {
                    HttpWebRequestHelper request = new HttpWebRequestHelper();
                    request.Post(item.Url, Newtonsoft.Json.JsonConvert.SerializeObject(data));
                }
            }
        }

        /// <summary>
        /// 获取配置
        /// </summary>
        /// <returns></returns>
        public List<Event> GetConfig() {
            List<Event> result = new List<Event>();

            string fileUrl = Directory.GetCurrentDirectory() + @"\App_Data\Event.Config";
            XmlDocument xd = new XmlDocument();
            xd.Load(fileUrl);

            XmlNodeList xmlNodeList = xd.SelectSingleNode("EventConfig").SelectNodes("Events/Event");
            //循环遍历
            foreach (XmlNode item in xmlNodeList)
            {
                Event eventEntity = new Event();
                eventEntity.Key = item.Attributes["Name"].Value;
                eventEntity.Remark = item.Attributes["Remark"].Value;

                XmlNodeList eventNodes =  item.SelectNodes("Event");
                foreach (XmlNode eventNode in eventNodes) {
                    //循环遍历
                    foreach (XmlNode subscriberNode in eventNode.SelectSingleNode("Subscribers").ChildNodes)
                    {
                        Subscriber subscriber = new Subscriber();
                        subscriber.Url = subscriberNode.Attributes["Url"].Value;
                        eventEntity.Subscribers.Add(subscriber);
                    }
                    result.Add(eventEntity);
                }                
            }
            return result;
        }
    }
}
