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
    public class VersionInfoBLL:BaseBLL<VersionInfo>
    {
        public static readonly VersionInfoBLL single = new VersionInfoBLL();
        protected override void SetDAL()
        {
            DAL = VersionInfoDAL.single;
        }

        public List<VersionInfo> GetModelList(int pageIndex, int pageSize, out int count)
        {
            Expression<Func<VersionInfo, bool>> expWhere = a => a.IsDelete != 1;
            Expression<Func<VersionInfo, int>> expOrder = a => a.ID;
            count = GetModelCount(expWhere);
            return DAL.GetModelsByPage<int>(pageSize, pageIndex, false, expOrder, expWhere);
        }
    }
}
