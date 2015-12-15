using Nop.Web.Framework.Themes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Tameion.SupportTicketSystem.Infrastructure
{
    public class CustomViewEngine : ThemeableRazorViewEngine
    {
        // this constructor is executed when the application starts
        public CustomViewEngine()
        {
            ViewLocationFormats = new[] {
                "~/Plugins/Tameion.SupportTicketSystem/Views/{0}.cshtml",
                "~/Plugins/Tameion.SupportTicketSystem/Views/{1}/{0}.cshtml",
                "~/Plugins/Tameion.SupportTicketSystem/Areas/Admin/Views/{1}/{0}.cshtml",
                "~/Plugins/Tameion.SupportTicketSystem/Areas/Vendor/Views/{1}/{0}.cshtml"
            };
            
            PartialViewLocationFormats = new[] {
                "~/Plugins/Tameion.SupportTicketSystem/Views/{0}.cshtml",
                "~/Plugins/Tameion.SupportTicketSystem/Views/{1}/{0}.cshtml",
                "~/Plugins/Tameion.SupportTicketSystem/Areas/Admin/Views/{1}/{0}.cshtml",
                "~/Plugins/Tameion.SupportTicketSystem/Areas/Vendor/Views/{1}/{0}.cshtml"
            };
        }
    }
}
