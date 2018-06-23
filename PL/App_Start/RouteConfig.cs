using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TP
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "LF",
                url: "LF/{Id}",
                defaults: new { controller = "LF", action = "Main", Id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "IO",
                url: "IO/{Id}",
                defaults: new { controller = "IO", action = "Main", Id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Helper",
                url: "Helper/{action}/{key}",
                defaults: new { controller = "Helper", action = "404", key = UrlParameter.Optional }
            );
            
            routes.MapRoute(
                name: "Home",
                url: "Home/{action}/{key}",
                defaults: new { controller = "Home", action = "Main", key = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{key}",
                defaults: new { controller = "Home", action = "Main", key = UrlParameter.Optional }
            );
        }
    }
}
