using BLL;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class NodeInfoController : BaseController
    {
       //获取节点列表
        public ContentResult GetNodeInfoList()
        {
            try
            {
                if (!MTool.IsNumber(Request["pageIndex"]) || !MTool.IsNumber(Request["pageNumber"]))
                {
                    msg = "页码和页面大小请输入数字";
                    status = 0;
                    return GetRst();
                }
                if(!MTool.IsNumber(Request["NodeType"]))
                {
                    msg = "节点类型需为数字";
                    status = 0;
                    return GetRst();
                }
                int nodeType = Convert.ToInt32(Request["NodeType"]);
                int pageIndex = Convert.ToInt32(Request["pageIndex"]);
                int pageNumber = Convert.ToInt32(Request["pageNumber"]);
                int count = 0;
                data = NodeInfoBLL.single.GetModelsByPage(pageIndex, pageNumber,nodeType, out count);
                data = "\"count\":" + count + ",\"list\":" + data;
            }
            catch
            {
                status = 0;
                msg = "发生异常";
            }
            return GetRst();
        }

        //获取节点详细信息
        public ContentResult GetNodeInfoDetail()
        {
            try
            {
                if (!MTool.IsNumber(Request["Id"]))
                {
                    msg = "请输入正确的id";
                    status = 0;
                    return GetRst();
                }
                int id = Convert.ToInt32(Request["Id"]);
                data = NodeInfoBLL.single.GetNodeInfoDetails(id);
                data = "\"info\":" + data;
            }
            catch
            {
                status = 0;
                msg = "发生异常";
            }
            return GetRst();
        }

        //删除节点
        public ContentResult DeleteNode()
        {
            try
            {
                if (!MTool.IsNumber(Request["Id"]))
                {
                    msg = "请输入正确的id";
                    status = 0;
                    return GetRst();
                }
                int id = Convert.ToInt32(Request["Id"]);
                NodeInfo nodeInfo = NodeInfoBLL.single.GetModel(a => a.Id == id);
                if (nodeInfo == null)
                {
                    msg = "该节点不存在";
                    status = 0;
                    return GetRst();
                }
                if (nodeInfo.IsDelete==1)
                {
                    msg = "该节点已被删除";
                    status = 0;
                    return GetRst();
                }
                nodeInfo.IsDelete = 1;
                nodeInfo.DeleteTime = DateTime.Now;
                nodeInfo.DeleteID = LoginAdmin.ID;
                if (!NodeInfoBLL.single.Update(nodeInfo))
                {
                    msg = "节点删除失败";
                    status = 0;
                }
            }
            catch
            {
                status = 0;
                msg = "发生异常";
            }
            return GetRst();
        }

        //添加/更新节点
        public ContentResult AUNode()
        {
            try
            {
                NodeInfo nodeInfo = new NodeInfo();
                if (!string.IsNullOrWhiteSpace(Request["Id"]))
                {
                    int id = Convert.ToInt32(Request["Id"]);
                    nodeInfo = NodeInfoBLL.single.GetModel(a => a.Id == id);
                    if (nodeInfo == null)
                    {
                        status = 0;
                        msg = "该id不存在";
                        return GetRst();
                    }
                }
                if (!MTool.IsNumber(Request["HostId"]))
                {
                    status = 0;
                    msg = "hostid需为数字";
                    return GetRst();
                }
                if (!MTool.IsNumber(Request["NodeType"]))
                {
                    msg = "节点类型需为数字";
                    status = 0;
                    return GetRst();
                }
                if (!string.IsNullOrWhiteSpace(Request["Id"]))
                {
                    int id =  Convert.ToInt32(Request["Id"]);
                    nodeInfo = NodeInfoBLL.single.GetModel(a => a.Id ==id);
                    if (nodeInfo == null)
                    {
                        status = 0;
                        msg = "该id不存在";
                        return GetRst();
                    }
                }
                nodeInfo.HostId = Convert.ToInt32(Request["HostId"]);
                nodeInfo.Name = Request["Name"];
                nodeInfo.Version = Request["Version"];
                nodeInfo.NodeType = Convert.ToInt32(Request["NodeType"]);
                if (string.IsNullOrWhiteSpace(nodeInfo.Name))
                {
                    status = 0;
                    msg = "请输入名称";
                    return GetRst();
                }
                if (string.IsNullOrWhiteSpace(nodeInfo.Version))
                {
                    status = 0;
                    msg = "版本号";
                    return GetRst();
                }
                VPSInfo vpsInfo = VPSInfoBLL.single.GetModel(a => a.ID == nodeInfo.HostId && a.IsDelete != 1);
                if (vpsInfo == null)
                {
                    status = 0;
                    msg = "该主机不存在";
                    return GetRst();
                }
                if (!NodeInfoBLL.single.Update(nodeInfo))
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
	}
}