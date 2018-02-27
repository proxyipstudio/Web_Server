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
    public class CheckRuleController : BaseController
    {
        //获得校验规则列表
        public ContentResult GetCheckRuleList()
        {
            try
            {
                List<CheckRule> list = CheckRuleBLL.single.GetModelList(a => a.IsDelete != 1);
                data=JsonConvert.SerializeObject(list,timeConverter);
                data = "\"list\":" + data;
            }
            catch
            {
                status = 0;
                msg = "发生异常";
            }
            return GetRst();
            
        }

        //删除校验规则
        public ContentResult DeleteCheckRule()
        {
            try
            {
                int id = Convert.ToInt32(Request["Id"]);
                CheckRule rule = CheckRuleBLL.single.GetModel(a => a.ID == id && a.IsDelete!=1);
                if(rule==null)
                {
                    status = 0;
                    msg = "该校验规则已被删除";
                    return GetRst();
                }
                rule.IsDelete = 1;
                rule.DeleteTime = DateTime.Now;
                rule.DeleteId = LoginAdmin.ID;
                if (!CheckRuleBLL.single.Update(rule))
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
        public ContentResult AUCheckRule()
        {
            try
            {
                CheckRule rule = new CheckRule();
                if (!string.IsNullOrWhiteSpace(Request["Id"]))
                {
                    int id = Convert.ToInt32(Request["Id"]);
                    rule = CheckRuleBLL.single.GetModel(a => a.ID == id);
                    if (rule == null)
                    {
                        status = 0;
                        msg = "该id不存在";
                        return GetRst();
                    }
                }
                rule.Url = Request["Urls"];
                if (rule.Url == null)
                {
                    msg = "请输入url";
                    return GetRst();
                }
                rule.RegIp = Request["RegIp"];
                rule.RegPort = Request["RegPort"];
                rule.RegAnonymous = Request["RegAnonymous"];
                rule.RegType = Request["RegType"];
                rule.RegCountry = Request["RegCountry"];
                rule.RegProvince = Request["RegProvince"];
                rule.RegCity = Request["RegCity"];
                if (!CheckRuleBLL.single.Update(rule))
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

        //根据id获得校验规则信息
        public ContentResult GetCheckRuleById()
        {
            try
            {
                int id = Convert.ToInt32(Request["Id"]);
                CheckRule checkRule = CheckRuleBLL.single.GetModel(a => a.ID == id && a.IsDelete != 1);
                if (checkRule == null)
                {
                    status = 0;
                    msg = "该校验规则已被删除";
                    return GetRst();
                }
                data = "\"info\":" + JsonConvert.SerializeObject(checkRule);
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