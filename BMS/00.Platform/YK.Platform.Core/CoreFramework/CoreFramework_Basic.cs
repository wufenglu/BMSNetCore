using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

using YK.Platform.Core.Model;
using YK.Platform.Core.SqlHelper;
using YK.Platform.Core.Enums;
using YK.Platform.Entitys;
using YK.Platform.Utility.Extensions;
using YK.Platform.Core.Event;

namespace YK.Platform.Core.CoreFramework
{
    /// <summary>
    /// 公共操作类，增（Insert）、删（Delete）、改（Update）、查（Get）功能
    /// </summary>
    /// <typeparam name="TEntity">实体</typeparam>
    internal partial class CoreFramework<TEntity> where TEntity : Entity, new()
    {
        //获取实体列的特性
        List<EntityPropColumnAttributes> columnAttrList = new List<EntityPropColumnAttributes>();
        /// <summary>
        /// 租户编码
        /// </summary>
        public string OrgCode { get; set; }

        public CoreFramework(string orgCode)
        {
            OrgCode = orgCode;
            columnAttrList = AttributeHelper.GetEntityColumnAtrributes<TEntity>();
        }

        #region Insert

        /// <summary>
        /// 插入（添加）
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="isBackId">是否返回主键</param>
        /// <returns></returns>
        public int Insert(TEntity entity)
        {
            return Insert(entity, false).ToStr().ToInt();
        }
        #region 备份
        ///// <summary>
        ///// 插入（添加）
        ///// </summary>
        ///// <param name="entity">实体</param>
        ///// <param name="isBackId">是否返回主键</param>
        ///// <returns></returns>
        //public object Insert(TEntity entity, bool isBackId)
        //{
        //    //参数列表
        //    List<SqlParameter> listPara = new List<SqlParameter>();
        //    string insertStr = "";//插入字段
        //    string paraStr = "";//参数
        //    //获取实体列的特性
        //    List<EntityPropColumnAttributes> columnAttrList = AttributeHelper.GetEntityColumnAtrributes<TEntity>();
        //    foreach (PropertyInfo prop in entity.GetType().GetProperties())
        //    {
        //        EntityPropColumnAttributes columnAttribute = columnAttrList.Where(w => w.propName.ToLower() == prop.Name.ToLower()).First();
        //        //当不为自动增长时
        //        if (columnAttribute.isDbGenerated == false)
        //        {
        //            object val = prop.GetValue(entity, null);
        //            if (val != null)
        //            {
        //                insertStr += columnAttribute.fieldName + ",";//字符拼接
        //                paraStr += "@" + columnAttribute.fieldName + ",";//字符拼接                    
        //                listPara.Add(new SqlParameter("@" + columnAttribute.fieldName, val));//参数添加
        //            }
        //        }
        //    }
        //    //去掉最后的逗号
        //    insertStr = insertStr.TrimEnd(',');
        //    paraStr = paraStr.TrimEnd(',');
        //    //拼接SQL语句
        //    string cmdText = "insert into " + tableName + "(" + insertStr + ") values(" + paraStr + ");";
        //    if (isBackId)
        //    {
        //        cmdText += "SELECT @@IDENTITY AS ID;";
        //        return SqlConvertHelper.ExecuteScalar(cmdText, listPara);
        //    }
        //    else
        //    {
        //        return SqlConvertHelper.ExecuteNonQuery(cmdText, listPara);
        //    }
        //}
        #endregion

        /// <summary>
        /// 插入（添加）
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="isBackId">是否返回主键</param>
        /// <returns></returns>
        public object Insert(TEntity entity, bool isBackId)
        {
            var entityList = new List<TEntity>() { entity };
            DBModel dBModel = GetInsertInfo(entityList);

            //实体事件--变更前
            EventHelper.Instance.Execute(ChangingEventKey, entity);
            object result = null;

            if (isBackId)
            {
                dBModel.SQL += "SELECT @@IDENTITY AS ID;";
                result = SqlConvertHelper.GetInstallSqlHelper(OrgCode).ExecuteScalar(dBModel.SQL, dBModel.Params);
            }
            else
            {
                result = SqlConvertHelper.GetInstallSqlHelper(OrgCode).ExecuteNonQuery(dBModel.SQL, dBModel.Params);
            }

            //设置缓存
            object id = GetPrimaryKeyValue(entity);
            Cache.BusinessCachesHelper<TEntity>.AddEntityCache(id, entity);

            //实体事件--变更后
            EventHelper.Instance.Execute(ChangedEventKey, entity);
            return result;
        }

        /// <summary>
        /// 批量插入（添加）
        /// </summary>
        /// <param name="entityList">实体列表</param>
        /// <returns></returns>
        public object BatchInsert(List<TEntity> entityList)
        {
            int pageSize = 100;
            int pageCount = entityList.Count / pageSize;
            for (int i = 0; i < pageCount; i++)
            {
                List<TEntity> list = entityList.Skip(i * pageSize).Take(pageSize).ToList();
                DBModel dBModel = GetInsertInfo(list);

                //实体事件--变更前               
                EventHelper.Instance.Execute(ChangingEventKey, list);

                //执行
                SqlConvertHelper.GetInstallSqlHelper(OrgCode).ExecuteNonQuery(dBModel.SQL, dBModel.Params);

                //实体事件--变更后
                EventHelper.Instance.Execute(ChangedEventKey, list);
            }
            return true;
        }

