using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebTraffic.Alipay;
using WebTraffic.Common;
using WebTraffic.Models;
using WebTraffic.WebCollect;
using Newtonsoft.Json;
using WebTraffic.FilterAction;
namespace WebTraffic.Controllers
{
    public class HomeController : BaseController
    {
       
        Web_Spider web = new Web_Spider();
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        [LoginFilter]
        public ActionResult Index()
        {
           
            Task taskItem = new Task();
            int page = 1;
            page = string.IsNullOrWhiteSpace(Request.Params["page"]) ? 1 : int.Parse(Request.Params["page"]);
            Dictionary<string, string> dic = new Dictionary<string, string>();
            List<Task> taskList = BaseModelDB.Task.ToList();
            var pageItem = taskItem.PageList(page, 10, "/home/index", ref taskList);
            ViewBag.PageData = pageItem;
            ViewBag.ListItem = taskList;
         
          
            // modelDB.Task
            return View();
        }
        /// <summary>
        /// 添加任务
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public JsonResult AddTask()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            Task task = new Task();

            task.Title = "";
            task.Url = string.IsNullOrWhiteSpace(Request.Params["ArticleUrl"]) ? "" : Request.Params["ArticleUrl"];
            if (task.Url.Length > 2)
            {
                web.Url = task.Url;
                string webHtml = web.getHtmlContentByUrl();
                task.Title = Html_Regex.RegexByStr(Html_Regex.HtmlTitle, webHtml);
                task.Title = Html_Regex.RegexReolaceHtml(task.Title, Html_Regex.HtmlAllContent);
            }
            task.UserID = BaseUserID;
            task.UserWecat = string.IsNullOrWhiteSpace(Request.Params["UserWecat"]) ? 0 : int.Parse(Request.Params["UserWecat"].ToString());
            task.TransmitWecat = string.IsNullOrWhiteSpace(Request.Params["TransmitWecat"]) ? 0 : int.Parse(Request.Params["TransmitWecat"]);
            task.FriendWecat = string.IsNullOrWhiteSpace(Request.Params["FriendWecat"]) ? 0 : int.Parse(Request.Params["FriendWecat"]);
            task.UserName = BaseUserName;
            if (!string.IsNullOrWhiteSpace(Request.Params["Vip"]))
            {
                task.Vip = int.Parse(Request.Params["Vip"]) > 0;
            }
            else { task.Vip = false; }
            task.Praise = string.IsNullOrWhiteSpace(Request.Params["Praise"]) ? 0 : int.Parse(Request.Params["Praise"]);
            task.PraiseUnit = string.IsNullOrWhiteSpace(Request.Params["PraiseUnit"]) ? "" : Request.Params["PraiseUnit"];
            task.CreateTime = DateTime.Now;
            task.TaskStatus = 0;
            task.ReadNum = string.IsNullOrWhiteSpace(Request.Params["ReadNum"]) ? 0 : int.Parse(Request.Params["ReadNum"]);
            task.Speed = string.IsNullOrWhiteSpace(Request.Params["Speed"]) ? 0 : int.Parse(Request.Params["Speed"]);
            BaseModelDB.Task.Add(task);
            int n = 0;
            try
            {
                n = BaseModelDB.SaveChanges();
            }
            catch (Exception ex)
            {
            }
            if (n > 0)
            {
                dic.Add("status", "200");
                dic.Add("msg", "添加成功");
            }
            else
            {
                dic.Add("status", "300");
                dic.Add("msg", "添加失败");
            }
            return Json(dic);
        }

        public ActionResult test() {
            return Content("<h1>Hello World!</h1>");
        }


        public ActionResult Pay()
        {
            return View();
        }


