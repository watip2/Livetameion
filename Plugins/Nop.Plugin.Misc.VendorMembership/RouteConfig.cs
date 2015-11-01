using Nop.Plugin.Misc.VendorMembership.Infrastructure;
using Nop.Web.Framework.Mvc.Routes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace Nop.Plugin.Misc.TrialTracker
{
    public class RouteConfig : IRouteProvider
    {
        public int Priority
        {
            get { return 0; }
        }

        public void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute("Plugin.Misc.VendorMembership.RegisterVendor",
                "VendorMembership/Register",
                new { controller = "VendorMembership", action = "Register" },
                new[] { "Nop.Plugin.Misc.VendorMembership.Controllers" }
            );

            routes.MapRoute("Plugin.Misc.VendorMembership.Dashboard",
                "VendorMembership/Dashboard",
                new { controller = "VendorMembership", action = "Dashboard" },
                new[] { "Nop.Plugin.Misc.VendorMembership.Controllers" }
            );

            //routes.MapRoute("Plugin.Misc.TrialTracker.UpdateTrial",
            //    "TrialTracker/UpdateTrial",
            //    new { controller = "TrialTracker", action = "UpdateTrial" },
            //    new[] { "Nop.Plugin.Misc.TrialTracker.Controllers" }
            //);

            //routes.MapRoute("Plugin.Misc.TrialTracker.DeleteTrial",
            //    "TrialTracker/DeleteTrial",
            //    new { controller = "TrialTracker", action = "DeleteTrial" },
            //    new[] { "Nop.Plugin.Misc.TrialTracker.Controllers" }
            //);

            ViewEngines.Engines.Insert(0, new CustomViewEngine());
        }
    }
}
