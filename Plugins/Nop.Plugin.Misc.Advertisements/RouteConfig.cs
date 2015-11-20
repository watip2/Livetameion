using Nop.Plugin.Misc.Advertisements.Infrastructure;
using Nop.Web.Framework.Mvc.Routes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace Nop.Plugin.Misc.Advertisements
{
    public class RouteConfig : IRouteProvider
    {
        public int Priority
        {
            get { return 0; }
        }

        public void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute("Plugin.Misc.GroupDeals.AdsController",
                "Ads/{action}/{id}",
                new { controller = "Ads", action = "Index", id = UrlParameter.Optional },
                new[] { "Nop.Plugin.Misc.GroupDeals.Controllers" }
            );

            ViewEngines.Engines.Insert(0, new CustomViewEngine());
        }
    }
}
