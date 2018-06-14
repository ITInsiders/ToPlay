using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TP.BL.Services;
using TP.ML.Helper;

namespace TP.PL.Helpers
{
    public class LanguageDictionary
    {
        private static HttpContext HC => HttpContext.Current;
        private Languages LNG;

        public LanguageDictionary()
        {
            GetLanguageInCookies();
        }

        public LanguageDictionary(Languages LNG)
        {
            SetLanguage(LNG);
        }

        private void GetLanguageInCookies()
        {
            if (HC.Request.Cookies["_language"] != null)
            {
                switch (HC.Request.Cookies["_language"].Value)
                {
                    case "De": this.LNG = Languages.De; break;
                    case "Ru": this.LNG = Languages.Ru; break;
                    default: this.LNG = Languages.En; break;
                }
            }
            else
            {
                this.LNG = Languages.En;
            }
        }

        public void SetLanguage(Languages LNG)
        {
            if (HC.Request.Cookies["_language"] != null)
            {
                HC.Response.Cookies["_language"].Expires = DateTime.Now.AddYears(-1);
            }

            HttpCookie Cookie = new HttpCookie("_language");
            Cookie.Expires = DateTime.Now.AddYears(10);
            Cookie.Value = LNG.ToString();
            HC.Response.Cookies.Add(Cookie);

            this.LNG = LNG;
        }

        public void SetStringLanguage(string LNG) => SetLanguageInCookie(LNG);

        public static void SetLanguageInCookie(string LNG)
        {
            switch (LNG)
            {
                case "En": SetLanguageInCookie(Languages.En); break;
                case "De": SetLanguageInCookie(Languages.De); break;
                case "Ru": SetLanguageInCookie(Languages.Ru); break;
            }
        }

        public static void SetLanguageInCookie(Languages LNG)
        {
            if (HC.Request.Cookies["_language"] != null)
            {
                HC.Response.Cookies["_language"].Expires = DateTime.Now.AddYears(-1);
            }

            HttpCookie Cookie = new HttpCookie("_language");
            Cookie.Expires = DateTime.Now.AddYears(10);
            Cookie.Value = LNG.ToString();
            HC.Response.Cookies.Add(Cookie);
        }

        public Languages GetLanguage()
        {
            return this.LNG;
        }

        public string Get(string Text) => (LNG != Languages.En) ? SystemNameService.I.Get(Text, LNG) ?? Text : Text;
        public string Get(string Text, string Table) => (LNG != Languages.En) ? SystemNameService.I.Get(Text, LNG) ?? Text : Text;
    }
}