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
    public class IpPriceController : BaseController
    {
       
        public ContentResult AUIpPrice()
        {
            try
            {
                IpPrice ipPrice = new IpPrice();
                if (!string.IsNullOrWhiteSpace(Request["Id"]))
                {
                    int id = Convert.ToInt32(Request["Id"]);
                    ipPrice = IpPriceBLL.single.GetModel(a => a.ID == id);
                    if (ipPrice == null)
                    {
                        status = 0;
                        msg = "该id不存在";
                        return GetRst();
                    }
                }
                if (!MTool.IsNumber(Request["UseTime"]))
                {
                    status = 0;
                    msg = "可用天数需为整数";
                    return GetRst();
                }
                string price = Request["Price"];
                ipPrice.Price = Convert.ToDouble(price);
                ipPrice.Discount = Convert.ToDouble(Request["Discount"]);
                ipPrice.UseTime = Convert.ToInt32(Request["UseTime"]);
                ipPrice.Description = Request["Description"];
                HttpPostedFileBase file = Request.Files["Pic"];
                //判断是否上传了文件
                if (file.ContentLength != 0)
                {
                    string path = string.Empty;
                    string fileName = Path.GetFileName(file.FileName);
                    string fileEx = System.IO.Path.GetExtension(fileName);//获取上传文件的扩展名
                    string FileType = ".bmp,.jpg,.png,.gif";//定义上传文件的类型字符串
                    if (!FileType.Contains(fileEx))
                    {
                        status = 0;
                        msg = "只能导入指定类型的文件";
                        return GetRst();
                    }
                    MTool.SaveFile(file, "价格图片", out path);
                    ipPrice.PicPath = path;
                }
                ipPrice.IsDelete = 0;
                if (!IpPriceBLL.single.Update(ipPrice))
                {
                    status = 0;
                    msg = "新增/修改失败";
                }
            }
            catch(Exception ex)
            {
                status = 0;
                msg = "发生异常";
            }
            return GetRst();
        }
       
        public ContentResult DeleteIpPrice()
        {
            try
            {
                int id = Convert.ToInt32(Request["Id"]);
                IpPrice rule = IpPriceBLL.single.GetModel(a => a.ID == id && a.IsDelete != 1);
                if (rule == null)
                {
                    status = 0;
                    msg = "该套餐已被删除";
                    return GetRst();
                }
                rule.IsDelete = 1;
                rule.DeleteTime = DateTime.Now;
                rule.DeleteID = LoginAdmin.ID;
                if (!IpPriceBLL.single.Update(rule))
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

        public ContentResult GetIpPriceList()
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
                List<IpPrice> list = IpPriceBLL.single.GetModelList(pageIndex, pageNumber, out count);
                data = JsonConvert.SerializeObject(list, timeConverter);
                data = "\"count\":" + count + ",\"list\":" + data;
            }
            catch
            {
                status = 0;
                msg = "发生异常";
            }
            return GetRst();
        }


        //根据Id获得价格信息
        public ContentResult GetIpPriceById()
        {
            try
            {
                int id = Convert.ToInt32(Request["Id"]);
                IpPrice ipprice = IpPriceBLL.single.GetModel(a => a.ID == id && a.IsDelete != 1);
                if (ipprice == null)
                {
                    status = 0;
                    msg = "该套餐已被删除";
                    return GetRst();
                }
                data = "\"info\":" + JsonConvert.SerializeObject(ipprice);
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