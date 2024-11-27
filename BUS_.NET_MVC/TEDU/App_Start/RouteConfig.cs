using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace TEDU
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapRoute(
                name: "Home",
                url: "Trang-chu/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "TEDU.Controllers" }
            );

            routes.MapRoute(
                name: "About",
                url: "ve-chung-toi/{id}",
                defaults: new { controller = "Home", action = "About", id = UrlParameter.Optional },
                namespaces: new[] { "TEDU.Controllers" }
            );

            routes.MapRoute(
                name: "Contact",
                url: "lien-he/{id}",
                defaults: new { controller = "Home", action = "Contact", id = UrlParameter.Optional },
                namespaces: new[] { "TEDU.Controllers" }
            );

            routes.MapRoute(
                name: "Schedule",
                url: "lich-trinh/{id}",
                defaults: new { controller = "Home", action = "Schedule", id = UrlParameter.Optional },
                namespaces: new[] { "TEDU.Controllers" }
            );

            routes.MapRoute(
                name: "News",
                url: "tin-tuc/{id}",
                defaults: new { controller = "Home", action = "News", id = UrlParameter.Optional },
                namespaces: new[] { "TEDU.Controllers" }
            );

            routes.MapRoute(
                name: "Lookup",
                url: "tra-cuu-ve/{id}",
                defaults: new { controller = "Home", action = "Lookup", id = UrlParameter.Optional },
                namespaces: new[] { "TEDU.Controllers" }
            );

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "TEDU.Controllers" }
            );

        }

    }
}
