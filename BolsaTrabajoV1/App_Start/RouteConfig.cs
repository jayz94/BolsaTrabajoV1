using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace BolsaTrabajoV1
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "AdministracionCatalogos", action = "MainCatalogos", id = UrlParameter.Optional }
            );
            //defino mis propias rutas
            routes.MapRoute(
                name: null,
                url: "AreaConocimiento",
                defaults: new { controller = "AreaConocimiento", action = "Index", id = UrlParameter.Optional }
                );

            //terminan rutas primarias de catalogos
            routes.MapRoute(
               name: null,
               url: "TipoHabilidad",
               defaults: new { controller = "TipoHabilidad", action = "Index", id = UrlParameter.Optional }
               );
        }
    }
}
