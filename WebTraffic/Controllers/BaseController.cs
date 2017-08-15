using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WebTraffic.Models;

namespace WebTraffic.Controllers
{
    public class BaseController : Controller
    {
        protected int BaseUserID;
        protected string BaseUserName;
        protected TrafficEntities BaseModelDB = new TrafficEntities();
        // GET: Base
        public BaseController()
        {
          
        }
        protected override void Initialize(RequestContext requestContext)
        {
            base.Initialize(requestContext);
            //校验用户是否已经登录
            if (requestContext.HttpContext.Session["trafficUserID"] == null || requestContext.HttpContext.Session["trafficUserName"] == null || int.Parse(requestContext.HttpContext.Session["trafficUserID"].ToString()) <= 0)
            {
                //跳转到登陆页
                requestContext.HttpContext.Response.Redirect("/login/index");
            }
            else
            {
                BaseUserID = int.Parse(requestContext.HttpContext.Session["trafficUserID"].ToString());
                BaseUserName = requestContext.HttpContext.Session["trafficUserName"].ToString();
            }
          
        }
       

        protected override void Dispose(bool disposing)
        {
            BaseModelDB.Dispose();
            base.Dispose(disposing);
        }
    }
}