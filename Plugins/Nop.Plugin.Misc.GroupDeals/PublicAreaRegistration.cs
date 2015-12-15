using System.Web.Mvc;

namespace Nop.Plugin.Misc.GroupDeals
{
    public class PublicAreaRegistration : AreaRegistration
    {
        public override string AreaName
        {
            get
            {
                return "Public";
            }
        }

        public override void RegisterArea(AreaRegistrationContext context)
        {
            context.MapRoute(
                "Public_GroupDeals_default",
                "GroupDeals/{action}/{id}",
                new { controller = "GroupDeals", action = "Index", area = "Public", id = "" },
                new[] { "Nop.Plugin.Misc.GroupDeals.Areas.Public.Controllers" }
            );
        }
    }
}
