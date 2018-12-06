using YK.Platform.Core.CoreFramework;
using YK.Platform.Entitys;

namespace YK.Platform.Core
{
    /// <summary>
    /// 核心
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class Framework<TEntity> where TEntity : Entity, new()
    {
        /// <summary>
        /// 核心
        /// </summary>
        public static ICoreFramework<TEntity> Instance(string orgCode=null)
        {
             return new CoreFramework<TEntity>(orgCode); 
        }
    }
}
