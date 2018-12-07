using YK.Platform.Entitys;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace YK.Config.Model
{
    /// <summary> 
    ///页面
    /// </summary>
    [Table("Config_Pages")]
    [Description("页面")]
    public class Pages : Entity
    {
        /// <summary> 
        ///模块ID
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        /// <summary> 
        ///模块ID
        /// </summary>
        public int ModuleID { get; set; }
        /// <summary> 
        ///名称
        /// </summary>
        public string Name { get; set; }
        /// <summary> 
        ///编码
        /// </summary>
        public string Code { get; set; }
        /// <summary> 
        ///路径
        /// </summary>
        public string Url { get; set; }
        /// <summary> 
        ///排序
        /// </summary>
        public int OrderBy { get; set; }
    }
}
