using YK.Platform.Entitys;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace YK.Config.Model
{
    /// <summary> 
    ///组织数据库
    /// </summary>
    [Description("组织数据库")]
    [Table("Config_OrganizationDataBase")]
    public class OrganizationDataBase : Entity
    {
        /// <summary> 
        ///编号
        /// </summary>
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }
        /// <summary> 
        ///租户ID
        /// </summary>
        public int OrganizationID { get; set; }
        /// <summary> 
        ///数据库类型
        /// </summary>
        public string DbType { get; set; }
        /// <summary> 
        ///数据库地址
        /// </summary>
        public string Server { get; set; }
        /// <summary> 
        ///数据库名称
        /// </summary>
        public string DatabaseName { get; set; }
        /// <summary> 
        ///用户名
        /// </summary>
        public string UserName { get; set; }
        /// <summary> 
        ///密码
        /// </summary>
        public string Password { get; set; }
        /// <summary> 
        ///端口
        /// </summary>
        public string Port { get; set; }
        /// <summary> 
        ///是否启用
        /// </summary>
        public bool IsEnable { get; set; }
        /// <summary> 
        ///是否主库
        /// </summary>
        public bool IsMaster { get; set; }
        /// <summary> 
        ///权重
        /// </summary>
        public bool Weight { get; set; }
    }
}
