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
    public   class OrderBLL:BaseBLL<Order>
    {
        public static readonly OrderBLL single = new OrderBLL();

        protected override void SetDAL()
        {
            DAL = OrderDAL.single;
        }

        public List<Order> GetModelList(int pageIndex, int pageSize, out int count)
        {
            Expression<Func<Order, bool>> expWhere = a => a.IsDelete != 1;
            Expression<Func<Order, int>> expOrder = a => a.ID;
            count = GetModelCount(expWhere);
            return DAL.GetModelsByPage<int>(pageSize, pageIndex, false, expOrder, expWhere);
        }
    }
}
