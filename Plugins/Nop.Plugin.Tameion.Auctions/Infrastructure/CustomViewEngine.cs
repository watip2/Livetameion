using Nop.Web.Framework.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Tameion.Auctions.Infrastructure
{
    public class CustomViewEngine : ThemeableRazorViewEngine
    {
        // this constructor is executed when the application starts
        public CustomViewEngine()
        {
            ViewLocationFormats = new[] {
                "~/Plugins/Tameion.Auctions/Views/{0}.cshtml",
                "~/Plugins/Tameion.Auctions/Areas/Admin/Views/{1}/{0}.cshtml",
                "~/Plugins/Tameion.Auctions/Areas/Vendor/Views/{1}/{0}.cshtml",
                "~/Plugins/Tameion.Auctions/Areas/Public/Views/{1}/{0}.cshtml"
            };
            
            PartialViewLocationFormats = new[] {
                "~/Plugins/Misc.Groupdeals/Views/{0}.cshtml",
                "~/Plugins/Tameion.Auctions/Areas/Admin/Views/{1}/{0}.cshtml",
                "~/Plugins/Tameion.Auctions/Areas/Vendor/Views/{1}/{0}.cshtml",
                "~/Plugins/Tameion.Auctions/Areas/Public/Views/{1}/{0}.cshtml"
            };
        }
    }
}
