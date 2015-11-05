using System.Web.Mvc;
using Nop.Web.Framework.Security;
using Nop.Core.Infrastructure;
using Nop.Services.Catalog;
using Nop.Core.Domain.Catalog;
using System;
using Nop.Core.Data;
using Nop.Core.Domain.Vendors;
using Nop.Services.Vendors;
using Nop.Plugin.Misc.VendorMembership.Domain;

namespace Nop.Web.Controllers
{
    public partial class HomeController : BasePublicController
    {
        private IRepository<VendorPayoutMethod> _vendorPayoutMethodRepo;
        private IRepository<Vendor> _vendorRepo;
        private IRepository<PayoutMethod> _payoutMethodRepo;
        private IRepository<Category> _categoryRepo;
        private IRepository<VendorBusinessType> _vendorBusinessTypeRepo;

        public HomeController(
            IRepository<VendorPayoutMethod> vendorPayoutMethodRepo,
            IRepository<Vendor> vendorRepo,
            IRepository<Category> categoryRepo,
            IRepository<PayoutMethod> payoutMethodRepo,
            IRepository<VendorBusinessType> vendorBusinessTypeRepo
        )
        {
            _vendorPayoutMethodRepo = vendorPayoutMethodRepo;
            _vendorRepo = vendorRepo;
            _payoutMethodRepo = payoutMethodRepo;
            _categoryRepo = categoryRepo;
            _vendorBusinessTypeRepo = vendorBusinessTypeRepo;
        }

        [NopHttpsRequirement(SslRequirement.No)]
        public ActionResult Index()
        {
            //_testRepo.Insert(new Test { Name = "Sohail" });

            //var v = _vendorPayoutMethodRepo.GetById(1);
            //var t = _testRepo.GetById(1);

            var pm = _payoutMethodRepo.GetById(1);
            var bt = _vendorBusinessTypeRepo.GetById(1);
            var v = _vendorRepo.GetById(bt.VendorId);
            var c = _categoryRepo.GetById(bt.BusinessTypeId);

            var vendorService = EngineContext.Current.Resolve<IVendorService>();
            var v2 = vendorService.GetVendorById(1027);
            
            //vv.PayoutMethod
            return View();
        }
    }
}
