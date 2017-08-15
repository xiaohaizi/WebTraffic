using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebTraffic.Alipay;
using WebTraffic.Common;
using Newtonsoft.Json;
using WebTraffic.Models;
using System.Data.Entity;

namespace WebTraffic.Controllers
{
    public class NotifyController : Controller
    {
        // GET: Notify
        
        public string  AlipayNotify()
        {
            string result = "fail";
           
            CommonBll.WriteTextFile("******" + DateTime.Now.ToString()+"开始*******************", "log\\log.txt");
            SortedDictionary<string, string> sPara = GetRequestPost();
            CommonBll.WriteTextFile(JsonConvert.SerializeObject(sPara), "log\\log.txt");           
            if (sPara.Count > 0)//判断是否有带返回参数
            {
                Notify aliNotify = new Notify();
                bool verifyResult = aliNotify.Verify(sPara, Request.Form["notify_id"], Request.Form["sign"]);
                if (verifyResult)//验证成功
                {
                    CommonBll.WriteTextFile("验证成功", "log\\log.txt");
                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                    //请在这里加上商户的业务逻辑程序代码

                    CommonBll.WriteTextFile(Request.Form["out_trade_no"]+"--"+ Request.Form["out_trade_no"]+"--"+ Request.Form["trade_status"], "log\\log.txt");
                    //——请根据您的业务逻辑来编写程序（以下代码仅作参考）——
                    //获取支付宝的通知返回参数，可参考技术文档中服务器异步通知参数列表

                    //商户订单号

                    string out_trade_no = Request.Form["out_trade_no"];

                    //支付宝交易号

                    string trade_no = Request.Form["trade_no"];

                    //交易状态
                    string trade_status = Request.Form["trade_status"];


                 if (Request.Form["trade_status"] == "TRADE_SUCCESS")
                    {
                        using (TrafficEntities ModelDB = new TrafficEntities())
                        {
                           var orderItem= ModelDB.Orders.Where(x => x.OrderNum == out_trade_no && x.OrderStatus == 0).FirstOrDefault();
                            if (orderItem != null)
                            {
                               var userItem= ModelDB.Users.Where(x => x.ID == orderItem.UserID).FirstOrDefault();
                                if (userItem != null)
                                {
                                    userItem.Balance = userItem.Balance + orderItem.Moneys;
                                    ModelDB.Entry<Users>(userItem).State = EntityState.Modified;
                                }
                                orderItem.OrderStatus = 1;
                                orderItem.UpdateTime = DateTime.Now;
                                ModelDB.Entry<Orders>(orderItem).State = EntityState.Modified;
                                Recharge item = new Recharge();
                                item.CreateTime = DateTime.Now;
                                item.FromType = "alipay";
                                item.Moneys = orderItem.Moneys;
                                item.OrderNum = orderItem.OrderNum;
                                item.UserID = orderItem.UserID;
                                item.UserName = orderItem.UserName;
                                item.OtherOrderNum = trade_no;
                                item.UpdateTime = DateTime.Now;
                                item.Remarks = orderItem.Remarks;
                                ModelDB.Entry<Recharge>(item).State = EntityState.Added;
                                ModelDB.SaveChanges();

                            }
                            result = "success";
                        }
                        //判断该笔订单是否在商户网站中已经做过处理
                        //如果没有做过处理，根据订单号（out_trade_no）在商户网站的订单系统中查到该笔订单的详细，并执行商户的业务程序
                        //请务必判断请求时的total_fee、seller_id与通知时获取的total_fee、seller_id为一致的
                        //如果有做过处理，不执行商户的业务程序

                        //注意：
                        //付款完成后，支付宝系统发送该交易状态通知
                    }
                    else
                    {
                    }

                    //——请根据您的业务逻辑来编写程序（以上代码仅作参考）——

                    //请不要修改或删除

                    /////////////////////////////////////////////////////////////////////////////////////////////////////////////
                }
                else//验证失败
                {
                    result="fail";
                }
            }

            return result;
            //return View();
        }

        /// <summary>
        /// 获取支付宝POST过来通知消息，并以“参数名=参数值”的形式组成数组
        /// </summary>
        /// <returns>request回来的信息组成的数组</returns>
        public SortedDictionary<string, string> GetRequestPost()
        {
            int i = 0;
            SortedDictionary<string, string> sArray = new SortedDictionary<string, string>();
            NameValueCollection coll;
            //Load Form variables into NameValueCollection variable.
            coll = Request.Form;

            // Get names of all forms into a string array.
            String[] requestItem = coll.AllKeys;

            for (i = 0; i < requestItem.Length; i++)
            {
                sArray.Add(requestItem[i], Request.Form[requestItem[i]]);
            }

            return sArray;
        }
    }
}