using BLL;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class ClientController : Controller
    {
        #region 获得分配的任务
        public ContentResult GetTask()
        {
            int id = Convert.ToInt32(Request["Id"]);
            TaskRule rule = TaskRuleBLL.single.GetModel(a => a.ID == id);
            string rst = JsonConvert.SerializeObject(rule);
            return Content(rst);
        }
        #endregion

        #region 获得校验规则
        public ContentResult GetCheckRule()
        {
            string fdb = AppDomain.CurrentDomain.BaseDirectory;
            CheckRule check = CheckRuleBLL.single.GetModelFirst();
            string rst = JsonConvert.SerializeObject(check);
            return Content(rst);
        }
        #endregion
    }
}