        /// <summary>
        /// 大数据量插入
        /// </summary>
        /// <param name="entityList">实体列表</param>
        /// <param name="isPkIdentity">是否保留标志源</param>
        /// <returns></returns>
        public bool Insert(List<TEntity> entityList, bool isPkIdentity)
        {
            //转换为DataTable
            DataTable dt = EntityListToDataTable(entityList);
            return SqlConvertHelper.GetInstallSqlHelper(OrgCode).BatchCopyInsert(dt.TableName, dt, isPkIdentity);
        }

        /// <summary>
        /// 获取插入数据的相关信息
        /// </summary>
        /// <param name="entityList">实体列表</param>
        /// <returns></returns>
        private DBModel GetInsertInfo(List<TEntity> entityList)
        {
            DBModel dBModel = new DBModel();
            dBModel.Params = new List<SqlParameter>();

            StringBuilder cmdStringBuilder = new StringBuilder();

            int num = 0;
            foreach (var entity in entityList)
            {
                num++;

                List<ParamSqlModel> paramSqlModels = GetParamSQLByEntityProp(entity);
                //没有变更则跳出
                if (paramSqlModels == null) {
                    continue;
                }

                //查找非动生成的字段
                List<ParamSqlModel> changeParamSqlModels = paramSqlModels.Where(w => w.IsDbGenerated == false).ToList();
                //没有需要插入的则跳出
                if (changeParamSqlModels == null || changeParamSqlModels.Count == 0)
                {
                    continue;
                }

                //参数
                List<string> paramList = new List<string>();
                //插入字段
                List<string> filedList = new List<string>();

                //遍历赋值
                foreach (ParamSqlModel paramSqlModel in changeParamSqlModels)
                {
                    paramSqlModel.Param.ParameterName = paramSqlModel.Param.ParameterName + num;
                    paramList.Add(paramSqlModel.Param.ParameterName);
                    filedList.Add(paramSqlModel.Field);
                    dBModel.Params.Add(paramSqlModel.Param);
                }

                string fieldStr = string.Join(",", filedList.ToArray());
                string paramStr = string.Join(",", paramList.ToArray());

                //拼接SQL语句
                string cmdText = "insert into " + TableName + "(" + fieldStr + ") values(" + paramStr + ");";
                cmdStringBuilder.Append(cmdText);
            }

            dBModel.SQL = cmdStringBuilder.ToString();

            return dBModel;
        }       

        #endregion

