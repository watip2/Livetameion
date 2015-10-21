using Nop.Core.Data;
using Nop.Plugin.Misc.VendorMembership.Domain;
using Nop.Web.Framework.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nop.Plugin.Misc.VendorMembership
{
    public class VendorMembershipController : BasePluginController
    {
        private IRepository<Vendorrr> _vendorrrRepo;
        
        public VendorMembershipController(IRepository<Vendorrr> vendorrrRepo)
        {
            _vendorrrRepo = vendorrrRepo;
        }

        public ActionResult Register()
        {
            return View();
        }
    }
}
