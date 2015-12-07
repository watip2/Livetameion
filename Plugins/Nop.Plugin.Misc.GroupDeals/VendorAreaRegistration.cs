using System.Web.Mvc;

namespace Nop.Plugin.Misc.GroupDeals
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
                "Vendor_GroupDeals_default",
                "Vendor/GroupDeals/{action}/{id}",
                new { controller = "GroupDeals", action = "Index", area = "Vendor", id = "" },
                new[] { "Nop.Plugin.Misc.GroupDeals.Areas.Vendor.Controllers" }
            );
        }
    }
}
