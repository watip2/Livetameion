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
                "Vendor_Tickets_default",
                "Vendor/Tickets/{action}/{id}",
                new { controller = "Tickets", action = "Index", area = "Vendor", id = "" },
                new[] { "Nop.Plugin.Tameion.SupportTicketSystem.Areas.Vendor.Controllers" }
            );
        }
    }
}
