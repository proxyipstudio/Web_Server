using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class TaskRuleBLL:BaseBLL<TaskRule>
    {
        public static readonly TaskRuleBLL single = new TaskRuleBLL();

        protected override void SetDAL()
        {
            DAL = TaskRuleDAL.single;
        }

        /// <summary>
        /// 获得未被采集的第一个规则
        /// </summary>
        /// <returns></returns>
        public TaskRule GetModelFirst()
        {
            Expression<Func<TaskRule, bool>> expWhere = a => a.IsDelete != 1;
            return GetModel(expWhere);
        }

        public List<TaskRule> GetModelList(int pageIndex,int pageSize,out int count)
        {
            Expression<Func<TaskRule, bool>> expWhere = a => a.IsDelete != 1;
            Expression<Func<TaskRule, int>> expOrder = a => a.ID;
            count = GetModelCount(expWhere);
            return DAL.GetModelsByPage<int>(pageSize, pageIndex, false, expOrder, expWhere);
        }
    }
}
