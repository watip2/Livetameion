using System.Web.Mvc;

namespace Nop.Plugin.Tameion.Auctions
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
                "Vendor_Auctions_default",
                "Vendor/{controller}/{action}/{id}",
                new { controller = "Auctions", action = "Index", area = "Vendor", id = "" },
                new[] { "Nop.Plugin.Tameion.Auctions.Areas.Vendor.Controllers" }
            );
        }
    }
}
