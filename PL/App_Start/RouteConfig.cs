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
                name: "Helper",
                url: "Helper/{action}/{key}",
                defaults: new { controller = "Helper", action = "404", key = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "InterestingOpinion",
                url: "IO/{action}/{key}",
                defaults: new { controller = "IO", action = "Main", key = UrlParameter.Optional }
            );
            
            routes.MapRoute(
                name: "Home",
                url: "Home/{action}/{key}",
                defaults: new { controller = "Home", action = "Main", key = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{action}/{key}",
                defaults: new { controller = "Home", action = "Main", key = UrlParameter.Optional }
            );
        }
    }
}
