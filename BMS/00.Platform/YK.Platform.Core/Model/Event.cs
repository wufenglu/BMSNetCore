using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YK.Platform.Core.Model
{
    /// <summary>
    /// 事件
    /// </summary>
    public class Event
    {
        public Event() {
            Subscribers = new List<Subscriber>();
        }

        /// <summary>
        /// 事件键
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// 事件说明
        /// </summary>
        public string Remark { get; set; }

        /// <summary>
        /// 订阅集合
        /// </summary>
        public List<Subscriber> Subscribers { get; set; }
    }
}
