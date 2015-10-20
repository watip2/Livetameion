using System.Web.Mvc;
using Nop.Web.Framework.Security;
using Nop.Core.Infrastructure;
using Nop.Services.Catalog;
using Nop.Core.Domain.Catalog;
using System;
using Nop.Core.Data;
using Nop.Plugin.Misc.TrialTracker.Domain;
using Nop.Core.Domain.Vendors;

namespace Nop.Web.Controllers
{
    public partial class HomeController : BasePublicController
    {
        private IRepository<TrialTrackerRecord> _trialRepo;
        private IRepository<Product> _productRepo;
        private IRepository<Vendor> _vendorRepo;

        public HomeController(IRepository<TrialTrackerRecord> trialRepo, IRepository<Product> productRepo, IRepository<Vendor> vendorRepo)
        {
            _trialRepo = trialRepo;
            _productRepo = productRepo;
            _vendorRepo = vendorRepo;
        }

        [NopHttpsRequirement(SslRequirement.No)]
        public ActionResult Index()
        {
            //var productService = new NopEngine().Resolve<IProductService>();
            //var p = new Product();
            //p.Name = "Test Product";
            //p.UpdatedOnUtc = DateTime.UtcNow;
            //p.CreatedOnUtc = DateTime.UtcNow;
            //productService.InsertProduct(p);
            
            var trial = _trialRepo.GetById(1);
            var pro = _productRepo.GetById(trial.ProductId);
            _vendorRepo.Insert(new Vendor { 
                 Active = true,
                 Name = "Sohail Khan",
                 Email = "sohail@baba.com",
                 Description = "vendor description...",
                 MetaDescription = "meta description...",
                 AdminComment = "admin comment"
            });

            return View();
        }
    }
}
