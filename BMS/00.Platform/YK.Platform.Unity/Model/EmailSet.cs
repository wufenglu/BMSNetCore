using System.Xml.Serialization;

namespace YK.Platform.Unity.Model
{
    /// <summary>
    /// 邮箱信息设置
    /// </summary>
    [XmlRoot("EmailSet")]
    public class EmailSet
    {
        /// <summary>
        /// 打开或者关闭邮件发送功能
        /// </summary>
        [XmlElement(ElementName = "openOrcloseWeb")]
        public int openOrcloseWeb { get; set; }

        /// <summary>
        /// 邮箱账号
        /// </summary>
        [XmlElement(ElementName = "EmailName")]
        public string EmailName { get; set; }

        /// <summary>
        /// 邮箱密码
        /// </summary>
        [XmlElement(ElementName = "EmailPwd")]
        public string EmailPwd { get; set; }

        /// <summary>
        /// SMTP地址
        /// </summary>
        [XmlElement(ElementName = "SMTP")]
        public string SMTP { get; set; }

        /// <summary>
        /// 端口号
        /// </summary>
        [XmlElement(ElementName = "Port")]
        public string Port { get; set; }       
    }
}
