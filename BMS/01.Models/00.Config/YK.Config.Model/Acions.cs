using YK.Platform.Entitys;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace YK.Config.Model
{
    /// <summary> 
    ///动作
    /// </summary>
    [Table("Config_Acions")]
    [Description("动作")]
    public class Acions : Entity
    {
        /// <summary> 
        ///编号
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        /// <summary> 
        ///页面ID
        /// </summary>
        public int PageID { get; set; }
        /// <summary> 
        ///名称
        /// </summary>
        public string Name { get; set; }
        /// <summary> 
        ///编码
        /// </summary>
        public string Code { get; set; }
        /// <summary> 
        ///排序
        /// </summary>
        public int OrderBy { get; set; }
    }
}
