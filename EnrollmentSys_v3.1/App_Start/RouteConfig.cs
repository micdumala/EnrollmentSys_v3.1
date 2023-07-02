using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EnrollmentSys_v3._1
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Preregister", action = "Home", id = UrlParameter.Optional }
            );

            routes.MapRoute(
                name: "NewStudent",
                url: "Admin/NewStudent/{RegId}",
                defaults: new { controller = "RegisterAccnt", action = "Index", RegId = UrlParameter.Optional }
            ) ;
        }
    }
}
