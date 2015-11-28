using Nop.Plugin.Misc.GroupDeals.Views;
using Nop.Web.Framework.Mvc.Routes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace Nop.Plugin.Misc.GroupDeals
{
    public class RouteConfig : IRouteProvider
    {
        public int Priority
        {
            get { return 0; }
        }

        public void RegisterRoutes(RouteCollection routes)
        {
            var route = routes.MapRoute("Plugin.Misc.GroupDeals.Areas.Vendor.GroupdealsController",
                "Vendor/Groupdeals/{action}/{id}",
                new { area = "Vendor", controller = "Groupdeals", action = "Index", id = UrlParameter.Optional },
                new[] { "Nop.Plugin.Misc.GroupDeals.Areas.Vendor.Controllers" }
            );
            routes.Remove(route);
            routes.Insert(0, route);

            route = routes.MapRoute("Plugin.Misc.GroupDeals.Areas.Admin.GroupdealsController",
                "Admin/Groupdeals/{action}/{id}",
                new { area = "Admin", controller = "Groupdeals", action = "Index", id = UrlParameter.Optional },
                new[] { "Nop.Plugin.Misc.GroupDeals.Areas.Admin.Controllers" }
            );//.DataTokens.Add("area", "Admin")
            routes.Remove(route);
            routes.Insert(0, route);

            ViewEngines.Engines.Insert(0, new CustomViewEngine());
        }
    }
}
