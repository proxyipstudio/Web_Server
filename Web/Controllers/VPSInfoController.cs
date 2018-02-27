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
    public class VPSInfoController : BaseController
    {
        //新增、修改Vps信息
        public ContentResult AUVps()
        {
            try
            {
                VPSInfo vpsinfo = new VPSInfo();
              
                if (!string.IsNullOrWhiteSpace(Request["Id"]))
                {
                    int id = Convert.ToInt32(Request["Id"]);
                    vpsinfo = VPSInfoBLL.single.GetModel(a => a.ID ==id );
                    if (vpsinfo == null)
                    {
                        status = 0;
                        msg = "该id不存在";
                        return GetRst();
                    }
                }
                vpsinfo.RAM = Request["RAM"];
                vpsinfo.Cpu = Request["Cpu"];
                vpsinfo.IsOpen = Convert.ToInt32(Request["IsOpen"]);
                vpsinfo.HostName = Request["HostName"];
                vpsinfo.SerialNumber = Request["SerialNumber"];
                vpsinfo.IPIn = Request["IPIn"];
                vpsinfo.IPOut = Request["IPOut"];
                string SerialNumber = Request["SerialNumber"];
                VPSInfo vpsInfo2 = VPSInfoBLL.single.GetModel(a => a.SerialNumber == SerialNumber);
                if (vpsInfo2 != null)
                {
                    status = 0;
                    msg = "序列号已存在";
                    return GetRst();
                }
                if (string.IsNullOrWhiteSpace(vpsinfo.HostName))
                {
                    status = 0;
                    msg = "请输入主机名";
                    return GetRst();
                }
                vpsinfo.adminstrator = Request["adminstrator"];
                vpsinfo.password = Request["password"];
                vpsinfo.KDUser = Request["KDUser"];
                vpsinfo.KDPassword = Request["KDPassword"];
                vpsinfo.IsDelete = 0;
                if (!VPSInfoBLL.single.Update(vpsinfo))
                {
                    status = 0;
                    msg = "新增/更新失败";
                }
            }
            catch
            {
                status = 0;
                msg = "发生异常";
            }
            return GetRst();
        }

        //删除主机信息
        public ContentResult DeleteVps()
        {
            try
            {
                int id = Convert.ToInt32(Request["Id"]);
                VPSInfo rule = VPSInfoBLL.single.GetModel(a => a.ID == id && a.IsDelete != 1);
                if (rule == null)
                {
                    status = 0;
                    msg = "该校验规则已被删除";
                    return GetRst();
                }
                rule.IsDelete = 1;
                rule.DeleteTime = DateTime.Now;
                rule.DeleteID = LoginAdmin.ID;
                if (!VPSInfoBLL.single.Update(rule))
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

        //获得Vps列表
        public ContentResult GetVpsList()
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
                List<VPSInfo> list = VPSInfoBLL.single.GetModelList(pageIndex, pageNumber, out count);
                data = JsonConvert.SerializeObject(list, timeConverter);
                data = "\"count\":" + count + ",\"List\":" + data;
            }
            catch
            {
                status = 0;
                msg = "发生异常";
            }
            return GetRst();
        }

        //获取所有vps信息
        public ContentResult GetAllVps()
        {
            try
            {
                List<VPSInfo> list = VPSInfoBLL.single.GetModelList(a => a.IsDelete != 1);
                data = JsonConvert.SerializeObject(list, timeConverter);
                data = "\"list\":" + data;
            }
            catch
            {
                status = 0;
                msg = "发生异常";
            }
            return GetRst();
        }

        //根据Id获得vps信息
        public ContentResult GetVpsById()
        {
            try
            {
                int id = Convert.ToInt32(Request["Id"]);
                VPSInfo vps = VPSInfoBLL.single.GetModel(a => a.ID == id && a.IsDelete != 1);
                if (vps == null)
                {
                    status = 0;
                    msg = "该主机已被删除";
                    return GetRst();
                }
                data = "\"info\":" + JsonConvert.SerializeObject(vps);
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