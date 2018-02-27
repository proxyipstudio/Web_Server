using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace Web
{
    public static  class MTool
    {
        /// <summary>
        /// 验证是否为纯数字
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNumber(string value)
        {
            if(value == null)
            {
                return false;
            }
            return Regex.IsMatch(value, @"^[+-]?\d*$");
        }


       /// <summary>
       /// 保存文件
       /// </summary>
       /// <param name="file">文件</param>
       /// <param name="saveDirecPath">文件保存的文件夹</param>
       /// <param name="filePath">返回的文件路径</param>
        public static void SaveFile(HttpPostedFileBase file, string saveDirecPath, out string filePath)
        {
            string fdb = AppDomain.CurrentDomain.BaseDirectory+"文件\\";
            if (!Directory.Exists(fdb))
            {
                Directory.CreateDirectory(fdb);
            }
            string fileName = saveDirecPath+"\\" + file.FileName;
            string path = fdb + fileName;
            file.SaveAs(path);
            filePath = fileName;
        }
    }
}