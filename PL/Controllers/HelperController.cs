using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TP.PL.Helpers;

namespace TP.PL.Controllers
{
    public class HelperController : Controller
    {
        public struct ChangeLanguageStruct
        {
            public string language { get; set; }
        }

        [HttpPost]
        public ActionResult ChangeLanguage(string language)
        {
            LanguageDictionary.SetLanguageInCookie(language);
            return Redirect(Request.UrlReferrer.AbsolutePath);
        }
    }
}