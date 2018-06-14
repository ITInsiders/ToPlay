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

        [HttpGet]
        public ActionResult Result()
        {
            ViewBag.Info = pageInfo.setView("Result");
            return View();
        }

        [HttpPost]
        public ActionResult Main(object Object)
        {
            ViewBag.Info = pageInfo.setView("Main").setPart(true);
            return View();
        }

        [HttpPost]
        public ActionResult Game(object Object)
        {
            ViewBag.Info = pageInfo.setView("Game").setPart(true);
            return View();
        }

        [HttpPost]
        public ActionResult Result(object Object)
        {
            ViewBag.Info = pageInfo.setView("Result");
            return View();
        }
    }
}