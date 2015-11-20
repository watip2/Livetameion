using Nop.Plugin.Misc.TaxGroups.Infrastructure;
using Nop.Web.Framework.Mvc.Routes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace Nop.Plugin.Misc.TaxGroups
{
    public class RouteConfig : IRouteProvider
    {
        public int Priority
        {
            get { return 0; }
        }

        public void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute("Plugin.Misc.TaxGroups.GroupRulesController",
                "GroupRules/{action}/{id}",
                new { controller = "GroupRules", action = "Index", id = UrlParameter.Optional },
                new[] { "Nop.Plugin.Misc.TaxGroups.Controllers" }
            );

            routes.MapRoute("Plugin.Misc.TaxGroups.MemberGroupsController",
                "MemberGroups/{action}/{id}",
                new { controller = "MemberGroups", action = "Index", id = UrlParameter.Optional },
                new[] { "Nop.Plugin.Misc.TaxGroups.Controllers" }
            );

            routes.MapRoute("Plugin.Misc.TaxGroups.TaxClassesController",
                "TaxClasses/{action}/{id}",
                new { controller = "TaxClasses", action = "Index", id = UrlParameter.Optional },
                new[] { "Nop.Plugin.Misc.TaxGroups.Controllers" }
            );
            
            ViewEngines.Engines.Insert(0, new CustomViewEngine());
        }
    }
}