        #region Update共有方法
        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="id">主键值</param>
        /// <returns></returns>
        public int Update(TEntity entity)
        {
            EntityPropColumnAttributes columnAttribute = columnAttrList.Where(w => w.fieldName.ToLower() == PrimaryKey.ToLower()).First();

            //获取属性及属性值
            PropertyInfo prop = typeof(TEntity).GetProperty(columnAttribute.propName);
            object value = prop.GetValue(entity, null);

            //拼接条件
            List<Expression> express = new List<Expression>();
            express.Add(new Expression(columnAttribute.propName, ConditionEnum.Eq, value));

            return Update(entity, express);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="express">表达式</param>
        /// <returns></returns>
        public int Update(TEntity entity, List<Expression> express)
        {
            CoreFrameworkEntity careEntity = GetParaListAndWhere(express);
            return UpdateExecte(entity, careEntity);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="express">表达式</param>
        /// <returns></returns>
        public int Update(TEntity entity, System.Linq.Expressions.Expression<Func<TEntity, bool>> express)
        {
            CoreFrameworkEntity lamdaEntity = GetLambdaEntity(express);
            return UpdateExecte(entity, lamdaEntity);
        }

        #endregion

        #region Update私有方法

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="entity">实体</param>
        /// <param name="express">表达式</param>
        /// <returns></returns>
        private int UpdateExecte(TEntity entity, CoreFrameworkEntity coreFrameworkEntity)
        {
            DBModel dbModel = GetUpdateInfo(entity);
            if (dbModel == null) {
                return 0;
            }
                        
            dbModel.Params.AddRange(coreFrameworkEntity.ParaList);
            dbModel.SQL += " where " + coreFrameworkEntity.Where;

            //实体事件--变更前
            EventHelper.Instance.Execute(ChangingEventKey, entity);

            //执行
            int result = SqlConvertHelper.GetInstallSqlHelper(OrgCode).ExecuteNonQuery(dbModel.SQL, dbModel.Params);

            //设置缓存
            object id = GetPrimaryKeyValue(entity);
            Cache.BusinessCachesHelper<TEntity>.AddEntityCache(id, entity);

            //实体事件--变更后
            EventHelper.Instance.Execute(ChangedEventKey, entity);

            return result;
        }

        /// <summary>
        /// 获取修改信息
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        private DBModel GetUpdateInfo(TEntity entity)
        {
            List<ParamSqlModel> paramSqlModels = GetParamSQLByEntityProp(entity);
            //没有变更则跳出
            if (paramSqlModels == null)
            {
                return null;
            }

            List<ParamSqlModel> changeParamSqlModels = paramSqlModels.Where(w => w.IsDbGenerated == false && w.IsPrimaryKey == false).ToList();
            if (changeParamSqlModels == null || changeParamSqlModels.Count == 0)
            {
                return null;
            }

            string setSql = string.Join(",", changeParamSqlModels.Select(s => s.SQL).ToArray());
            string cmdText = "update " + TableName + " set " + setSql;

            return new DBModel()
            {
                SQL = cmdText,
                Params = changeParamSqlModels.Select(s => s.Param).ToList()
            };
        }

        #endregion

        #region Delete

        /// <summary>
        /// 批量删除
        /// </summary>
        /// <param name="ids">主键集合</param>
        /// <returns></returns>
        public int Delete(List<object> ids)
        {
            List<Expression> express = new List<Expression>();
            foreach (object id in ids) {
                express.Add(new Expression(PrimaryKey, ConditionEnum.Eq, id, JoinEnum.Or));
            }
            return Delete(express);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="id">主键值</param>
        /// <returns></returns>
        public int Delete(object id)
        {
            List<Expression> express = new List<Expression>();
            express.Add(new Expression(PrimaryKey, ConditionEnum.Eq, id));
            return Delete(express);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="express">表达式</param>
        /// <returns></returns>
        public int Delete(List<Expression> express)
        {
            //获取参数和条件
            CoreFrameworkEntity coreFrameworkEntity = GetParaListAndWhere(express);
            return DeleteExecute(coreFrameworkEntity);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="express">表达式</param>
        /// <returns></returns>
        public int Delete(System.Linq.Expressions.Expression<Func<TEntity, bool>> express)
        {
            CoreFrameworkEntity lamdaEntity = GetLambdaEntity<TEntity>(express);
            return DeleteExecute(lamdaEntity);
        }

        /// <summary>
        /// 删除执行
        /// </summary>
        /// <param name="coreFrameworkEntity"></param>
        /// <returns></returns>
        private int DeleteExecute(CoreFrameworkEntity coreFrameworkEntity) {
            //参数列表
            List<SqlParameter> listPara = new List<SqlParameter>();
            listPara.AddRange(coreFrameworkEntity.ParaList);

            //实体事件--变更前
            TEntity entity = Get(PrimaryKey);
            EventHelper.Instance.Execute(ChangingEventKey, entity);

            //拼接SQL语句
            string cmdText = "delete from " + TableName + " where " + coreFrameworkEntity.Where;
            int result = SqlConvertHelper.GetInstallSqlHelper(OrgCode).ExecuteNonQuery(cmdText, listPara);

            //设置缓存
            object id = GetPrimaryKeyValue(entity);
            Cache.BusinessCachesHelper<TEntity>.RemoveEntityCache(id);

            //实体事件--变更后
            EventHelper.Instance.Execute(ChangedEventKey, entity);

            return result;
        }
        #endregion

        #region Get
        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="express">表达式</param>
        /// <returns></returns>
        public TEntity Get(List<Expression> express)
        {
            //获取参数和条件
            CoreFrameworkEntity coreFrameworkEntity = GetParaListAndWhere(express);
            return GetExecute(coreFrameworkEntity);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="express">表达式</param>
        /// <returns></returns>
        public TEntity Get(System.Linq.Expressions.Expression<Func<TEntity, bool>> express)
        {
            CoreFrameworkEntity lamdaEntity = GetLambdaEntity<TEntity>(express);
            return GetExecute(lamdaEntity);
        }

        /// <summary>
        /// 获取实体
        /// </summary>
        /// <param name="id">主键值</param>
        /// <returns></returns>
        public TEntity Get(object id)
        {
            //设置缓存
            TEntity entity = Cache.BusinessCachesHelper<TEntity>.GetEntityCache(id);
            if (entity != null) {
                return entity;
            }

            //主键是否存在
            if (!string.IsNullOrEmpty(PrimaryKey))
            {
                //获取实体
                List<Expression> express = new List<Expression>();
                express.Add(new Expression(PrimaryKey, ConditionEnum.Eq, id));
                return Get(express);
            }
            return default(TEntity); ;
        }

        /// <summary>
        /// Get执行
        /// </summary>
        /// <param name="coreFrameworkEntity"></param>
        /// <returns></returns>
        private TEntity GetExecute(CoreFrameworkEntity coreFrameworkEntity) {
            //拼接SQL语句
            string cmdText = "select * from " + TableName + " where " + coreFrameworkEntity.Where;
            IDataReader sdr = SqlConvertHelper.GetInstallSqlHelper(OrgCode).ExecuteReader(cmdText, coreFrameworkEntity.ParaList);
            List<TEntity> list = DynamicBuilder<TEntity>.GetList(sdr, columnAttrList);
            return list.Count > 0 ? list.First() : default(TEntity);
        }
        #endregion
    }

}
