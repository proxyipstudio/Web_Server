using Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    class EFContextFactory
    {
        #region 从线程的数据槽中获得上下文对象

        private static object obLock = new object();
        /// <summary>
        /// 从线程的数据槽中获得上下文对象
        /// </summary>
        /// <returns></returns>
        public static DbContext GetDbContextFromWM()
        {
            DbContext context = CallContext.GetData("Dbcontext") as IPEntities;
            if(context == null)
            {
                lock(obLock)
                {
                    if (context == null)
                    {
                        context = new IPEntities();
                        CallContext.SetData("Dbcontext", context);
                    }
                }
            }
            return context;
        }
        #endregion
    }
}
