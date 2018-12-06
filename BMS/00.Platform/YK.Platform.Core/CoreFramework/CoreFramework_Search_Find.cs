using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace YK.Platform.Core.CoreFramework
{
    /// <summary>
    /// Find
    /// </summary>
    internal partial class CoreFramework<TEntity> 
    {
        /// <summary>
        /// 不分页查询，通过表达式查询数据
        /// </summary>
        /// <param name="express">表达式</param>
        /// <returns></returns>
        public List<TEntity> Find(System.Linq.Expressions.Expression<Func<TEntity, bool>> express)
        {
            return Search(express);
        }

        /// <summary>
        /// 不分页查询，通过表达式和排序查询数据
        /// </summary>
        /// <param name="express">表达式</param>
        /// <param name="orderBy">排序,为空则不排序</param>
        /// <returns></returns>
        public List<TEntity> FindAll()
        {
            return Search(null);
        }
    }
}
