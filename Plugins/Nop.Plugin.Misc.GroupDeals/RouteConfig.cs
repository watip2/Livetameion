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
            routes.MapRoute("Plugin.Misc.GroupDeals.GroupDealsController",
                "GroupDeals/{action}/{id}",
                new { controller = "GroupDeals", action = "Index", id = UrlParameter.Optional },
                new[] { "Nop.Plugin.Misc.GroupDeals.Controllers" }
            );

            routes.MapRoute("Plugin.Misc.GroupDeals.VendorGroupDealsController",
                "VendorGroupDeals/{action}/{id}",
                new { controller = "VendorGroupDeals", action = "Index", id = UrlParameter.Optional },
                new[] { "Nop.Plugin.Misc.GroupDeals.Controllers" }
            );

            ViewEngines.Engines.Insert(0, new CustomViewEngine());
        }
    }
}
