using System.Web.Mvc;

namespace Nop.Plugin.Tameion.SupportTicketSystem
{
    public class AdminAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Admin";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Admin_SupportTickets_default",
                "Admin/SupportTickets/{action}/{id}",
                new { controller = "SupportTicketTopics", action = "Index", area = "Admin", id = "" },
                new[] { "Nop.Plugin.Tameion.SupportTicketSystem.Areas.Admin.Controllers" }
            );
        }
    }
}
