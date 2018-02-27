using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class IpPriceDAL:BaseDAL<IpPrice>
    {
        public static readonly IpPriceDAL single = new IpPriceDAL();
    }
}
