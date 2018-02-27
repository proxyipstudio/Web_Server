using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    /// <summary>
    /// 直接执行sql
    /// </summary>
    public class SqlDAL
    {
        /// <summary>
        /// 实例
        /// </summary>
        public static readonly SqlDAL single = new SqlDAL();

        private DbContext db = EFContextFactory.GetDbContextFromWM();

        #region -----执行简单sql-----
        /// <summary>
        /// 执行sql  返回影响行数
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public int ExecuteSql(string sql)
        {
            return db.Database.ExecuteSqlCommand(sql);
        }

        /// <summary>
        /// 执行sql 返回datatable
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public DataTable Query(string sql)
        {
            return db.Database.ExecuteDataTable(sql);
        }
        #endregion

        #region 执行存储过程
        /// <summary>
        /// 执行无参数的存储过程
        /// </summary>
        /// <param name="name">存储过程名</param>
        public int ExecTran(string name)
        {
            return  db.Database.ExecuteSqlCommand(name);
        }
        #endregion


    }
}
