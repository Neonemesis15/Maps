using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Xplora.GIS
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute(
            //    "Mayorista", // Route name
            //    "Mayorista/{*all}", // URL with parameters
            //    new { controller = "Reports", action = "Mayorista", all = UrlParameter.Optional } // Parameter defaults
            //);

            //routes.MapRoute(
            //    "Minorista", // Route name
            //    "Minorista/{*all}", // URL with parameters
            //    new { controller = "Reports", action = "Minorista", all = UrlParameter.Optional } // Parameter defaults
            //);

            //routes.MapRoute(
            //    "FarmaciasIT", // Route name
            //    "FarmaciasIT/{*all}", // URL with parameters
            //    new { controller = "Reports", action = "FarmaciasIT", all = UrlParameter.Optional } // Parameter defaults
            //);

            //routes.MapRoute(
            //    "FarmaciasDT", // Route name
            //    "FarmaciasDT/{*all}", // URL with parameters
            //    new { controller = "Reports", action = "FarmaciasDT", all = UrlParameter.Optional } // Parameter defaults
            //);

            //routes.MapRoute(
            //    "Bodegas", // Route name
            //    "Bodegas/{*all}", // URL with parameters
            //    new { controller = "Reports", action = "Bodegas", all = UrlParameter.Optional } // Parameter defaults
            //);

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Reports", action = "Bodegas", id = UrlParameter.Optional } // Parameter defaults
            );
        }

        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}