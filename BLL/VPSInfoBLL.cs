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
    public  class VPSInfoBLL:BaseBLL<VPSInfo>
    {
        public static readonly VPSInfoBLL single = new VPSInfoBLL();

        protected override void SetDAL()
        {
            DAL = VPSInfoDAL.single;
        }

        public List<VPSInfo> GetModelList(int pageIndex, int pageSize, out int count)
        {
            Expression<Func<VPSInfo, bool>> expWhere = a => a.IsDelete != 1;
            Expression<Func<VPSInfo, int>> expOrder = a => a.ID;
            count = GetModelCount(expWhere);
            return DAL.GetModelsByPage<int>(pageSize, pageIndex, false, expOrder, expWhere);
        }
    }
}
