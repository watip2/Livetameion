using System.Web.Mvc;

namespace Nop.Plugin.Tameion.Auctions
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
            //context.MapRoute(
            //    "Public_Auctions_default",
            //    "{controller}/{action}/{id}",
            //    new { controller = "Auctions", action = "Index", area = "Public", id = "" },
            //    new[] { "Nop.Plugin.Tameion.Auctions.Areas.Public.Controllers" }
            //);
        }
    }
}
