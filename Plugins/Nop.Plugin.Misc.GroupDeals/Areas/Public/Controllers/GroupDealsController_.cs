using Nop.Core.Data;
using Nop.Core.Domain.Catalog;
using Nop.Plugin.Misc.GroupDeals.Models;
using Nop.Plugin.Misc.GroupDeals.Services;
using Nop.Services.Catalog;
using Nop.Services.Common;
using Nop.Web.Framework.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Nop.Services.Common;

namespace Nop.Plugin.Misc.GroupDeals.Areas.Public.Controllers
{
    public class GroupDealsController_ : BasePluginController
    {
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly IGroupDealService _groupDealProductService;
        private readonly IGroupDealService _groupDealService;
        private readonly IRepository<Product> _productRepo;
        private readonly IProductService _productService;

        public GroupDealsController_(IGroupDealService groupDealService,
            IRepository<Product> productRepo,
            IGroupDealService groupDealProductService,
            IGenericAttributeService genericAttributeService,
            IProductService productService)
        {
            _groupDealService = groupDealService;
            _productRepo = productRepo;
            _groupDealProductService = groupDealProductService;
            _genericAttributeService = genericAttributeService;
            _productService = productService;
        }

        [AcceptVerbs("GET")]
        public ActionResult Index()
        {
            var groupDeals = _groupDealService.GetAllGroupDealProducts();
            return View(groupDeals);
        }

        [AcceptVerbs("GET")]
        public ActionResult Details(int Id)
        {
            var groupDeal = _groupDealService.GetGroupDealProductById(Id);
            //if (groupDeal == null || groupDeal.Deleted)
            //return InvokeHttp404();
            int[] ids = { 1,2,3,4,5,6,7,8,9,10,11,12,13,14,15 };
            var products = _productRepo.Table.Where(p => ids.Contains(p.Id) && p.Name.Contains("Apple"));

            var product = _productService.GetProductById(1);
            var v = product.GetAttribute<string>("Effortee", _genericAttributeService);

            var pro = new Product
            {
                Name = "group deal 111",
                StockQuantity = 10,
                OrderMaximumQuantity = 10,
                OrderMinimumQuantity = 1,
                Published = true,
                ProductType = ProductType.SimpleProduct,

                // datetime fields
                CreatedOnUtc = DateTime.UtcNow,
                UpdatedOnUtc = DateTime.UtcNow,
                AvailableEndDateTimeUtc = DateTime.Parse("01/01/2016"),
                AvailableStartDateTimeUtc = DateTime.UtcNow,
                PreOrderAvailabilityStartDateTimeUtc = DateTime.Now,
                SpecialPriceStartDateTimeUtc = DateTime.Now,
                SpecialPriceEndDateTimeUtc = DateTime.Parse("01/01/2016")
            };
            //_groupDealProductService.InsertGroupDealProduct(pro);

            var pro1 = _groupDealProductService.GetGroupDealProductById(47);
            var sename = pro1.GetAttribute<string>("SeName", _genericAttributeService);

            return View("GroupDealTemplate.Simple", groupDeal);
        }
    }
}
