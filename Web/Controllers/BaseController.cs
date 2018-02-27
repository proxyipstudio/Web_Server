using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json.Converters;

namespace Web.Controllers
{
    public class BaseController : Controller
    {
       public IsoDateTimeConverter timeConverter = new IsoDateTimeConverter { DateTimeFormat = "yyyy'-'MM'-'dd' 'HH':'mm':'ss" };
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
        /// 登陆的用户
        /// </summary>
        public Admin LoginAdmin = new Admin();

        /// <summary>
        /// 返回结果
        /// </summary>
        /// <returns></returns>
        public ContentResult GetRst()
        {
            data = string.Format("{{\"status\":{0},\"msg\":\"{1}\",\"data\":{{{2}}}}}", status, msg, data);
            return Content(data);
        }

        ////在控制器方法之前执行
        //protected override void OnActionExecuting(ActionExecutingContext filterContext)
        //{
        //    base.OnActionExecuting(filterContext);
        //    if (filterContext.HttpContext.Session["Admin"] == null)
        //    {
        //        filterContext.HttpContext.Response.Redirect("/Login/NoLogin");
        //    }
        //    else
        //    {
        //        LoginAdmin = (Admin)Session["Admin"];
        //    }
        //}

    }
}
