using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity.Migrations;
using LinqKit;

namespace DAL
{
    public class BaseDAL<T> where T : class,new()
    {
        protected DbContext db = EFContextFactory.GetDbContextFromWM();

        #region 单个查询
        /// <summary>
        /// 根据查询条件获取单个实体
        /// </summary>
        /// <param name="condition">查询条件</param>
        /// <returns></returns>
        public T GetModelSingle(Expression<Func<T, bool>> condition)
        {
            try
            {
                return db.Set<T>().AsExpandable().AsNoTracking().Where(condition).FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }
        #endregion

        #region 多个查询
        /// <summary>
        /// 根据查询条件获取实体数据集
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public List<T> GetModelList(Expression<Func<T, bool>> condition)
        {
            return db.Set<T>().AsExpandable().AsNoTracking().Where(condition).ToList();
        }
        #endregion

        #region
        /// <summary>
        /// 获得模型数量
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public int GetModelCount(Expression<Func<T, bool>> condition)
        {
            return db.Set<T>().AsExpandable().AsNoTracking().Where(condition).Count<T>();
        }
        #endregion


        #region 增加
        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="t"></param>
        public bool Add(T t)
        {
            db.Set<T>().Add(t);
            return db.SaveChanges() > 0;
        }
        #endregion

        #region 删除
        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="t"></param>
        public bool Delete(T t)
        {
            try
            {
                db.Set<T>().Remove(t);
                return db.SaveChanges() > 0;
            }
            catch
            {
                return false;
            }
        }
        #endregion

        #region 更新
        /// <summary>
        /// 更新数据，如不存在则添加
        /// </summary>
        /// <param name="t"></param>
        public bool Update(T t)
        {
            db.Set<T>().AddOrUpdate(t);
            return db.SaveChanges()>0;
        }
        #endregion

        #region 分页查询
        /// <summary>
        /// 分页查询
        /// </summary>
        /// <typeparam name="type"></typeparam>
        /// <param name="pageSize">页数</param>
        /// <param name="pageIndex">每页数量</param>
        /// <param name="isAsc">是否升序</param>
        /// <param name="OrderByLambda">排序</param>
        /// <param name="WhereLambda">条件</param>
        /// <returns></returns>
        public List<T> GetModelsByPage<type>(int pageSize, int pageIndex, bool isAsc,
            Expression<Func<T, type>> OrderByLambda, Expression<Func<T, bool>> WhereLambda)
        {
            //是否升序
            if (isAsc)
            {
                return db.Set<T>().AsExpandable().AsNoTracking().Where(WhereLambda).OrderBy(OrderByLambda).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            }
            else
            {
                return db.Set<T>().AsExpandable().AsNoTracking().Where(WhereLambda).OrderByDescending(OrderByLambda).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            }
        }
        #endregion

        #region 快速插入
        /// <summary>
        /// 快速插入大量数据
        /// </summary>
        /// <param name="list"></param>
        public bool BulkInsert(List<T> list)
        {
            try
            {
                db.BulkInsert(list);
                db.BulkSaveChanges();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
        #endregion

        #region 保存修改，返回修改数量
        /// <summary>
        /// 保存，返回修改行数
        /// </summary>
        /// <returns></returns>
        public int SaveChanges()
        {
            return db.SaveChanges();
        }
        #endregion
    }
}
