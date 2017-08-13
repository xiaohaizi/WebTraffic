using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Net;
using System.Text.RegularExpressions;
namespace WebTraffic.WebCollect
{
    public class Web_Spider
    {
        private string url;
        public string Url
        {
            get { return url; }
            set { url = value; }
        }


        //默认根据utf-8编码
        /// <summary>
        /// 通过网址获取页面内容
        /// </summary>
        /// <returns></returns>
        public string getHtmlContentByUrl(bool _shieldIP = false)
        {
            string content = "";
            string s = "utf-8";
            string st = "";
            WebHeaderCollection HeadersStr;
            WebClient webclient = new WebClient();
            webclient.Headers["User-Agent"] = "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.1 (KHTML, like Gecko) Chrome/21.0.1180.89 Safari/537.1";
            webclient.Headers["Accept-Charset"] = "GBK,utf-8;q=0.7,*;q=0.3";
            try
            {
             
                Stream stream = webclient.OpenRead(this.Url);
                HeadersStr = webclient.ResponseHeaders;
                if (HeadersStr.HasKeys())
                {
                    string enstr = HeadersStr["Content-Type"];
                    if (enstr.IndexOf("charset") > 0) { st = s = enstr.Split('=')[1]; }

                }
                using (StreamReader rdsr = new StreamReader(stream, Encoding.GetEncoding(s)))
                {
                    content = rdsr.ReadToEnd();
                    rdsr.Close();
                }
                if (st.Length <= 0)
                {
                    s = Html_Regex.RegexByStr(Html_Regex.Encodstr, content);
                    if (s != "Error")
                    {
                        s = s.Replace("charset=", "").Trim('"').ToLower();
                        if (s != "utf-8" && !string.IsNullOrWhiteSpace(s))
                        {
                            content = getHtmlContentByUrl(s);
                        }
                    }
                    else
                    {
                        content = getHtmlContentByUrl("utf-8");
                    }
                }
            }
            catch (Exception e)
            {

            }


            return content;
        }

        /// <summary>
        /// 根据用户输入的编码获取
        /// </summary>
        /// <param name="EncodText">编码(utf-8/gb2312)</param>
        /// <returns>返回值</returns>
        public string getHtmlContentByUrl(string EncodText)
        {
            string content = "";
            WebClient webclient = new WebClient();
            webclient.Headers["User-Agent"] = "Mozilla/5.0 (Windows NT 6.2; WOW64) AppleWebKit/537.1 (KHTML, like Gecko) Chrome/21.0.1180.89 Safari/537.1";
            webclient.Headers["Accept-Charset"] = "GBK,utf-8;q=0.7,*;q=0.3";

           

            Stream stream = webclient.OpenRead(this.Url);

            using (StreamReader rdsr = new StreamReader(stream, Encoding.GetEncoding(EncodText)))
            {
                content = rdsr.ReadToEnd();
                rdsr.Close();
            }


            return content;
        }




        /// <summary>
        /// utf-8转gb2312
        /// </summary>
        /// <param name="str">要转换的字符</param>
        /// <returns></returns>
        public string Utf8ChangeGb2312(string str)
        {
            string newStr = "";
            Encoding utf8 = Encoding.UTF8;
            Encoding gb2312 = Encoding.GetEncoding("gb2312");
            byte[] unicodeBytes = gb2312.GetBytes(str);

            byte[] asciiBytes = Encoding.Convert(gb2312, utf8, unicodeBytes);

            char[] asciiChars = new char[utf8.GetCharCount(asciiBytes, 0, asciiBytes.Length)];
            utf8.GetChars(asciiBytes, 0, asciiBytes.Length, asciiChars, 0);
            newStr = new string(asciiChars);
            return newStr;

        }



        public void Test(string _url)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_url);
            //request.Method = "GET";
            request.BeginGetResponse(new AsyncCallback(ReadCallback), request);
        }
        public void ReadCallback(IAsyncResult asyc)
        {
            string content = "";
            string s = "utf-8";
            string st = "";
            HttpWebRequest request = (HttpWebRequest)asyc.AsyncState;
            HttpWebResponse response = (HttpWebResponse)request.EndGetResponse(asyc);
            WebHeaderCollection HeadersStr;
            HeadersStr = request.Headers;
            Stream stream = response.GetResponseStream();
            if (HeadersStr.HasKeys())
            {
                string enstr = HeadersStr["Content-Type"];
                if (enstr.IndexOf("charset") > 0) { st = s = enstr.Split('=')[1]; }

            }
            using (StreamReader rdsr = new StreamReader(stream, Encoding.GetEncoding(s)))
            {
                content = rdsr.ReadToEnd();
                rdsr.Close();
            }
        }



        public void DownLoadFile(string _fileUrl, string _filePath)
        {

            WebClient mywebclient = new WebClient();
            mywebclient.DownloadFile(new Uri(_fileUrl), _filePath);
        }

    }
}
