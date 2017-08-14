using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using WebTraffic.Models;
namespace WebTraffic.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Userlogin()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
           string username= Request.Params["username"]==null?"": Request.Params["username"].Trim();
            string userpwd = Request.Params["userpwd"]==null?"":Request.Params["userpwd"].Trim();
            if (userpwd.Length > 0) {
                byte[] b = System.Text.Encoding.Default.GetBytes(userpwd);

                b = new System.Security.Cryptography.MD5CryptoServiceProvider().ComputeHash(b);
                string ret = "";
                for (int i = 0; i < b.Length; i++)
                {
                    ret += b[i].ToString("x").PadLeft(2, '0');
                }
                userpwd = ret;
            }

            using (TrafficEntities DB=new TrafficEntities()) {
               var item= DB.Users.Where(x => x.UserName == username && x.UserPwd == userpwd).FirstOrDefault();
                if (item != null)
                {
                    Session["trafficUserID"] = item.ID;
                    Session["trafficUserName"] = item.UserName;
                    dic.Add("msg", "登录成功!");
                    dic.Add("status", "200");
                }
                else {
                    dic.Add("msg", "用户名或密码错误!");
                    dic.Add("status", "300");
                }
            }

                return Json(dic);
        }

        public void Logout()
        {
            Session["trafficUserID"] = null;
            Session["trafficUserName"] = null;
            Response.Redirect("/Login/Index");
        }
    }
}