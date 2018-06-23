using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TP.PL.Helpers;

namespace TP.PL.Controllers
{
    public class IOController : Controller
    {
        PageInfo pageInfo = new PageInfo("IO");

        [HttpGet]
        public ActionResult Main(string Id = null)
        {
            ViewBag.Info = pageInfo.setView("Main");
            ViewBag.Id = Id;
            return View();
        }

        [HttpPost]
        public ActionResult Main(object Object, string Id = null)
        {
            ViewBag.Info = pageInfo.setView("Main").setPart(true);
            ViewBag.Id = Id;
            return View();
        }
    }
}