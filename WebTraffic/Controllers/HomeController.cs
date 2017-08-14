using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebTraffic.Models;
using WebTraffic.WebCollect;

namespace WebTraffic.Controllers
{
    public class HomeController : Controller
    {
        TrafficEntities modelDB = new TrafficEntities();
        Web_Spider web = new Web_Spider();
        /// <summary>
        /// 首页
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            Task taskItem = new Task();
            int page = 1;
            page = string.IsNullOrWhiteSpace(Request.Params["page"]) ? 1 : int.Parse(Request.Params["page"]);
            Dictionary<string, string> dic = new Dictionary<string, string>();
            List<Task> taskList = new List<Task>();
            var pageItem = taskItem.PageList(page, 10, "/home/index", out taskList);
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
            if(task.Url.Length>2)
            {
                web.Url = task.Url;
              string webHtml=  web.getHtmlContentByUrl();
                task.Title= Html_Regex.RegexByStr(Html_Regex.HtmlTitle, webHtml);
                task.Title = Html_Regex.RegexReolaceHtml( task.Title, Html_Regex.HtmlAllContent);
            }
           task.UserID = 0;
            task.UserWecat = string.IsNullOrWhiteSpace(Request.Params["UserWecat"]) ? 0 : int.Parse(Request.Params["UserWecat"].ToString());
            task.TransmitWecat = string.IsNullOrWhiteSpace(Request.Params["TransmitWecat"]) ? 0 : int.Parse(Request.Params["TransmitWecat"]);
            task.FriendWecat = string.IsNullOrWhiteSpace(Request.Params["FriendWecat"]) ? 0 : int.Parse(Request.Params["FriendWecat"]);
            task.UserName = "";
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
            modelDB.Task.Add(task);
            int n = 0;
            try
            {
                n = modelDB.SaveChanges();
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

    }
}