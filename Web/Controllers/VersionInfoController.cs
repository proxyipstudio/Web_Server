using BLL;
using Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class VersionInfoController : BaseController
    {
        //新增/更新组件版本
        public ContentResult AUVersion()
        {
            try
            {
                VersionInfo version = new VersionInfo();
                if (!string.IsNullOrWhiteSpace(Request["Id"]))
                {
                    int id = Convert.ToInt32(Request["Id"]);
                    version = VersionInfoBLL.single.GetModel(a => a.ID == id);
                    if (version == null)
                    {
                        status = 0;
                        msg = "该id不存在";
                        return GetRst();
                    }
                }
                HttpPostedFileBase file = Request.Files["VersionFile"];
                //判断是否上传了文件
                if (file.ContentLength != 0)
                {
                    string path = string.Empty;
                    string fileName = Path.GetFileName(file.FileName);
                    string fileEx = System.IO.Path.GetExtension(fileName);//获取上传文件的扩展名
                    string FileType = ".Exe,.py,.dll";//定义上传文件的类型字符串
                    if (!FileType.Contains(fileEx))
                    {
                        status = 0;
                        msg = "只能导入指定类型的文件";
                        return GetRst();
                    }
                    MTool.SaveFile(file, "版本文件", out path);
                    version.VersionAddress = path;
                }
                version.VersionNumber = Request["VersionNumber"];
                version.PartName = Request["PartName"];
                version.PartType = Request["PartType"];
                if (!MTool.IsNumber(Request["PartOwner"]))
                {
                    status = 0;
                    msg = "组件所有者需为数字";
                    return GetRst();
                }
                if (string.IsNullOrWhiteSpace(version.VersionNumber))
                {
                    status = 0;
                    msg = "请输入版本号";
                    return GetRst();
                }
                if (string.IsNullOrWhiteSpace(version.PartName))
                {
                    status = 0;
                    msg = "请输入组件名称";
                    return GetRst();
                }
                if (string.IsNullOrWhiteSpace(version.VersionAddress))
                {
                    status = 0;
                    msg = "请输入组件地址";
                    return GetRst();
                }
                if (string.IsNullOrWhiteSpace(version.PartType))
                {
                    status = 0;
                    msg = "请输入组件类型";
                    return GetRst();
                }
                if (!VersionInfoBLL.single.Update(version))
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

        //删除组件信息
        public ContentResult DeleteVersion()
        {
            try
            {
                int id = Convert.ToInt32(Request["Id"]);
                VersionInfo version = VersionInfoBLL.single.GetModel(a => a.ID == id && a.IsDelete != 1);
                if (version == null)
                {
                    status = 0;
                    msg = "该组件版本已被删除";
                    return GetRst();
                }
                version.IsDelete = 1;
                version.DeleteTime = DateTime.Now;
                version.DeleteID = LoginAdmin.ID;
                if (!VersionInfoBLL.single.Update(version))
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

        //获得版本列表
        public ContentResult GetVersionList()
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
                List<VersionInfo> list = VersionInfoBLL.single.GetModelList(pageIndex, pageNumber, out count);
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

        //根据Id获得版本信息
        public ContentResult GetVersionInfoById()
        {
            try
            {
                int id = Convert.ToInt32(Request["Id"]);
                VersionInfo versioninfo = VersionInfoBLL.single.GetModel(a => a.ID == id && a.IsDelete != 1);
                if (versioninfo == null)
                {
                    status = 0;
                    msg = "该版本信息已被删除";
                    return GetRst();
                }
                data = "\"info\":" + JsonConvert.SerializeObject(versioninfo);
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