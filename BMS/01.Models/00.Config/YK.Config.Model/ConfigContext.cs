using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.IO;

namespace YK.Config.Model
{
    /// <summary>
    /// 配置上下文
    /// </summary>
    public class ConfigContext : DbContext
    {
        /// <summary>
        /// 配置对象
        /// </summary>
        public IConfiguration Configuration = null;

        public ConfigContext() { }

        public ConfigContext(IConfiguration configuration,DbContextOptions<ConfigContext> options)
            : base(options)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// 设置链接
        /// </summary>
        /// <param name="optionsBuilder"></param>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            IConfiguration configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .Add(new JsonConfigurationSource { Path = "appsettings.json", Optional = false, ReloadOnChange = true })
                .Build();
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        }

        /// <summary>
        /// 用户
        /// </summary>
        public DbSet<User> User { get; set; }

        /// <summary>
        /// 模块
        /// </summary>
        public DbSet<Modules> Modules { get; set; }

        /// <summary>
        /// 页面
        /// </summary>
        public DbSet<Pages> Pages { get; set; }

        /// <summary>
        /// 权限点
        /// </summary>
        public DbSet<Acions> Acions { get; set; }

        /// <summary>
        /// 组织
        /// </summary>
        public DbSet<Organizations> Organizations { get; set; }

        /// <summary>
        /// 组织数据库
        /// </summary>
        public DbSet<OrganizationDataBase> OrganizationDataBase { get; set; }

        /// <summary>
        /// 组织模块
        /// </summary>
        public DbSet<OrganizationModules> OrganizationModules { get; set; }
    }
}
