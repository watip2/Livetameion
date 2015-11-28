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
            routes.MapRoute("Plugin.Tameion.SupportTicketSystem.SupportTicketTopicsController",
                "SupportTickets/{action}/{id}",
                new { controller = "SupportTicketTopics", action = "Index", id = UrlParameter.Optional },
                new[] { "Nop.Plugin.Tameion.SupportTicketSystem.Controllers" }
            );

            routes.MapRoute("Plugin.Tameion.SupportTicketSystem.SupportTicketResponsesController",
                "SupportTicketResponses/{action}/{id}",
                new { controller = "SupportTicketResponses", action = "Index", id = UrlParameter.Optional },
                new[] { "Nop.Plugin.Tameion.SupportTicketSystem.Controllers" }
            );

            ViewEngines.Engines.Insert(0, new CustomViewEngine());
        }
    }
}
