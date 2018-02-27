using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CommonHelper;

namespace Web.Controllers
{
    public class APIController : Controller
    {
        #region 与baseControl相同的信息
        /// <summary>
        /// 状态：1为返回预期结果，0为未返回预期结果
        /// </summary>
        public int status = 1;
        /// <summary
        /// 信息
        /// </summary>
        public string msg = "success";
        /// <summary>
        /// 数据
        /// </summary>
        public string data = string.Empty;
        /// <summary>
        /// 返回结果
        /// </summary>
        /// <returns></returns>
        public ContentResult GetRst()
        {
            data = string.Format("{{\"status\":{0},\"msg\":\"{1}\",\"data\":{{{2}}}}}", status, msg, data);
            return Content(data);
        }
        #endregion

        //提取ip
        public ContentResult GetIp()
        {
            try
            {
                if(!MTool.IsNumber(Request["Count"]))
                {
                    msg = "数量需为整数";
                    status = 0;
                    return GetRst();
                }
                int count = Convert.ToInt32(Request["Count"]);
                if(count>3000)
                {
                    count = 3000;
                }
                string code = Request["Code"];
                
                if (string.IsNullOrWhiteSpace(code))
                {
                    msg = "请输入提取码";
                    status = 0;
                    return GetRst();
                }
                Order order = OrderBLL.single.GetModel(a => a.Code == code && a.IsDelete != 1);
                if (order == null)
                {
                    msg = "请输入正确的提取码";
                    status = 0;
                    return GetRst();
                }
                if (order.ExpireDate < DateTime.Now)
                {
                    msg = "订单已过期，请续费";
                    status = 0;
                    return GetRst();
                }
                order.SumTakeCount += 1;
                order.SumTakeNumber += count;
                OrderBLL.single.Update(order);
                string url = Pub.IpRedisAddress + "/ProxyIp/GetProxyIp_User?count=" + count + "&user=" + order.ID;
                data = "\"info\":" + HttpAdd.OnlyGetHtml(url);
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