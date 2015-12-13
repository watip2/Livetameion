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
                "~/Plugins/Tameion.SupportTicketTopics/Views/{0}.cshtml",
                "~/Plugins/Tameion.SupportTicketTopics/Views/{1}/{0}.cshtml",
                "~/Plugins/Tameion.SupportTicketTopics/Areas/Admin/Views/{1}/{0}.cshtml",
                "~/Plugins/Tameion.SupportTicketTopics/Areas/Vendor/Views/{1}/{0}.cshtml",
                "~/Plugins/Tameion.SupportTicketResponses/Views/{0}.cshtml",
                "~/Plugins/Tameion.SupportTicketResponses/Views/{1}/{0}.cshtml"
            };
            
            PartialViewLocationFormats = new[] {
                "~/Plugins/Tameion.SupportTicketTopics/Views/{0}.cshtml",
                "~/Plugins/Tameion.SupportTicketTopics/Views/{1}/{0}.cshtml",
                "~/Plugins/Tameion.SupportTicketTopics/Areas/Admin/Views/{1}/{0}.cshtml",
                "~/Plugins/Tameion.SupportTicketTopics/Areas/Vendor/Views/{1}/{0}.cshtml",
                "~/Plugins/Tameion.SupportTicketResponses/Views/{0}.cshtml",
                "~/Plugins/Tameion.SupportTicketResponses/Views/{1}/{0}.cshtml"
            };
        }
    }
}
