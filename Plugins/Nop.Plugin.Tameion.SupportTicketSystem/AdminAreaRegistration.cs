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
                "Admin_GroupDeals_default",
                "Admin/GroupDeals/{action}/{id}",
                new { controller = "GroupDeals", action = "Index", area = "Admin", id = "" },
                new[] { "Nop.Plugin.Misc.GroupDeals.Areas.Admin.Controllers" }
            );
        }
    }
}
