using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using YK.Platform.Core.Model;
using YK.Platform.Core.Pager;

namespace YK.Platform.Core.CoreFramework
{
    /// <summary>
    /// Find
    /// </summary>
    internal partial class CoreFramework<TEntity> 
    {
        /// <summary>
        /// 分页查询，基础方法，参数：页面大小，页码，主键，查询字段，表达式，排序，数据总条数
        /// </summary>
        /// <param name="pageSize">页面大小</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="selectFields">查询字段</param>
        /// <param name="express">表达式</param>
        /// <param name="orderBy">排序</param>
        /// <param name="recordCount">数据总条数</param>        
        /// <returns></returns>
        public List<TEntity> Find(int pageSize, int pageIndex, string selectFields, System.Linq.Expressions.Expression<Func<TEntity, bool>> express, string orderBy, ref int recordCount)
        {
            //获取参数和条件
            CoreFrameworkEntity lambdaEntity = GetLambdaEntity(express);
            //条件
            string where = lambdaEntity.Where;
            //参数列表
            List<SqlParameter> listPara = lambdaEntity.ParaList;

            selectFields = string.IsNullOrEmpty(selectFields) ? "*" : selectFields;//查询字段
            orderBy = string.IsNullOrEmpty(orderBy) ? PrimaryKey : orderBy;

            IPager page = Pager.Pager.getInstance();
            IDataReader sdr = page.GetPagerInfo(TableName, selectFields, pageSize, pageIndex, where, orderBy, ref recordCount, listPara);

            return DynamicBuilder<TEntity>.GetList(sdr, columnAttrList);

        }

        /// <summary>
        /// 不分页查询,基础方法
        /// </summary>
        /// <param name="express">表达式</param>
        /// <param name="count">显示总数，空则全部显示</param>
        /// <param name="selectFields">查询字段</param>        
        /// <param name="orderBy">排序,为空则不排序</param>
        /// <returns></returns>
        public List<TEntity> Find(System.Linq.Expressions.Expression<Func<TEntity, bool>> express, int? count = null, string selectFields = null, string orderBy = null)
        {
            //获取参数和条件
            CoreFrameworkEntity lambdaEntity = GetLambdaEntity(express);

            //调用通用查询
            return this.CommonSearch(lambdaEntity, count, selectFields, orderBy);
        }

        /// <summary>
        /// 不分页查询，通过表达式查询数据
        /// </summary>
        /// <param name="express">表达式</param>
        /// <returns></returns>
        public List<TEntity> Find(System.Linq.Expressions.Expression<Func<TEntity, bool>> express)
        {
            return Find(express);
        }

        /// <summary>
        /// 不分页查询，通过表达式和排序查询数据
        /// </summary>
        /// <param name="express">表达式</param>
        /// <param name="orderBy">排序,为空则不排序</param>
        /// <returns></returns>
        public List<TEntity> FindAll()
        {
            return Find(null);
        }
    }
}
