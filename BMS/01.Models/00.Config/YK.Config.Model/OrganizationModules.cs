using YK.Platform.Entitys;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace YK.Config.Model
{
    /// <summary> 
    ///组织模块
    /// </summary>
    [Table("Config_OrganizationModules")]
    [Description("组织")]
    public class OrganizationModules : Entity
    {
        /// <summary> 
        ///编号
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        /// <summary> 
        ///父级
        /// </summary>
        public int ModuleID { get; set; }
        /// <summary> 
        ///租户ID
        /// </summary>
        public int OrganizationID { get; set; }
    }
}
