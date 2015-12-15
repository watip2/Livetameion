using Nop.Plugin.Tameion.SupportTicketSystem.Infrastructure;
using Nop.Web.Framework.Mvc.Routes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;

namespace Nop.Plugin.Tameion.SupportTicketSystem
{
    public class RouteConfig : IRouteProvider
    {
        public int Priority
        {
            get { return 0; }
        }

        public void RegisterRoutes(RouteCollection routes)
        {
            var route = routes.MapRoute("Plugin.Tameion.SupportTicketSystem.Areas.Admin.TicketsController",
                "Admin/Tickets/{action}/{id}",
                new { area = "Admin", controller = "Tickets", action = "Index", id = UrlParameter.Optional },
                new[] { "Nop.Plugin.Tameion.SupportTicketSystem.Areas.Admin.Controllers" }
            );
            routes.Remove(route);
            routes.Insert(0, route);

            route = routes.MapRoute("Plugin.Tameion.SupportTicketSystem.Areas.Vendor.TicketsController",
                "Vendor/Tickets/{action}/{id}",
                new { area = "Vendor", controller = "Tickets", action = "Index", id = UrlParameter.Optional },
                new[] { "Nop.Plugin.Tameion.SupportTicketSystem.Areas.Vendor.Controllers" }
            );
            routes.Remove(route);
            routes.Insert(0, route);

            route = routes.MapRoute("Plugin.Tameion.SupportTicketSystem.Areas.Admin.RepliesController",
                "Admin/Replies/{action}/{id}",
                new { area = "Admin", controller = "Replies", action = "Index", id = UrlParameter.Optional },
                new[] { "Nop.Plugin.Tameion.SupportTicketSystem.Areas.Admin.Controllers" }
            );
            routes.Remove(route);
            routes.Insert(0, route);

            route = routes.MapRoute("Plugin.Tameion.SupportTicketSystem.Areas.Vendor.RepliesController",
                "Vendor/Replies/{action}/{id}",
                new { area = "Vendor", controller = "Replies", action = "Index", id = UrlParameter.Optional },
                new[] { "Nop.Plugin.Tameion.SupportTicketSystem.Areas.Vendor.Controllers" }
            );
            routes.Remove(route);
            routes.Insert(0, route);
            
            ViewEngines.Engines.Insert(0, new CustomViewEngine());
        }
    }
}
