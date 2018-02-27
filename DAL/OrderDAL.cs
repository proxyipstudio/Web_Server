using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class OrderDAL:BaseDAL<Order>
    {
        public static readonly OrderDAL single = new OrderDAL();
    }
}
