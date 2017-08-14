using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebTraffic.FilterAction
{
    public class LoginFilterAttribute: ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

           
                //校验用户是否已经登录
                if (filterContext.HttpContext.Session["trafficUserID"] == null|| filterContext.HttpContext.Session["trafficUserName"] == null|| int.Parse(filterContext.HttpContext.Session["trafficUserID"].ToString()) <=0)
                {
                    //跳转到登陆页
                    filterContext.HttpContext.Response.Redirect("/Login/Index");
                }
                
            }
        
    }
}