﻿using Nop.Web.Framework.Themes;
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
                "~/Plugins/Misc.GroupDeals/Views/{0}.cshtml",
                "~/Plugins/Misc.GroupDeals/Views/{1}/{0}.cshtml"
            };
            
            PartialViewLocationFormats = new[] {
                "~/Plugins/Misc.GroupDeals/Views/{0}.cshtml",
                "~/Plugins/Misc.GroupDeals/Views/{1}/{0}.cshtml"
            };
        }
    }
}
