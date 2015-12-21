using System.Web.Mvc;

namespace Nop.Plugin.Misc.VendorMembership
{
    public class ThemeAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Theme";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Theme_default",
                "Theme/{controller}/{action}/{id}",
                new { controller = "Home", action = "Index", area = "Theme", id = "" },
                new[] { "Nop.Plugin.Misc.VendorMembership.Areas.Theme.Controllers" }
            );
        }
    }
}
