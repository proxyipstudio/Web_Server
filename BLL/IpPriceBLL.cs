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
   public class IpPriceBLL:BaseBLL<IpPrice>
    {
       public static readonly IpPriceBLL single = new IpPriceBLL();
       
        protected override void SetDAL()
        {
            DAL = IpPriceDAL.single;
        }

        public List<IpPrice> GetModelList(int pageIndex, int pageSize, out int count)
        {
            Expression<Func<IpPrice, bool>> expWhere = a => a.IsDelete != 1;
            Expression<Func<IpPrice, int>> expOrder = a => a.ID;
            count = GetModelCount(expWhere);
            return DAL.GetModelsByPage<int>(pageSize, pageIndex, false, expOrder, expWhere);
        }
    }
}
