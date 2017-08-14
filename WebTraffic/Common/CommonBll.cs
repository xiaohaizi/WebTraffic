using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;

namespace WebTraffic.Common
{
    public class CommonBll
    {
        /// <summary>
        /// 写入文件
        /// </summary>
        /// <param name="_content"></param>
        /// <param name="_fileName"></param>
        public static void WriteTextFile(string _content, string _fileName)
        {
            StringBuilder sb = new StringBuilder();
            if (!File.Exists(HttpRuntime.AppDomainAppPath.ToString() + _fileName))
            {
                using (File.Create(HttpRuntime.AppDomainAppPath.ToString() + _fileName))
                { }
            }
           
            StreamWriter sw = new StreamWriter(HttpRuntime.AppDomainAppPath.ToString() + _fileName, true);
            sw.WriteLine(_content);
            sw.Close();

        }
    }
}