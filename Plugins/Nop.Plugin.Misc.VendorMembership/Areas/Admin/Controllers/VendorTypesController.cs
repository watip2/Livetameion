using Nop.Web.Framework.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nop.Plugin.Misc.VendorMembership.Areas.Admin.Controllers
{
    public class VendorTypesController : BasePluginController
    {
        public VendorTypesController()
        {

        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
    }
}
