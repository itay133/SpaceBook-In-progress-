using System.Web.Mvc;
using System.Web.Routing;

namespace SpacebookSpa
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                //defaults: new { controller = "Home", action = "BusinessIndex", id = UrlParameter.Optional }
                defaults: new { controller = "Home", action = "UserIndex", id = UrlParameter.Optional }
                
            );
        }
    }
}
