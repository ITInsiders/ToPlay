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
                name: "Game",
                url: "Game/{action}/{hash}",
                defaults: new { controller = "Game", action = "Main", hash = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Home",
                url: "{action}/{key}",
                defaults: new { controller = "Home", action = "Main", key = UrlParameter.Optional }
            );
            
        }
    }
}
