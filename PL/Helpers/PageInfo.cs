using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TP.PL.Helpers
{
    public class PageInfo
    {
        public string Controller { get; set; }
        public string View { get; set; }
        public string Title { get; set; }
        public string Layout { get; set; }
        public bool isPart { get; set; }

        public LanguageDictionary Language { get; set; }
        public string GetLanguage
        {
            get { return Language.GetLanguage().ToString(); }
            set { Language.SetStringLanguage(value); }
        }

        public Identity Identity { get; set; }
        
        private string ResourseController => "Layout/" + Controller;
        private string ResourseView => Controller + "/" + View;

        public string LayoutHref => "~/Views/Layout/" + Controller + ".cshtml";
        
#if DEBUG
        public string StyleController => "~/Resources/CSS/" + ResourseController + ".css" ;
        public string StyleView => "~/Resources/CSS/" + ResourseView + ".css";
        public string ScriptController => "~/Resources/JS/" + ResourseController + ".js";
        public string ScriptView => "~/Resources/JS/" + ResourseView + ".js";
#else
        public string StyleController => "~/Resources/CSS/" + ResourseController + ".min.css";
        public string StyleView => "~/Resources/CSS/" + ResourseView + ".min.css";
        public string ScriptController => "~/Resources/JS/" + ResourseController + ".min.js";
        public string ScriptView => "~/Resources/JS/" + ResourseView + ".min.js";
#endif

        public PageInfo(string Controller, string View = null)
        {
            this.Language = new LanguageDictionary();
            this.Controller = Controller;
            this.Layout = Controller;
            this.View = View ?? "";
            this.Title = (View ?? "") + " | " + Controller + " | ToPlay";
            this.Identity = new Identity();
            this.isPart = false;
        }

        public PageInfo setController(string Controller)
        {
            this.Controller = Controller;
            this.Title = (View ?? "") + " | " + this.Controller + " | ToPlay";
            return this;
        }

        public PageInfo setView(string View)
        {
            this.View = View;
            this.Title = this.View + " | " + this.Controller + " | ToPlay";
            return this;
        }

        public PageInfo setTitle(string Title)
        {
            this.Title = Title;
            return this;
        }

        public PageInfo setPart(bool isPart)
        {
            this.isPart = isPart;
            return this;
        }
    }
}