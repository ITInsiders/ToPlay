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
        
        private string ResourseController => "Main/" + Controller;
        private string ResourseView => Controller + "/" + View;

        public string LayoutHref => "~/Views/Layout/" + Controller + ".cshtml";

        
#if DEBUG
        public string StyleController => "~/Resources/CSS/" + ResourseController + ".css";
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
            this.Controller = Controller;
            this.Layout = Controller;
            this.View = View;
            this.Title = View + " | " + Controller + " | ToPlay";
        }

        public PageInfo setController(string Controller)
        {
            this.Controller = Controller;
            return this;
        }
        public PageInfo setView(string View)
        {
            this.View = View;
            return this;
        }
        public PageInfo setTitle(string Title)
        {
            this.Title = Title;
            return this;
        }
    }
}