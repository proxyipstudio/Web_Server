using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BLL
{
    public class NodeInfoBLL:BaseBLL<NodeInfo>
    {
        public static readonly NodeInfoBLL single = new NodeInfoBLL();

        protected override void SetDAL()
        {
            DAL = NodeInfoDAL.single;
        }

        /// <summary>
        /// 分页查询
        /// </summary>
        /// <param name="pageIndex"></param>
        /// <param name="pageNumber"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public string GetModelsByPage(int pageIndex,int pageNumber,int nodeType,out int count)
        {
            IPEntities db = new IPEntities();
            var query = from a in db.NodeInfo
                      join b in db.VPSInfo
                      on a.HostId equals b.ID
                      where a.IsDelete != 1 && a.NodeType==nodeType
                      orderby a.Id
                      select new
                      {
                          a,
                          b.HostName,
                          b.SerialNumber,
                      };
            count = query.Count();

            var rst = query.Skip((pageIndex - 1) * pageNumber).Take(pageNumber).ToList();
            db.Dispose();
            return JsonConvert.SerializeObject(rst,timeConverter);
        }

        /// <summary>
        /// 获得节点以及主机的详细信息
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public string GetNodeInfoDetails(int id)
        {
            string rst = string.Empty;
            using(IPEntities db = new IPEntities())
            {
                var query = from a in db.NodeInfo
                            join b in db.VPSInfo
                            on a.HostId equals b.ID
                            where a.Id == id
                            select new
                            {
                                a,
                                b
                            };
                rst = JsonConvert.SerializeObject(query.ToList(),timeConverter);
            }
            return rst;
        }
    }
}
