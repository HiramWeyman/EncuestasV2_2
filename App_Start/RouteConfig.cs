using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace EncuestasV2
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Usuarios", action = "Agregar", id = UrlParameter.Optional }
            );

            //routes.MapRoute(
            //name: "RuraUsuario",
            //url: "Login/EncuestaPrueba/{user}",
            //defaults: new { controller = "Login", action = "EncuestaPrueba", user = UrlParameter.Optional }
            //  );
        }
    }
}
