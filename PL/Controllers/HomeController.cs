using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TP.BL.Services;
using TP.BL.DTO;
using TP.BL.Extensions;
using TP.PL.Models;

namespace TP.PL.Controllers
{
    public class HomeController : Controller
    {
        Identity Identity = new Identity();

        PageInfo pageInfo = PageInfo.Create("Home");

        public ActionResult Home()
        {       
            ViewBag.Page = pageInfo.setView("Home");
            return View();
        }

        public ActionResult Games()
        {
            ViewBag.Page = pageInfo.setView("Games");
            return View();
        }

        public ActionResult Support()
        {
            ViewBag.Page = pageInfo.setView("Support");
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Page = pageInfo.setView("About");
            return View();
        }

        [HttpGet]
        public ActionResult Entry()
        {
            ViewBag.Page = pageInfo.setView("Entry");
            return View();
        }

        [HttpPost]
        public ActionResult Entry(Entry Model)
        {
            ViewBag.Page = pageInfo.setView("Entry");
            if (ModelState.IsValid)
            {
                if (Identity.Authentication(Model.Login, Model.Password)) return Redirect(Request.UrlReferrer.AbsolutePath);
                else ModelState.AddModelError("Password", "Пароль не верный");
            }
            return View();
        }

        [HttpGet]
        public ActionResult Registration()
        {
            ViewBag.Page = pageInfo.setView("Registration");
            return View();
        }

        [HttpPost]
        public ActionResult Registration(Registration Model)
        {
            ViewBag.Page = pageInfo.setView("Registration");
            if (ModelState.IsValid)
            {
                Crypt crypt = new Crypt();
                Model.Login = Model.Login.ToUpper();
                Model.Password = crypt.Encrypt(Model.Password, Model.Login);
                UserServices.I.Create(Mapper<User, Registration>.Transformation(Model));
                Identity.Authentication(Model.Login, Model.RePassword);
                return Redirect(Request.UrlReferrer.AbsolutePath);
            }
            else return View();
        }

        [HttpGet]
        public ActionResult Exit()
        {
            Identity.clearAuthentication();
            return Redirect(Request.UrlReferrer.ToString());
        }

        [HttpGet]
        public JsonResult ECheckLogin(string Login)
        {
            var result = UserServices.I.Find(x => x.Login == Login).Any();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult RCheckLogin(string Login)
        {
            var result = !UserServices.I.Find(x => x.Login == Login).Any();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult RCheckEmail(string Email)
        {
            var result = !UserServices.I.Find(x => x.Email == Email).Any();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult RCheckPhone(string PhoneNumber)
        {
            var result = !UserServices.I.Find(x => x.PhoneNumber == PhoneNumber).Any();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult AuthenticationLogin()
        {
            if (Identity.isAuthentication) return Json(Identity.User.Login, JsonRequestBehavior.AllowGet);
            else return Json("", JsonRequestBehavior.AllowGet);
        }
    }
}