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


        public static string MD5(string str)
        {
            byte[] b = System.Text.Encoding.Default.GetBytes(str);

            b = new System.Security.Cryptography.MD5CryptoServiceProvider().ComputeHash(b);
            string ret = "";
            for (int i = 0; i < b.Length; i++)
            {
                ret += b[i].ToString("x").PadLeft(2, '0');
            }
            return ret;
        }
    }
}