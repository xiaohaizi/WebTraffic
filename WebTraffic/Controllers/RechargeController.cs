using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebTraffic.Models;

namespace WebTraffic.Controllers
{
    public class RechargeController : Controller
    {
        TrafficEntities modelDB = new TrafficEntities();
        // GET: Recharge
        public ActionResult Index()
        {
            Recharge taskItem = new Recharge();
            int page = 1;
            page = string.IsNullOrWhiteSpace(Request.Params["page"]) ? 1 : int.Parse(Request.Params["page"]);
            Dictionary<string, string> dic = new Dictionary<string, string>();
            List<Recharge> itemList = new List<Recharge>();
            var pageItem = taskItem.PageList(page, 10, "/recharge/index", out itemList);
            ViewBag.PageData = pageItem;
            ViewBag.ListItem = itemList;
            // modelDB.Task
            return View();
        }
    }
}