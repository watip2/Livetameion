using System.Web.Mvc;

namespace Nop.Plugin.Misc.VendorMembership
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
                "Admin_vendormembership_default",
                "Admin/{controller}/{action}/{id}",
                new { controller = "Multitenancy", action = "Settings", area = "Admin", id = "" },
                new[] { "Nop.Plugin.Misc.VendorMembership.Areas.Admin.Controllers" }
            );
        }
    }
}
