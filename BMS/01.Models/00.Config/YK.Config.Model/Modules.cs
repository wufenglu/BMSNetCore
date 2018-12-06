using YK.Platform.Entitys;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace YK.Config.Model
{
    /// <summary> 
    ///模块
    /// </summary>
    [Table("Config_Modules")]
    [Description("模块")]
    public class Modules : Entity
    {
        /// <summary> 
        ///编号
        /// </summary>
        [Key]
        public int ID { get; set; }
        /// <summary> 
        ///父级
        /// </summary>
        public int ParentID { get; set; }
        /// <summary> 
        ///名称
        /// </summary>
        public string Name { get; set; }
        /// <summary> 
        ///编码
        /// </summary>
        public string Code { get; set; }
        /// <summary> 
        ///层级
        /// </summary>
        public int Level { get; set; }
        /// <summary> 
        ///是否启用
        /// </summary>
        public bool IsEnable { get; set; }
        /// <summary> 
        ///排序
        /// </summary>
        public int OrderBy { get; set; }
    }
}
