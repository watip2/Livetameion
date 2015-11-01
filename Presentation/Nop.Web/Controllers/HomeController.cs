using System.Web.Mvc;
using Nop.Web.Framework.Security;
using Nop.Core.Infrastructure;
using Nop.Services.Catalog;
using Nop.Core.Domain.Catalog;
using System;
using Nop.Core.Data;
using Nop.Plugin.Misc.TrialTracker.Domain;
using Nop.Core.Domain.Vendors;
using Nop.Core.Domain.ExtendedModels;
using Nop.Services.Vendors;
using Nop.Plugin.Misc.VendorMembership.Domain;

namespace Nop.Web.Controllers
{
    public partial class HomeController : BasePublicController
    {
        private IRepository<VendorPayoutMethod> _vendorPayoutMethodRepo;
        private IRepository<Test> _testRepo;

        public HomeController(
            IRepository<VendorPayoutMethod> vendorPayoutMethodRepo,
            IRepository<Test> testRepo
        )
        {
            _vendorPayoutMethodRepo = vendorPayoutMethodRepo;
            _testRepo = testRepo;
        }

        [NopHttpsRequirement(SslRequirement.No)]
        public ActionResult Index() 
        {
            _testRepo.Insert(new Test { Name = "Sohail" });

            var v = _vendorPayoutMethodRepo.GetById(1);
            var t = _testRepo.GetById(1);

            //vv.PayoutMethod
            return View();
        }
    }
}
