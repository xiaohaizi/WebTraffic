using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
namespace WebTraffic.WebCollect
{
    public class Html_Regex
    {



        //(exp)	匹配exp,并捕获文本到自动命名的组里
        //(?<name>exp)	匹配exp,并捕获文本到名称为name的组里，也可以写成 (?'name'exp)
        //(?:exp)	匹配exp,不捕获匹配的文本，也不给此分组分配组号

        //(?=exp)	匹配exp前面的位置
        //(?<=exp)	匹配exp后面的位置
        //(?!exp)	匹配后面跟的不是exp的位置
        //(?<!exp)	匹配前面不是exp的位置
        //(?#comment)	这种类型的分组不对正则表达式的处理产生任何影响，用于提供注释让人阅 读
        //(?is)是模式修改符。意思是将后面的匹配模式修改成IGNORECASE和SINGLELINE，IGNORECASE就是忽略大小写，SINGLELINE就是规定特殊字符“.” 匹配任意的字符，包括换行符。默认情况下，特殊字符“.”不匹配换行符。
        //获取页面编码
        public readonly static string Encodstr = @"charset(\S)+""";
        public readonly static string NumStr = @"\d{0,4}";
        public readonly static string ABCStr = @"^[A-Za-z]+$";
        public readonly static string EmailStr = @"^(\w)+(\.\w+)*@(\w)+((\.\w{2,3}){1,3})$";

        //获取head部分
        public readonly static string HtmlHead = @"(?is)(<head).*?(</head>)";

        //获取页面title
        public readonly static string HtmlTitle = @"(?is)(<title).*?</title>";

        public readonly static string HtmlIFIE = @"(?is)(<\!--\[if).*?(endif\]-->)";


       

        //获取meta信息
        public readonly static string HtmlMeta = @"<meta.*?(/)?>";
        public readonly static string SpecialStr = "[`~!@#$%^&*()+=|{}':;',\\[\\].<>?~！@#￥%……&*（）——+|{}【】‘；：”“’。，、？]";

        //获取Body信息
        public readonly static string HtmlBody = @"(?is)(<body).*?(</body>)";
        public readonly static string HtmlDiv = @"(?is)(<div).*?(</div>)";
        public readonly static string HtmlStartForm = @"(?is)(<Form).*?(>)";

        public readonly static string HtmlTbody = @"(?is)(<tbody).*?(>)";

        public readonly static string HtmlLink = @"(?<=)<link.*?(/>)";
        public readonly static string HtmlHref = "(?is)href=\".*?(\")";
        public readonly static string HtmlHrefText = "(?is)<a.*?(</a>)";
        public readonly static string HtmlTag = "(?is)(<html).*?(>)";

        public readonly static string HtmlTagContent = "(?is)(<html).*?(</html>)";

        public readonly static string HtmlAnnotation = "(?is)(<!--).*?(-->)";  //注释
        public readonly static string HtmlOther = "(?is)(<!--\\[if).*?(\\]-->)"; //
        public readonly static string HtmlImg = "(?is)((?is)(<img).*?(>)"; //

        public readonly static string HtmlAllContent = "<[^>]+>|</[^>]+>";

        public readonly static string HtmlAllContent1 = "<[^>]+>";
        public readonly static string HtmlHref1 = "(?is)(href=\"(?!http|javascript|#)).*?(\")";
        public readonly static string DateTimeHtml = "(?is)(\\d{4,4}-\\d{1,2}-\\d{1,2})";
        public readonly static string ScriptHtml = @"<script[\s\S]*?<\/script>";

        public static string HtmlRegex;

        public static string HtmlCustom = "(?is)({0}).*?({1})";

        public static string HttpRegedx = "";

        public static string StartRegexStr;

        public static string EndRegexStr;
        /// <summary>
        /// 根据正则返回字符串
        /// </summary>
        /// <param name="reStr">正则表达式</param>
        /// <param name="content">要匹配的内容</param>
        /// <returns>返回值</returns>
        public static string RegexByStr(string reStr, string content)
        {
            string str = "";
            Regex re = new Regex(reStr);
            Match mth = re.Match(content);
            //  Regex.Replace(input, @"<(.[^>]*)>", "", RegexOptions.IgnoreCase);
            if (mth.Success)
            {
                str = mth.Value;

            }
            else
            {
                str = "Error";
            }

            return str;
        }

        /// <summary>
        /// 根据正则返回字符串列表
        /// </summary>
        /// <param name="reStr">正则表达式</param>
        /// <param name="content">要匹配的内容</param>
        /// <returns>返回值</returns>
        public static List<string> RegexByStrReturnList(string reStr, string content)
        {
            List<string> strList = null;
            MatchCollection mc = null;
            try
            {
                Regex re = new Regex(reStr);

                mc = re.Matches(content);
                if (mc.Count > 0)
                {
                    strList = new List<string>();
                    for (int i = 0; i < mc.Count; i++)
                    {
                        strList.Add(mc[i].Value);
                    }
                }
            }
            catch (Exception e)
            {
                strList = new List<string>();
                strList.Add("不能包含特殊字符");
            }

            return strList;
        }



        /// <summary>
        /// 根据正则返回字符串列表
        /// </summary>
        /// <param name="reStr">正则表达式</param>
        /// <param name="content">要匹配的内容</param>
        /// <returns>返回值</returns>
        public static List<string> RegexByStrReturnList(string startStr, string endStr, string content)
        {
            List<string> strList = null;
            MatchCollection mc = null;

            string reStr = string.Format(HtmlCustom, startStr, endStr);
            // Regex re = new Regex(reStr);

            strList = RegexByStrReturnList(reStr, content);


            return strList;
        }


        public static string GetHtmlByRegex(string startStr, string endStr, string content, bool _repalce = false)
        {
            string reg = string.Format(HtmlCustom, startStr, endStr);
            string html = RegexByStr(reg, content);
            if (_repalce)
            {
                html = html.Replace(startStr, "").Replace(endStr, "");
            }
            return html;

        }

        public static string SubStr(string startStr, string endStr, string content)
        {
            string result = "";
            result = content.Substring((content.IndexOf(startStr)), (content.IndexOf(endStr)));
            result = content.Replace(result, "");
            return result;
        }

        public static string[] SplitByString(string strText, string _splitstr)
        {
            return Regex.Split(strText, _splitstr, RegexOptions.IgnoreCase);

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="startStr"></param>
        /// <param name="endStr"></param>
        /// <param name="content"></param>
        /// <returns></returns>
        public static List<string> SubByStrReturnList(string startStr, string endStr, string content)
        {
            List<string> strList = null;
            int start = -1;
            int end = -1;
            start = content.IndexOf(startStr);
            end = content.IndexOf(endStr);

            return strList;
        }


        public static string RegexReolaceHtml(string content, string regexStr)
        {
            return Regex.Replace(content, regexStr, " ");

        }


        public static string SubStrByStr(string strContent, string startStr, string endStr)
        {


            string result = "";
            int a = strContent.IndexOf(startStr);

            result = strContent;
            if (a > 0)
            {
                int b = (strContent.IndexOf(endStr) - a) + endStr.Length;
                if (b > 0)
                {
                    result = strContent.Substring(a, b);
                }
            }
            return result;
        }

        /// <summary>
        /// 验证
        /// </summary>
        /// <param name="strIn"></param>
        /// <param name="_regstr"></param>
        /// <returns></returns>
        public static bool IsValidStr(string strIn, string _regstr)
        {
            return Regex.IsMatch(strIn, _regstr);
        }
    }
}