        public ActionResult PayAction()
        {
            //商户订单号，商户网站订单系统中唯一订单号，必填
            string out_trade_no = DateTime.Now.ToString("yyyyMMddHHmmss");

            //订单名称，必填
            string subject = "测试";

            //付款金额，必填
            string total_fee = "0.1";

            //商品描述，可空
            string body ="测试";

            //Recharge rechargeModel = new Recharge();
            //rechargeModel.Moneys = decimal.Parse(Request.Params["payMoney"]);
            //rechargeModel.OrderNum = out_trade_no;
            //rechargeModel.CreateTime = DateTime.Now;
            //rechargeModel.UserID = 1;
            //rechargeModel.UserName = "";
            //rechargeModel.FromType = "alipay";
            //rechargeModel.Remarks = Request.Params["Remarks"];
            Orders orderModel = new Orders();
            orderModel.OrderNum = out_trade_no;
            orderModel.Moneys= decimal.Parse(Request.Params["payMoney"]);
            orderModel.OrderStatus = 0;
            orderModel.UserID = int.Parse(Session["trafficUserID"].ToString());
            orderModel.UserName = Session["trafficUserName"].ToString();
            orderModel.PayType = "alipay";
            BaseModelDB.Orders.Add(orderModel);

            ////////////////////////////////////////////////////////////////////////////////////////////////
            BaseModelDB.SaveChanges();
            //把请求参数打包成数组
            SortedDictionary<string, string> sParaTemp = new SortedDictionary<string, string>();
            sParaTemp.Add("service", Config.service);
            sParaTemp.Add("partner", Config.partner);
            sParaTemp.Add("seller_id", Config.seller_id);
            sParaTemp.Add("_input_charset", Config.input_charset.ToLower());
            sParaTemp.Add("payment_type", Config.payment_type);
            sParaTemp.Add("notify_url", Config.notify_url);
            sParaTemp.Add("return_url", Config.return_url);
            sParaTemp.Add("anti_phishing_key", Config.anti_phishing_key);
            sParaTemp.Add("exter_invoke_ip", Config.exter_invoke_ip);
            sParaTemp.Add("out_trade_no", out_trade_no);
            sParaTemp.Add("subject", subject);
            sParaTemp.Add("total_fee", total_fee);
            sParaTemp.Add("body", body);
            //其他业务参数根据在线开发文档，添加参数.文档地址:https://doc.open.alipay.com/doc2/detail.htm?spm=a219a.7629140.0.0.O9yorI&treeId=62&articleId=103740&docType=1
            //如sParaTemp.Add("参数名","参数值");

            //建立请求
            string sHtmlText = Submit.BuildRequest(sParaTemp, "post", "确认");
           // Response.Write(sHtmlText);
            return Content(sHtmlText);
        }

        public ActionResult UpdatePwd()
        {
            return View();
        }

        [HttpPost]
        public JsonResult UpdatePwdAction()
        {

            Dictionary<string, string> dic = new Dictionary<string, string>();
            string oldpwd = string.IsNullOrWhiteSpace(Request.Params["oldpwd"]) ? "" : Request.Params["oldpwd"].Trim();
            string newpwd = string.IsNullOrWhiteSpace(Request.Params["newpwd"]) ? "" : Request.Params["newpwd"].Trim();
            string newpwd1 = string.IsNullOrWhiteSpace(Request.Params["newpwd1"]) ? "" : Request.Params["newpwd1"].Trim();
            if (oldpwd.Length > 0 && newpwd.Length > 0 && newpwd1.Length > 0)
            {
                if (newpwd != newpwd1)
                {
                    dic.Add("status", "300");
                    dic.Add("msg", "新密码不一致!");
                }
                else {
                    string oldmd5 = CommonBll.MD5(oldpwd);
                   
                    var userItem=  BaseModelDB.Users.Where(x => x.ID == BaseUserID && x.UserPwd == oldmd5).FirstOrDefault();
                    if (userItem == null)
                    {
                        dic.Add("status", "300");
                        dic.Add("msg", "密码错误!");
                    }
                    else
                    {
                        userItem.UserPwd = CommonBll.MD5(newpwd);
                        BaseModelDB.Entry<Users>(userItem).State = System.Data.Entity.EntityState.Modified;
                        int n=BaseModelDB.SaveChanges();
                        if (n>0)
                        {
                            dic.Add("status", "200");
                            dic.Add("msg", "修改成功!");
                        }
                        else
                        {
                            dic.Add("status", "300");
                            dic.Add("msg", "修改失败!");
                        }

                    }
                }
            }
            else {
                dic.Add("status", "300");
                dic.Add("msg", "旧密码或新密码不能为空!");
            }
            return Json(dic);
        }
    }
}