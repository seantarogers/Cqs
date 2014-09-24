using System.Web.Mvc;
using System.Web.Routing;

namespace Cqs.Presentation.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.MapMvcAttributeRoutes();

            routes.MapRoute(
              "Default",
              "{controller}/{action}",
              new { controller = "Customer", action = "Index" });
        }
    }
}
