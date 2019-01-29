using System.Xml.Serialization;

namespace YK.Platform.Utility.Model
{
    /// <summary>
    /// 站点信息设置
    /// </summary>
    [XmlRoot("WebSet")]
    public class WebSet
    {
        /// <summary>
        /// 打开或者关闭网站
        /// </summary>
        [XmlElement(ElementName = "openOrcloseWeb")]
        public int openOrcloseWeb { get; set; }

        /// <summary>
        /// 站点
        /// </summary>
        [XmlElement(ElementName = "Website")]
        public string Website { get; set; }

        /// <summary>
        /// 站点名称
        /// </summary>
        [XmlElement(ElementName = "WebName")]
        public string WebName { get; set; }

        /// <summary>
        /// 邮箱
        /// </summary>
        [XmlElement(ElementName = "Email")]
        public string Email { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        [XmlElement(ElementName = "Address")]
        public string Address { get; set; }

        /// <summary>
        /// 邮编
        /// </summary>
        [XmlElement(ElementName = "ZipCode")]
        public string ZipCode { get; set; }

        /// <summary>
        /// 电话
        /// </summary>
        [XmlElement(ElementName = "Phone")]
        public string Phone { get; set; }

        /// <summary>
        /// 备案号
        /// </summary>
        [XmlElement(ElementName = "Copyright")]
        public string Copyright { get; set; }

        /// <summary>
        /// 是否允许复制网页
        /// </summary>
        [XmlElement(ElementName = "DuplicateWebpage")]
        public string DuplicateWebpage { get; set; }

        /// <summary>
        /// ICP
        /// </summary>
        [XmlElement(ElementName = "ICP")]
        public string ICP { get; set; }

        /// <summary>
        /// 关键字
        /// </summary>
        [XmlElement(ElementName = "KeyWord")]
        public string KeyWord { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        [XmlElement(ElementName = "Description")]
        public string Description { get; set; }

        /// <summary>
        /// 传真
        /// </summary>
        [XmlElement(ElementName = "Fax")]
        public string Fax { get; set; }

        /// <summary>
        /// TQ
        /// </summary>
        [XmlElement(ElementName = "TQ")]
        public string TQ { get; set; }

        /// <summary>
        /// 水印类型
        /// </summary>
        [XmlElement(ElementName = "WaterMarkType")]
        public int WaterMarkType { get; set; }

        /// <summary>
        /// 水印文字
        /// </summary>
        [XmlElement(ElementName = "WaterMarkTxt")]
        public string WaterMarkTxt { get; set; }

        /// <summary>
        /// 水印图片路径
        /// </summary>
        [XmlElement(ElementName = "WaterMarkPicUrl")]
        public string WaterMarkPicUrl { get; set; }

        /// <summary>
        /// 水平对齐
        /// </summary>
        [XmlElement(ElementName = "WaterMarkHorizontal")]
        public string WaterMarkHorizontal { get; set; }

        /// <summary>
        /// 垂直对齐
        /// </summary>
        [XmlElement(ElementName = "WaterMarkVertical")]
        public string WaterMarkVertical { get; set; }
    }
}
