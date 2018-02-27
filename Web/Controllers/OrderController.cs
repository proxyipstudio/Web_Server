using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;

namespace Web.Controllers
{
    public class OrderController : BaseController
    {
        //获得订单列表
        public ContentResult GetOrderList()
        {
            try
            {
                if(!MTool.IsNumber(Request["pageIndex"])||!MTool.IsNumber(Request["pageNumber"]))
                {
                    msg = "页码和页面大小请输入数字";
                    status = 0;
                    return GetRst();
                }

                int pageIndex = Convert.ToInt32(Request["pageIndex"]);
                int pageNumber = Convert.ToInt32(Request["pageNumber"]);
                int count = 0;
                List<Order> list = OrderBLL.single.GetModelList(pageIndex, pageNumber, out count);
                data = JsonConvert.SerializeObject(list,timeConverter);
                data = "\"count\":" + count + ",\"order\":" + data + "";
            }
            catch
            {
                status = 0;
                msg = "发生异常";
            }
            return GetRst();
          
        }

        //删除订单
        public ContentResult DeleteOrder()
        {
            try
            {
                int orderId = Convert.ToInt32(Request["Id"]);
                Order order = OrderBLL.single.GetModel(a => a.ID == orderId && a.IsDelete != 1);
                if(order==null)
                {
                    status = 0;
                    msg = "该订单已被删除";
                    return GetRst();
                }
                order.IsDelete = 1;
                order.DeleteId = LoginAdmin.ID;
                order.DeleteTime = DateTime.Now;
                if (!OrderBLL.single.Update(order))
                {
                    status = 0;
                    msg = "删除失败";
                }
            }
            catch
            {
                msg = "发生异常";
                status = 0;
            }
            return GetRst();
        }

        //添加订单
        public ContentResult AUOrder()
        {
            try
            {
                Order order = new Order();
                if(!string.IsNullOrWhiteSpace(Request["Id"]))
                {
                    int id =Convert.ToInt32( Request["Id"]);
                    order = OrderBLL.single.GetModel(a => a.ID == id);
                    if (order == null)
                    {
                        status = 0;
                        msg = "该id不存在";
                        return GetRst();
                    }
                }
                int ipPriceID = Convert.ToInt32(Request["ipPriceID"]);
                IpPrice ipPrice = IpPriceBLL.single.GetModel(a => a.ID == ipPriceID);
                if(ipPrice==null)
                {
                    status = 0;
                    msg = "该套餐已失效";
                    return GetRst();
                }
                order.BuyTime = DateTime.Now;
                order.TelNum = Request["TelNum"];
                order.Code = Guid.NewGuid().ToString();
                order.ExpireDate = DateTime.Now.AddDays(ipPrice.UseTime);
                if (!OrderBLL.single.AddModel(order))
                {
                    status = 0;
                    msg = "订单添加失败";
                }
                data = "\"code:\":\"" + order.Code+"\"";
            }
            catch(Exception ex)
            {
                msg = "发生异常";
                status = 0;
            }
            return GetRst();
        }

        //根据id获得订单信息
        public ContentResult GetOrderById()
        {
            try
            {
                int id = Convert.ToInt32(Request["Id"]);
                Order order = OrderBLL.single.GetModel(a => a.ID == id && a.IsDelete != 1);
                if (order == null)
                {
                    status = 0;
                    msg = "该订单已被删除";
                    return GetRst();
                }
                data = "\"info\":" + JsonConvert.SerializeObject(order);
            }
            catch
            {
                status = 0;
                msg = "发生异常";
            }
            return GetRst();
        }
	}
}