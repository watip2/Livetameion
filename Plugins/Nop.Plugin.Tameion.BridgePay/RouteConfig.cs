using Nop.Plugin.Tameion.BridgePay.Infrastructure;
using Nop.Web.Framework.Mvc.Routes;
using System.Web.Mvc;
using System.Web.Routing;

namespace Nop.Plugin.Tameion.BridgePay
{
    public class RouteConfig : IRouteProvider
    {
        public int Priority
        {
            get { return 0; }
        }

        public void RegisterRoutes(RouteCollection routes)
        {
            var route = routes.MapRoute("Plugin.Tameion.BridgePay.BridgePayController",
                "Admin/BridgePay/{action}/{id}",
                new { area = "Admin", controller = "BridgePay", action = "", id = UrlParameter.Optional },
                new[] { "Nop.Plugin.Tameion.BridgePay.Controllers" }
            );
            
            ViewEngines.Engines.Insert(0, new CustomViewEngine());
        }
    }
}
