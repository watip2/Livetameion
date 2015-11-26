using System.Web.Mvc;

namespace Nop.Admin
{
    public class AdminAreaRegistration : AreaRegistration
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
                "Vendor_default",
                "Vendor/{controller}/{action}/{id}",
                new { controller = "Orders", action = "Index", area = "Vendor", id = "" },
                new[] { "Nop.Plugin.Misc.VendorMembership.Controllers" }
            );
        }
    }
}
