using Nop.Web.Framework.Themes;

namespace Nop.Plugin.Tameion.BridgePay.Infrastructure
{
    public class CustomViewEngine : ThemeableRazorViewEngine
    {
        // this constructor is executed when the application starts
        public CustomViewEngine()
        {
            ViewLocationFormats = new[] {
                "~/Plugins/Tameion.BridgePay/Views/{0}.cshtml",
                "~/Plugins/Tameion.BridgePay/Views/{1}/{0}.cshtml"
            };

            PartialViewLocationFormats = new[] {
                "~/Plugins/Tameion.BridgePay/Views/{0}.cshtml",
                "~/Plugins/Tameion.BridgePay/Views/{1}/{0}.cshtml",
                "~/Plugins/Tameion.BridgePay/Views/Shared/{0}.cshtml"
            };
        }
    }
}
