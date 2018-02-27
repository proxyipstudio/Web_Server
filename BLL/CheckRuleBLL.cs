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
    public class CheckRuleBLL : BaseBLL<CheckRule>
    {
        public static readonly CheckRuleBLL single = new CheckRuleBLL();

        protected override void SetDAL()
        {
            DAL = CheckRuleDAL.single;
        }

        #region 获得校验规则第一个
        /// <summary>
        /// 获得校验规则第一个
        /// </summary>
        /// <returns></returns>
        public CheckRule GetModelFirst()
        {
            Expression<Func<CheckRule, bool>> expWhere = a => a.IsDelete != 1;
            return GetModel(expWhere);
        }
        #endregion
    }
}
