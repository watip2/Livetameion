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
            var route = routes.MapRoute("Plugin.Tameion.SupportTicketSystem.Areas.Admin.SupportTicketTopicsController",
                "Admin/SupportTickets/{action}/{id}",
                new { area = "Admin", controller = "SupportTicketTopics", action = "Index", id = UrlParameter.Optional },
                new[] { "Nop.Plugin.Tameion.SupportTicketSystem.Areas.Admin.Controllers" }
            );
            routes.Remove(route);
            routes.Insert(0, route);

            route = routes.MapRoute("Plugin.Tameion.SupportTicketSystem.Areas.Vendor.SupportTicketTopicsController",
                "Vendor/SupportTickets/{action}/{id}",
                new { area = "Vendor", controller = "SupportTicketTopics", action = "Index", id = UrlParameter.Optional },
                new[] { "Nop.Plugin.Tameion.SupportTicketSystem.Areas.Vendor.Controllers" }
            );
            routes.Remove(route);
            routes.Insert(0, route);

            route = routes.MapRoute("Plugin.Tameion.SupportTicketSystem.Areas.Admin.SupportTicketResponsesController",
                "Admin/SupportTicketResponses/{action}/{id}",
                new { area = "Admin", controller = "SupportTicketResponses", action = "Index", id = UrlParameter.Optional },
                new[] { "Nop.Plugin.Tameion.SupportTicketSystem.Areas.Admin.Controllers" }
            );
            routes.Remove(route);
            routes.Insert(0, route);

            route = routes.MapRoute("Plugin.Tameion.SupportTicketSystem.Areas.Vendor.SupportTicketResponsesController",
                "Vendor/SupportTicketResponses/{action}/{id}",
                new { area = "Vendor", controller = "SupportTicketResponses", action = "Index", id = UrlParameter.Optional },
                new[] { "Nop.Plugin.Tameion.SupportTicketSystem.Areas.Vendor.Controllers" }
            );
            routes.Remove(route);
            routes.Insert(0, route);
            
            ViewEngines.Engines.Insert(0, new CustomViewEngine());
        }
    }
}
