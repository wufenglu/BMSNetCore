using YK.Platform.Entitys;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace YK.Config.Model
{
    /// <summary> 
    ///系统级用户
    /// </summary>
    [Table("Config_User")]
    [Description("系统级用户")]
    public class User : Entity
    {
        /// <summary> 
        ///编号
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public virtual int ID { get; set; }
        /// <summary> 
        ///用户名
        /// </summary>
        public virtual string UserName { get; set; }
        /// <summary> 
        ///用户名
        /// </summary>
        public virtual string UserCode { get; set; }
        /// <summary> 
        ///密码
        /// </summary>
        public virtual string Password { get; set; }
        /// <summary> 
        ///状态
        /// </summary>
        public virtual bool IsEnable { get; set; }
    }
}
