using Nop.Web.Framework.Mvc.Routes;
using System.Web.Mvc;
using System.Web.Routing;

namespace Nop.Plugin.Tameion.SelectAndSell
{
    public class RouteConfig : IRouteProvider
    {
        public int Priority
        {
            get { return 0; }
        }

        public void RegisterRoutes(RouteCollection routes)
        {
            var route = routes.MapRoute("Plugin.Tameion.SelectAndSell.SelectSellController",
                "Vendor/SelectSell/{action}/{id}",
                new { area = "Vendor", controller = "SelectSell", action = "Index", id = UrlParameter.Optional },
                new[] { "Nop.Plugin.Tameion.SelectAndSell.Controllers" }
            );
            routes.Remove(route);
            routes.Insert(0, route);
            
            //ViewEngines.Engines.Insert(0, new CustomViewEngine());
        }
    }
}
