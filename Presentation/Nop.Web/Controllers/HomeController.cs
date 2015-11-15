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
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Data;
using Nop.Plugin.Misc.GroupDeals.Models;
using Nop.Plugin.Misc.GroupDeals.Services;
using Nop.Plugin.Misc.GroupDeals.DataAccess;

namespace Nop.Web.Controllers
{
    public partial class HomeController : BasePublicController
    {
        private IRepository<VendorPayoutMethod> _vendorPayoutMethodRepo;
        private IRepository<Vendor> _vendorRepo;
        private IRepository<PayoutMethod> _payoutMethodRepo;
        private IRepository<Category> _categoryRepo;
        private IRepository<VendorBusinessType> _vendorBusinessTypeRepo;
        private IRepository<GroupDeal> _groupDealRepo;
        private IGroupDealService _groupdealService;

        public HomeController(
            IRepository<VendorPayoutMethod> vendorPayoutMethodRepo,
            IRepository<Vendor> vendorRepo,
            IRepository<Category> categoryRepo,
            IRepository<PayoutMethod> payoutMethodRepo,
            IRepository<VendorBusinessType> vendorBusinessTypeRepo,
            IRepository<GroupDeal> groupDealRepo,
            IGroupDealService groupdealService)
        {
            _vendorPayoutMethodRepo = vendorPayoutMethodRepo;
            _vendorRepo = vendorRepo;
            _payoutMethodRepo = payoutMethodRepo;
            _categoryRepo = categoryRepo;
            _vendorBusinessTypeRepo = vendorBusinessTypeRepo;
            _groupDealRepo = groupDealRepo;
            _groupdealService = groupdealService;
        }

        [NopHttpsRequirement(SslRequirement.No)]
        public ActionResult Index()
        {
            //var v = _vendorPayoutMethodRepo.GetById(1);
            //var t = _testRepo.GetById(1);

            var pm = _payoutMethodRepo.GetById(1);
            var bt = _vendorBusinessTypeRepo.GetById(1);
            var v = _vendorRepo.GetById(bt.VendorId);
            var c = _categoryRepo.GetById(bt.BusinessTypeId);
            
            var vendorService = EngineContext.Current.Resolve<IVendorService>();
            var v2 = vendorService.GetVendorById(1027);
            
            var genericAttributeService = EngineContext.Current.Resolve<IGenericAttributeService>();
            genericAttributeService.SaveAttribute(v2, "RegDate", DateTime.Now);
            var regdate = v2.GetAttribute<DateTime>("RegDate", genericAttributeService);

            var vendor = vendorService.GetVendorById(1027);
            var gd = new GroupDeal
            {
                AttributeSetId = 0,
                CreatedOnUtc = DateTime.Now,
                UpdatedOnUtc = DateTime.Now,
                Name = "Group Deal Name",
                VendorId = vendor.Id,
                AllowBackInStockSubscriptions = false,
                AvailableStartDateTimeUtc = DateTime.Now,
                AvailableEndDateTimeUtc = DateTime.Parse("01/01/2016"),
                Country = "Pakistan",
                StateOrProvince = "KPK",
                City = "Kamra",
                Active = true,
                ShortDescription = "this is short description",
                FullDescription = "this is full description"
            };
            _groupdealService.InsertGroupDeal(gd);
            var deals = _groupdealService.GetAllGroupDealsByVendorId(1027);

            //vv.PayoutMethod
            return View();
        }
    }
}
