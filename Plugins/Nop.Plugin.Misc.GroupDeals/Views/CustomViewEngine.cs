using Nop.Web.Framework.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.GroupDeals.Views
{
    public class CustomViewEngine : ThemeableRazorViewEngine
    {
        // this constructor is executed when the application starts
        public CustomViewEngine()
        {
            ViewLocationFormats = new[] {
                "~/Plugins/Misc.Groupdeals/Views/{0}.cshtml",
                "~/Plugins/Misc.Groupdeals/Areas/Admin/Views/{1}/{0}.cshtml",
                "~/Plugins/Misc.Groupdeals/Areas/Vendor/Views/{1}/{0}.cshtml",
                "~/Plugins/Misc.Groupdeals/Areas/Public/Views/{1}/{0}.cshtml"
            };
            
            PartialViewLocationFormats = new[] {
                "~/Plugins/Misc.Groupdeals/Views/{0}.cshtml",
                "~/Plugins/Misc.Groupdeals/Areas/Admin/Views/{1}/{0}.cshtml",
                "~/Plugins/Misc.Groupdeals/Areas/Vendor/Views/{1}/{0}.cshtml",
                "~/Plugins/Misc.Groupdeals/Areas/Public/Views/{1}/{0}.cshtml"
            };
        }
    }
}
