using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TarifDefteri
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "tarifim",
                url: "tarifim",
                defaults: new { controller = "pages", action = "searchResultDetailPage", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "defterim",
                url: "defterim",
                defaults: new { controller = "pages", action = "searchResultPage", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "pages", action = "searchEnginePage", id = UrlParameter.Optional }
            );
        }
    }
}
