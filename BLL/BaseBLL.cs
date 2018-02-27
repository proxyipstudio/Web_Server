using DAL;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public abstract class BaseBLL<T> where T : class,new()
    {
        public IsoDateTimeConverter timeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy'-'MM'-'dd' 'HH':'mm':'ss" };

        /// <summary>
        /// 构造方法中，创建仓储。
        /// </summary>
        public BaseBLL()
        {
            SetDAL();
        }

        /// <summary>
        /// DAL属性在BaseBLL的子类中用到
        /// </summary>
        protected BaseDAL<T> DAL { get; set; }
       
        
        /// <summary>
        /// 实现什么DAL
        /// </summary>
        protected abstract void SetDAL();


        /// <summary>
        /// 根据查询条件获取单个实体
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public T GetModel(Expression<Func<T, bool>> condition)
        {
            return DAL.GetModelSingle(condition);
        }

        /// <summary>
        /// 根据查询条件获取实体列表
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public List<T> GetModelList(Expression<Func<T,bool>> condition)
        {
            return DAL.GetModelList(condition);
        }

        /// <summary>
        /// 根据查询条件获取实体数量
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public int GetModelCount(Expression<Func<T,bool>> condition)
        {
            return DAL.GetModelCount(condition);
        }

        /// <summary>
        /// 更新实体集，如不存在则添加
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool Update(T t)
        {
           return   DAL.Update(t);
           
        }

        /// <summary>
        /// 增加
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool AddModel(T t)
        {
            return  DAL.Add(t);
        }

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
            return DAL.GetModelsByPage(pageSize, pageIndex, isAsc, OrderByLambda, WhereLambda);
        }

        /// <summary>
        /// 删除模型,真正删除
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public bool RemoveModel(T t)
        {
            if(t == null)
            {
                return false;
            }
            return DAL.Delete(t);
        }


        /// <summary>
        /// 保存
        /// </summary>
        /// <returns></returns>
        public int SaveChanges()
        {
            return DAL.SaveChanges();
        }

        /// <summary>
        /// 快速插入
        /// </summary>
        /// <param name="list"></param>
        /// <returns></returns>
        public bool BulkInsert(List<T> list)
        {
            return DAL.BulkInsert(list);
        }
    }
}
