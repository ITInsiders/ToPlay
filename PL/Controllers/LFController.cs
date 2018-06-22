using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TP.PL.Helpers;

namespace TP.PL.Controllers
{
    public class LFController : Controller
    {
        PageInfo pageInfo = new PageInfo("LF");

        [HttpGet]
        public ActionResult Main()
        {
            ViewBag.Info = pageInfo.setView("Main");
            return View();
        }

        [HttpGet]
        public ActionResult Game()
        {
            ViewBag.Info = pageInfo.setView("Game");
            return View();
        }
    }
}