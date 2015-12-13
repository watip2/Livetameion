using System.Web.Mvc;

namespace Nop.Plugin.Tameion.SupportTicketSystem
{
    public class VendorAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Vendor";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Vendor_SupportTickets_default",
                "Vendor/SupportTickets/{action}/{id}",
                new { controller = "SupportTicketTopics", action = "Index", area = "Vendor", id = "" },
                new[] { "Nop.Plugin.Tameion.SupportTicketSystem.Areas.Vendor.Controllers" }
            );
        }
    }
}
