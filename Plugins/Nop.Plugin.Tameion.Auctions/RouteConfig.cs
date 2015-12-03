using Nop.Plugin.Tameion.Auctions.Infrastructure;
using Nop.Web.Framework.Mvc.Routes;
using System.Web.Mvc;
using System.Web.Routing;

namespace Nop.Plugin.Tameion.Auctions
{
    public class RouteConfig : IRouteProvider
    {
        public int Priority
        {
            get { return 0; }
        }

        public void RegisterRoutes(RouteCollection routes)
        {
            var route = routes.MapRoute("Plugin.Tameion.Auctions.Public.AuctionsController",
                "Auctions/{action}/{id}",
                new { controller = "Auctions", action = "Index", id = UrlParameter.Optional },
                new[] { "Nop.Plugin.Tameion.Auctions.Areas.Public.Controllers" }
            );
            routes.Remove(route);
            routes.Insert(0, route);

            route = routes.MapRoute("Plugin.Tameion.Auctions.Vendor.AuctionsController",
                "Vendor/Auctions/{action}/{id}",
                new { area = "Vendor", controller = "Auctions", action = "Index", id = UrlParameter.Optional },
                new[] { "Nop.Plugin.Tameion.Auctions.Areas.Vendor.Controllers" }
            );
            routes.Remove(route);
            routes.Insert(0, route);

            route = routes.MapRoute("Plugin.Tameion.Auctions.Admin.AuctionsController",
                "Admin/Auctions/{action}/{id}",
                new { area = "Admin", controller = "Auctions", action = "Index", id = UrlParameter.Optional },
                new[] { "Nop.Plugin.Tameion.Auctions.Areas.Admin.Controllers" }
            );
            routes.Remove(route);
            routes.Insert(0, route);

            ViewEngines.Engines.Insert(0, new CustomViewEngine());
        }
    }
}
