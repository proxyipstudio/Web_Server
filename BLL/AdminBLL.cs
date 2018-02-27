using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DAL;
using System.Linq.Expressions;

namespace BLL
{
    public class AdminBLL:BaseBLL<Admin>
    {
        public static readonly AdminBLL single = new AdminBLL();

        protected override void SetDAL()
        {
            DAL = AdminDAL.single;
        }

       #region 登陆
        /// <summary>
        /// 登陆
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="passWord"></param>
        /// <returns></returns>
       public Admin AdminLogin(string userName,string passWord)
        {
            Expression<Func<Admin, bool>> expWhere = a => a.userName == userName && a.PassWord == passWord && a.IsDelete != 1;
            return GetModel(expWhere); 
       }
       #endregion
    }
}
