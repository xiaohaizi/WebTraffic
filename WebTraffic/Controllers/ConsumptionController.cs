using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebTraffic.Controllers
{
    public class ConsumptionController : BaseController
    {
        // GET: Consumption
        public ActionResult Index()
        {
            return View();
        }
    }
}