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
    public class LoginController : Controller
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

        //登陆
        public ContentResult LoginIn()
        {
            try
            {
                string userName = Request["UserName"];
                string passWord = Request["PassWord"];
                string rst = string.Empty;
                Admin admin = AdminBLL.single.AdminLogin(userName.Trim(), passWord.Trim());
                if (admin == null)
                {
                    status = 0;
                    msg = "用户名或密码错误";
                    return GetRst();
                }
                else
                {
                    rst = JsonConvert.SerializeObject(admin);
                }
                Session["Admin"] = admin; 
            }
            catch
            {
                status = 0;
                msg = "发生异常";
            }
            return GetRst();
        }

        //退出登陆
        public ContentResult LoginOut()
        {
            Session["Admin"] = null;
            return GetRst();
        }

        //未登陆
        public ContentResult NoLogin()
        {
            msg = "请先登陆";
            status = 0;
            return GetRst();
        }

        /// <summary>
        /// 改变个人信息
        /// </summary>
        /// <returns></returns>
        public ContentResult ChangeMyInfo()
        {
            try
            {

                if (Session["Admin"] == null)
                {
                    status = 0;
                    msg = "请登陆";
                    return GetRst();
                }
                Admin admin = (Admin)Session["Admin"];
                string passWord = Request["passWord"];

                if (passWord != admin.PassWord)
                {
                    status = 0;
                    msg = "密码错误";
                    return GetRst();
                }
                if (!string.IsNullOrWhiteSpace(Request["NewPwd"]))
                {
                    admin.PassWord = Request["NewPwd"];
                }
                if (!string.IsNullOrWhiteSpace(Request["Phone"]))
                {
                    admin.Phone = Request["Phone"];
                }
                if (!string.IsNullOrWhiteSpace(Request["UserName"]))
                {
                    admin.userName = Request["userName"];
                }
                if (!string.IsNullOrWhiteSpace(Request["Email"]))
                {
                    admin.Email = Request["Email"];
                }
                AdminBLL.single.Update(admin);

                Session["Admin"] = admin;
            }
            catch
            {
                status = 0;
                msg = "发生异常";
            }
            return GetRst();
        }

        /// <summary>
        /// 获得个人信息
        /// </summary>
        /// <returns></returns>
        public ContentResult GetMyInfo()
        {
            try
            {
                if (Session["Admin"] == null)
                {
                    status = 0;
                    msg = "请登陆";
                    return GetRst();
                }
                Admin admin = (Admin)Session["Admin"];
                admin.PassWord = null;
                data = "\"info\":" + JsonConvert.SerializeObject(admin);
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