using Nop.Plugin.Misc.VendorMembership.Infrastructure;
using Nop.Web.Framework.Mvc.Routes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace Nop.Plugin.Misc.VendorMembership
{
    public class RouteConfig : IRouteProvider
    {
        public int Priority
        {
            get { return 0; }
        }

        public void RegisterRoutes(RouteCollection routes)
        {
            //var route = routes.MapRoute("Plugin.Misc.VendorMembership.VendorMembershipController",
            //    "VendorMembership/{action}/{id}",
            //    new { controller = "VendorMembership", action = "Dashboard", id = UrlParameter.Optional },
            //    new[] { "Nop.Plugin.Misc.VendorMembership.Controllers" }
            //);
            //routes.Remove(route);
            //routes.Insert(0, route);

            var route = routes.MapRoute("Plugin.Misc.VendorMembership.Vendor.ProductsController",
                "Vendor/Products/{action}/{id}",
                new { area = "Vendor", controller = "Products", action = "ListProducts", id = UrlParameter.Optional },
                new[] { "Nop.Plugin.Misc.VendorMembership.Controllers" }
            );
            routes.Remove(route);
            routes.Insert(0, route);

            route = routes.MapRoute("Plugin.Misc.VendorMembership.Vendor.OrdersController",
                "Vendor/Orders/{action}/{id}",
                new { area = "Vendor", controller = "Orders", action = "Index", id = UrlParameter.Optional },
                new[] { "Nop.Plugin.Misc.VendorMembership.Controllers" }
            );
            routes.Remove(route);
            routes.Insert(0, route);

            route = routes.MapRoute("Plugin.Misc.VendorMembership.Vendor.AccountController",
                "Vendor/Account/{action}",
                new { area = "Vendor", controller = "Account", action = "Index" },
                new[] { "Nop.Plugin.Misc.VendorMembership.Controllers" }
            );
            routes.Remove(route);
            routes.Insert(0, route);

            route = routes.MapRoute("Plugin.Misc.VendorMembership.Vendor.Dashboard",
                "Vendor",
                new { area = "Vendor", controller = "Orders", action = "Index" },
                new[] { "Nop.Plugin.Misc.VendorMembership.Controllers" }
            );
            routes.Remove(route);
            routes.Insert(0, route);

            route = routes.MapRoute("Plugin.Misc.VendorMembership.Vendor.SettingsController",
                "Vendor/Settings",
                new { area = "Vendor", controller = "Settings", action = "Index" },
                new[] { "Nop.Plugin.Misc.VendorMembership.Controllers" }
            );
            routes.Remove(route);
            routes.Insert(0, route);

            route = routes.MapRoute("Plugin.Misc.VendorMembership.Vendor.InvoicesController",
                "Vendor/Invoices",
                new { area = "Vendor", controller = "Invoices", action = "Index" },
                new[] { "Nop.Plugin.Misc.VendorMembership.Controllers" }
            );
            routes.Remove(route);
            routes.Insert(0, route);

            ViewEngines.Engines.Insert(0, new CustomViewEngine());
        }
    }
}
