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
    public class TaskRuleController : BaseController
    {
        public ContentResult GetTaskRuleList()
        {
            try
            {
                if (!MTool.IsNumber(Request["pageIndex"]) || !MTool.IsNumber(Request["pageNumber"]))
                {
                    msg = "页码和页面大小请输入数字";
                    status = 0;
                    return GetRst();
                }

                int pageIndex = Convert.ToInt32(Request["pageIndex"]);
                int pageNumber = Convert.ToInt32(Request["pageNumber"]);
                int count = 0;
                List<TaskRule> list = TaskRuleBLL.single.GetModelList(pageIndex, pageNumber, out count);
                data = JsonConvert.SerializeObject(list,timeConverter);
                data = "\"count\":" + count + ",\"TaskRule\":" + data;
            }
            catch
            {
                status = 0;
                msg = "发生异常";
            }
            return GetRst();
        }

        //删除校验规则
        public ContentResult DeleteTaskRule()
        {
            try
            {
                int id = Convert.ToInt32(Request["Id"]);
                TaskRule rule = TaskRuleBLL.single.GetModel(a => a.ID == id && a.IsDelete != 1);
                if(rule==null)
                {
                    status = 0;
                    msg = "该校验规则已被删除";
                    return GetRst();
                }
                rule.IsDelete = 1;
                rule.DeleteTime = DateTime.Now;
                rule.DeleteId = LoginAdmin.ID;
                if (!TaskRuleBLL.single.Update(rule))
                {
                    status = 0;
                    msg = "删除失败";
                }
            }
            catch
            {
                status = 0;
                msg = "发生异常";
            }
            return GetRst();
        }

        //更新/添加校验规则
        public ContentResult AUTaskRule()
        {
            try
            {
                TaskRule rule = new TaskRule();
                if (!string.IsNullOrWhiteSpace(Request["Id"]))
                {
                    int id = Convert.ToInt32(Request["Id"]);
                    rule = TaskRuleBLL.single.GetModel(a => a.ID == id);
                    if (rule == null)
                    {
                        status = 0;
                        msg = "该id不存在";
                        return GetRst();
                    }
                }
                rule.Url = Request["Urls"];
                if (!rule.Url.ToLower().Contains("http"))
                {
                    msg = "请输入正确的url";
                    status = 0;
                    return GetRst();
                }
                rule.RegIp = Request["RegIp"];
                rule.RegPort = Request["RegPort"];
                rule.RegAnonymous = Request["RegAnonymous"];
                rule.RegType = Request["RegType"];
                rule.RegCountry = Request["RegCountry"];
                rule.RegProvince = Request["RegProvince"];
                rule.RegCity = Request["RegCity"];
                rule.ReqHead = Request["ReqHead"];
                if (!TaskRuleBLL.single.Update(rule))
                {
                    status = 0;
                    msg = "插入或更新失败";
                }
            }
            catch
            {
                status = 0;
                msg = "发生异常";
            }
            return GetRst();
        }

        //根据id获得采集规则信息
        public ContentResult GetTaskRuleById()
        {
            try
            {
                int id = Convert.ToInt32(Request["Id"]);
                TaskRule taskRule = TaskRuleBLL.single.GetModel(a => a.ID == id && a.IsDelete != 1);
                if (taskRule == null)
                {
                    status = 0;
                    msg = "该采集规则已被删除";
                    return GetRst();
                }
                data = "\"info\":" + JsonConvert.SerializeObject(taskRule);
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