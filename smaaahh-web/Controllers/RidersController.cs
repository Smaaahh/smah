using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace smaaahh_web.Controllers
{
    public class RidersController : Controller
    {
        // GET: Riders
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult Create()
        {
            return View();
        }
    }
}