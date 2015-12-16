using System.Web.Mvc;

namespace Nop.Plugin.Tameion.SelectAndSell
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
                "Vendor_SelectAndSell_default",
                "Vendor/{controller}/{action}/{id}",
                new { controller = "SelectSell", action = "Index", area = "Vendor", id = "" },
                new[] { "Nop.Plugin.Tameion.SelectAndSell.Controllers" }
            );
        }
    }
}
