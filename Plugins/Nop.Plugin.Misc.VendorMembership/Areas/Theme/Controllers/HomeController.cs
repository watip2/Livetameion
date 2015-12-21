using Nop.Web.Framework.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nop.Plugin.Misc.VendorMembership.Areas.Theme.Controllers
{
    public class HomeController : BasePluginController
    {
        public string Index()
        {
            return "home controller";
        }
    }
}
