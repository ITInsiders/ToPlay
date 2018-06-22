using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TP.PL.Helpers;
using TP.PL.Models;
using TP.BL.Services;
using TP.ML.Entities;
using TP.BL.Extensions;

namespace TP.PL.Controllers
{
    public class HomeController : Controller
    {
        PageInfo Info = new PageInfo("Home");

        [HttpGet]
        public ActionResult Main()
        {
            ViewBag.Info = Info.setView("Main");
            return View();
        }

        public ActionResult Games()
        {
            ViewBag.Info = Info.setView("Games");
            return View();
        }

        [HttpGet]
        public ActionResult Entry()
        {
            ViewBag.Info = Info.setView("Entry");
            return View();
        }

        [HttpPost]
        public ActionResult Entry(Entry Model)
        {
            ViewBag.Info = Info.setView("Entry");
            if (ModelState.IsValid)
            {
                if (Info.Identity.Authentication(Model.Login.ToUpper(), Model.Password)) return Redirect(Request.UrlReferrer.AbsolutePath);
                else ModelState.AddModelError("Password", "Пароль не верный");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Registration()
        {
            ViewBag.Info = Info.setView("Registration");
            return View();
        }

        [HttpPost]
        public ActionResult Registration(Registration Model)
        {
            ViewBag.Info = Info.setView("Registration");
            if (ModelState.IsValid)
            {
                Service<Gamer> ServiceGamer = Service<Gamer>.I;
                Model.Password = Hash.GetHash(Hash.TypeHash.SHA512, Model.Password, Hash.GenerateSalt(Model.Login));
                ServiceGamer.Create(Model.Gamer);
                ServiceGamer.SaveFromDataBase();
                return Redirect(Request.UrlReferrer.AbsolutePath);
            }
            else return View();
        }

        [HttpGet]
        public ActionResult Exit()
        {
            Info.Identity.clearAuthentication();
            return Redirect(Request.UrlReferrer.ToString());
        }

        [HttpGet]
        public JsonResult ECheckLogin(string Login)
        {
            var result = Service<User>.I.Any(x => x.Login == Login);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult RCheckLogin(string Login)
        {
            var result = !Service<User>.I.Any(x => x.Login == Login);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult RCheckEmail(string Email)
        {
            var result = !Service<User>.I.Any(x => x.Email == Email);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult RCheckPhone(string Phone)
        {
            var result = !Service<User>.I.Any(x => x.PhoneNumber == Phone);
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult AuthenticationLogin()
        {
            if (Info.Identity.isAuthentication)
                return Json(Info.Identity.User.Login, JsonRequestBehavior.AllowGet);
            else
                return Json("", JsonRequestBehavior.AllowGet);
        }
    }
}