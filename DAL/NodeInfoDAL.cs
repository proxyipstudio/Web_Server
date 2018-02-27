using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class NodeInfoDAL:BaseDAL<NodeInfo>
    {
        public static readonly NodeInfoDAL single = new NodeInfoDAL();
    }
}
