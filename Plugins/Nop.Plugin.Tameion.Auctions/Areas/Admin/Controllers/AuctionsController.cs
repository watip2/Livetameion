using Nop.Admin.Models.Catalog;
using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Directory;
using Nop.Plugin.Tameion.Auctions.DomainModels;
using Nop.Plugin.Tameion.Auctions.Helpers;
using Nop.Plugin.Tameion.Auctions.Infrastructure;
using Nop.Plugin.Tameion.Auctions.Models;
using Nop.Plugin.Tameion.Auctions.Services;
using Nop.Plugin.Tameion.Auctions.ViewModels;
using Nop.Services.Catalog;
using Nop.Services.Directory;
using Nop.Services.Helpers;
using Nop.Services.Localization;
using Nop.Services.Stores;
using Nop.Services.Vendors;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Kendoui;
using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Nop.Plugin.Tameion.Auctions.Areas.Admin.Controllers
{
    [AdminAuthorize]
    public class AuctionsController : BasePluginController
    {
        private readonly IAuctionService _auctionService;
        private readonly ILocalizationService _localizationService;
        private readonly IVendorService _vendorService;
        private readonly ICurrencyService _currencyService;
        private readonly CurrencySettings _currencySettings;
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly IWorkContext _workContext;
        private readonly ICategoryService _categoryService;
        private readonly IManufacturerService _manufacturerService;
        private readonly IStoreService _storeService;
        private readonly IProductService _productService;

        public AuctionsController(IAuctionService auctionService,
            ILocalizationService localizationService,
            IVendorService vendorService,
            ICurrencyService currencyService,
            CurrencySettings currencySettings,
            IDateTimeHelper dateTimeHelper,
            IWorkContext workContext,
            ICategoryService categoryService,
            IManufacturerService manufacturerService,
            IStoreService storeService,
            IProductService productService)
        {
            _auctionService = auctionService;
            _localizationService = localizationService;
            _vendorService = vendorService;
            _currencyService = currencyService;
            _currencySettings = currencySettings;
            _dateTimeHelper = dateTimeHelper;
            _workContext = workContext;
            _categoryService = categoryService;
            _manufacturerService = manufacturerService;
            _storeService = storeService;
            _productService = productService;
        }

        [AcceptVerbs("GET")]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AuctionList(DataSourceRequest command, AuctionListModel model)
        {
            var auctions = _auctionService.GetAllAuctions().ToList();
            var gridModel = new DataSourceResult();
            gridModel.Data = auctions;
            gridModel.Total = auctions.Count;
            return Json(gridModel);
        }

        [AcceptVerbs("GET")]
        public ActionResult Details(int Id)
        {
            //var tableName = PluginHelper.GetTableName<Auction>(new AuctionsContext());
            return View();
        }

        [AcceptVerbs("GET")]
        public ActionResult Create()
        {
            //if (!_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
            //    return AccessDeniedView();

            var model = new AuctionModel();
            PrepareAuctionModel(model, null, true, true);
            //AddLocales(_languageService, model.Locales);
            //PrepareAclModel(model, null, false);
            //PrepareStoresMappingModel(model, null, false);
            return View(model);
        }

        [AcceptVerbs("POST")]
        public ActionResult Create(AuctionModel model)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            if (ModelState.IsValid)
            {
                var auction = new ModelsMapper().CreateMap<AuctionModel, Auction>(model);
                auction.CreatedOnUtc = DateTime.UtcNow;
                auction.UpdatedOnUtc = DateTime.UtcNow;
                auction.StartingDate = DateTime.UtcNow;
                _auctionService.InsertAuction(auction);
                return RedirectToAction("Edit", new { Id = auction.Id });
            }

            PrepareAuctionModel(model, null, true, true);
            return View();
        }

        [AcceptVerbs("GET")]
        public ActionResult Edit(int Id)
        {
            var auction = _auctionService.GetAuctionById(Id);
            var model = new ModelsMapper().CreateMap<Auction, AuctionModel>(auction);

            PrepareAuctionModel(model, null, true, true);
            return View(model);
        }
        
        [AcceptVerbs("PUT")]
        public ActionResult Edit(AuctionModel model)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            if (ModelState.IsValid)
            {
                var auction = new ModelsMapper().CreateMap<AuctionModel, Auction>(model);
                auction.UpdatedOnUtc = DateTime.UtcNow;
                auction.StartingDate = DateTime.UtcNow;
                _auctionService.UpdateAuction(auction);
                return RedirectToAction("Index");
            }

            PrepareAuctionModel(model, null, true, true);
            return View();
        }

        [AcceptVerbs("DELETE")]
        public ActionResult Delete(AuctionModel model)
        {
            return View();
        }

        [NonAction]
        protected virtual void PrepareAuctionModel(AuctionModel model, Auction auction,
            bool setPredefinedValues, bool excludeProperties)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            //if (auction != null)
            //{
            //    var parentGroupedProduct = _productService.GetProductById(auction.ParentGroupedProductId);
            //    if (parentGroupedProduct != null)
            //    {
            //        model.AssociatedToProductId = auction.ParentGroupedProductId;
            //        model.AssociatedToProductName = parentGroupedProduct.Name;
            //    }
            //}

            model.PrimaryStoreCurrencyCode = _currencyService.GetCurrencyById(_currencySettings.PrimaryStoreCurrencyId).CurrencyCode;
            //model.BaseWeightIn = _measureService.GetMeasureWeightById(_measureSettings.BaseWeightId).Name;
            //model.BaseDimensionIn = _measureService.GetMeasureDimensionById(_measureSettings.BaseDimensionId).Name;
            if (auction != null)
            {
                model.CreatedOn = _dateTimeHelper.ConvertToUserTime(auction.CreatedOnUtc, DateTimeKind.Utc);
                model.UpdatedOn = _dateTimeHelper.ConvertToUserTime(auction.UpdatedOnUtc, DateTimeKind.Utc);
            }

            ////little performance hack here
            ////there's no need to load attributes, categories, manufacturers when creating a new product
            ////anyway they're not used (you need to save a product before you map add them)
            //if (auction != null)
            //{
            //    foreach (var productAttribute in _productAttributeService.GetAllProductAttributes())
            //    {
            //        model.AvailableProductAttributes.Add(new SelectListItem
            //        {
            //            Text = productAttribute.Name,
            //            Value = productAttribute.Id.ToString()
            //        });
            //    }
            //    foreach (var manufacturer in _manufacturerService.GetAllManufacturers(showHidden: true))
            //    {
            //        model.AvailableManufacturers.Add(new SelectListItem
            //        {
            //            Text = manufacturer.Name,
            //            Value = manufacturer.Id.ToString()
            //        });
            //    }
            //    var allCategories = _categoryService.GetAllCategories(showHidden: true);
            //    foreach (var category in allCategories)
            //    {
            //        model.AvailableCategories.Add(new SelectListItem
            //        {
            //            Text = category.GetFormattedBreadCrumb(allCategories),
            //            Value = category.Id.ToString()
            //        });
            //    }
            //}

            ////copy product
            //if (auction != null)
            //{
            //    model.CopyProductModel.Id = auction.Id;
            //    model.CopyProductModel.Name = "Copy of " + auction.Name;
            //    model.CopyProductModel.Published = true;
            //    model.CopyProductModel.CopyImages = true;
            //}

            ////templates
            //var templates = _productTemplateService.GetAllProductTemplates();
            //foreach (var template in templates)
            //{
            //    model.AvailableProductTemplates.Add(new SelectListItem
            //    {
            //        Text = template.Name,
            //        Value = template.Id.ToString()
            //    });
            //}

            //vendors
            //model.IsLoggedInAsVendor = _workContext.CurrentVendor != null;
            model.AvailableVendors.Add(new SelectListItem
            {
                Text = _localizationService.GetResource("Admin.Catalog.Products.Fields.Vendor.None"),
                Value = "0"
            });
            var vendors = _vendorService.GetAllVendors(showHidden: true);
            foreach (var vendor in vendors)
            {
                model.AvailableVendors.Add(new SelectListItem
                {
                    Text = vendor.Name,
                    Value = vendor.Id.ToString()
                });
            }

            ////delivery dates
            //model.AvailableDeliveryDates.Add(new SelectListItem
            //{
            //    Text = _localizationService.GetResource("Admin.Catalog.Products.Fields.DeliveryDate.None"),
            //    Value = "0"
            //});
            //var deliveryDates = _shippingService.GetAllDeliveryDates();
            //foreach (var deliveryDate in deliveryDates)
            //{
            //    model.AvailableDeliveryDates.Add(new SelectListItem
            //    {
            //        Text = deliveryDate.Name,
            //        Value = deliveryDate.Id.ToString()
            //    });
            //}

            ////warehouses
            //var warehouses = _shippingService.GetAllWarehouses();
            //model.AvailableWarehouses.Add(new SelectListItem
            //{
            //    Text = _localizationService.GetResource("Admin.Catalog.Products.Fields.Warehouse.None"),
            //    Value = "0"
            //});
            //foreach (var warehouse in warehouses)
            //{
            //    model.AvailableWarehouses.Add(new SelectListItem
            //    {
            //        Text = warehouse.Name,
            //        Value = warehouse.Id.ToString()
            //    });
            //}

            ////multiple warehouses
            //foreach (var warehouse in warehouses)
            //{
            //    var pwiModel = new ProductModel.ProductWarehouseInventoryModel
            //    {
            //        WarehouseId = warehouse.Id,
            //        WarehouseName = warehouse.Name
            //    };
            //    if (auction != null)
            //    {
            //        var pwi = auction.ProductWarehouseInventory.FirstOrDefault(x => x.WarehouseId == warehouse.Id);
            //        if (pwi != null)
            //        {
            //            pwiModel.WarehouseUsed = true;
            //            pwiModel.StockQuantity = pwi.StockQuantity;
            //            pwiModel.ReservedQuantity = pwi.ReservedQuantity;
            //            pwiModel.PlannedQuantity = _shipmentService.GetQuantityInShipments(auction, pwi.WarehouseId, true, true);
            //        }
            //    }
            //    model.ProductWarehouseInventoryModels.Add(pwiModel);
            //}

            ////product tags
            //if (auction != null)
            //{
            //    var result = new StringBuilder();
            //    for (int i = 0; i < auction.ProductTags.Count; i++)
            //    {
            //        var pt = auction.ProductTags.ToList()[i];
            //        result.Append(pt.Name);
            //        if (i != auction.ProductTags.Count - 1)
            //            result.Append(", ");
            //    }
            //    model.ProductTags = result.ToString();
            //}

            ////tax categories
            //var taxCategories = _taxCategoryService.GetAllTaxCategories();
            //model.AvailableTaxCategories.Add(new SelectListItem { Text = "---", Value = "0" });
            //foreach (var tc in taxCategories)
            //    model.AvailableTaxCategories.Add(new SelectListItem { Text = tc.Name, Value = tc.Id.ToString(), Selected = auction != null && !setPredefinedValues && tc.Id == auction.TaxCategoryId });

            ////baseprice units
            //var measureWeights = _measureService.GetAllMeasureWeights();
            //foreach (var mw in measureWeights)
            //    model.AvailableBasepriceUnits.Add(new SelectListItem { Text = mw.Name, Value = mw.Id.ToString(), Selected = auction != null && !setPredefinedValues && mw.Id == auction.BasepriceUnitId });
            //foreach (var mw in measureWeights)
            //    model.AvailableBasepriceBaseUnits.Add(new SelectListItem { Text = mw.Name, Value = mw.Id.ToString(), Selected = auction != null && !setPredefinedValues && mw.Id == auction.BasepriceBaseUnitId });

            ////specification attributes
            //var specificationAttributes = _specificationAttributeService.GetSpecificationAttributes();
            //for (int i = 0; i < specificationAttributes.Count; i++)
            //{
            //    var sa = specificationAttributes[i];
            //    model.AddSpecificationAttributeModel.AvailableAttributes.Add(new SelectListItem { Text = sa.Name, Value = sa.Id.ToString() });
            //    if (i == 0)
            //    {
            //        //attribute options
            //        foreach (var sao in _specificationAttributeService.GetSpecificationAttributeOptionsBySpecificationAttribute(sa.Id))
            //            model.AddSpecificationAttributeModel.AvailableOptions.Add(new SelectListItem { Text = sao.Name, Value = sao.Id.ToString() });
            //    }
            //}
            ////default specs values
            //model.AddSpecificationAttributeModel.ShowOnProductPage = true;

            ////discounts
            //model.AvailableDiscounts = _discountService
            //    .GetAllDiscounts(DiscountType.AssignedToSkus, showHidden: true)
            //    .Select(d => d.ToModel())
            //    .ToList();
            //if (!excludeProperties && auction != null)
            //{
            //    model.SelectedDiscountIds = auction.AppliedDiscounts.Select(d => d.Id).ToArray();
            //}

            ////default values
            //if (setPredefinedValues)
            //{
            //    model.MaximumCustomerEnteredPrice = 1000;
            //    model.MaxNumberOfDownloads = 10;
            //    model.RecurringCycleLength = 100;
            //    model.RecurringTotalCycles = 10;
            //    model.RentalPriceLength = 1;
            //    model.StockQuantity = 10000;
            //    model.NotifyAdminForQuantityBelow = 1;
            //    model.OrderMinimumQuantity = 1;
            //    model.OrderMaximumQuantity = 10000;

            //    model.UnlimitedDownloads = true;
            //    model.IsShipEnabled = true;
            //    model.AllowCustomerReviews = true;
            //    model.Published = true;
            //    model.VisibleIndividually = true;
            //}
        }

        public ActionResult RelatedProductAddPopup(int auctionId)
        {
            //if (!_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
            //    return AccessDeniedView();

            var model = new AuctionModel.AddRelatedProductModel();
            //a vendor should have access only to his products
            model.IsLoggedInAsVendor = _workContext.CurrentVendor != null;

            //categories
            model.AvailableCategories.Add(new SelectListItem { Text = _localizationService.GetResource("Admin.Common.All"), Value = "0" });
            var categories = _categoryService.GetAllCategories(showHidden: true);
            foreach (var c in categories)
                model.AvailableCategories.Add(new SelectListItem { Text = c.GetFormattedBreadCrumb(categories), Value = c.Id.ToString() });

            //manufacturers
            model.AvailableManufacturers.Add(new SelectListItem { Text = _localizationService.GetResource("Admin.Common.All"), Value = "0" });
            foreach (var m in _manufacturerService.GetAllManufacturers(showHidden: true))
                model.AvailableManufacturers.Add(new SelectListItem { Text = m.Name, Value = m.Id.ToString() });

            //stores
            model.AvailableStores.Add(new SelectListItem { Text = _localizationService.GetResource("Admin.Common.All"), Value = "0" });
            foreach (var s in _storeService.GetAllStores())
                model.AvailableStores.Add(new SelectListItem { Text = s.Name, Value = s.Id.ToString() });

            //vendors
            model.AvailableVendors.Add(new SelectListItem { Text = _localizationService.GetResource("Admin.Common.All"), Value = "0" });
            foreach (var v in _vendorService.GetAllVendors(showHidden: true))
                model.AvailableVendors.Add(new SelectListItem { Text = v.Name, Value = v.Id.ToString() });

            //product types
            //model.AvailableProductTypes = ProductType.SimpleProduct.ToSelectList(false).ToList();
            //model.AvailableProductTypes.Insert(0, new SelectListItem { Text = _localizationService.GetResource("Admin.Common.All"), Value = "0" });

            return View(model);
        }

        [HttpPost]
        public ActionResult RelatedProductAddPopupList(DataSourceRequest command, AuctionModel.AddRelatedProductModel model)
        {
            //if (!_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
            //    return AccessDeniedView();

            //a vendor should have access only to his products
            if (_workContext.CurrentVendor != null)
            {
                model.SearchVendorId = _workContext.CurrentVendor.Id;
            }

            var products = _productService.SearchProducts(
                categoryIds: new List<int> { model.SearchCategoryId },
                manufacturerId: model.SearchManufacturerId,
                storeId: model.SearchStoreId,
                vendorId: model.SearchVendorId,
                //productType: model.SearchProductTypeId > 0 ? (ProductType?)model.SearchProductTypeId : null,
                keywords: model.SearchProductName,
                pageIndex: command.Page - 1,
                pageSize: command.PageSize,
                showHidden: true
                );
            var gridModel = new DataSourceResult();
            //gridModel.Data = products.Select(x => x.ToModel());
            gridModel.Data = products.Select(x => new ModelsMapper().CreateMap<Product, ProductModel>(x));
            gridModel.Total = products.TotalCount;

            return Json(gridModel);
        }

        [HttpPost]
        [FormValueRequired("save")]
        public ActionResult RelatedProductAddPopup(string btnId, string formId, AuctionModel.AddRelatedProductModel model)
        {
            //if (!_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
            //    return AccessDeniedView();

            if (model.SelectedProductIds.Count() > 1)
            {
                ModelState.AddModelError("NumberOfProducts", "Please select a single product.");
                model.IsLoggedInAsVendor = _workContext.CurrentVendor != null;
                ViewBag.RefreshPage = false;
                ViewBag.btnId = btnId;
                ViewBag.formId = formId;
                return View(model);
            }

            if (model.SelectedProductIds != null)
            {
                foreach (int id in model.SelectedProductIds)
                {
                    var product = _productService.GetProductById(id);
                    if (product != null)
                    {
                        //a vendor should have access only to his products
                        //if (_workContext.CurrentVendor != null && product.VendorId != _workContext.CurrentVendor.Id)
                        //    continue;

                        var existingAuctionedProduct = _auctionService.GetAuctionedProductByAuctionId(model.AuctionId);
                        if (existingAuctionedProduct == null)
                        {
                            _auctionService.InsertAuctionedProduct(model.AuctionId, product.Id);
                        }
                    }
                }
            }

            //a vendor should have access only to his products
            model.IsLoggedInAsVendor = _workContext.CurrentVendor != null;
            ViewBag.RefreshPage = true;
            ViewBag.btnId = btnId;
            ViewBag.formId = formId;
            return View(model);
        }

        [HttpPost]
        public ActionResult AuctionProductList(DataSourceRequest command, int auctionId)
        {
            //if (!_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
            //    return AccessDeniedView();

            //a vendor should have access only to his products
            if (_workContext.CurrentVendor != null)
            {
                //var product = _productService.GetProductById(auctionId);
                //if (product != null && product.VendorId != _workContext.CurrentVendor.Id)
                //{
                //    return Content("This is not your product");
                //}
            }

            var auction = _auctionService.GetAuctionById(auctionId);
            var auctionProduct = _auctionService.GetAuctionedProductByAuctionId(auctionId);
            var auctionProductModel = 
                new AuctionModel.AuctionProductModel
                {
                    AuctionId = auctionId,
                    ProductId = auctionProduct.Id,
                    ProductName = auctionProduct.Name,
                    Status = auction.Status
                };
            List<AuctionModel.AuctionProductModel> auctionProductModels = new List<AuctionModel.AuctionProductModel>
            {
                auctionProductModel
            };

            var gridModel = new DataSourceResult
            {
                Data = auctionProductModels,
                Total = auctionProductModels.Count
            };

            return Json(gridModel);
        }

        [HttpPost]
        public ActionResult AuctionProductUpdate(AuctionModel.AuctionProductModel model)
        {
            //if (!_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
            //    return AccessDeniedView();

            var relatedProduct = _productService.GetRelatedProductById(model.Id);
            if (relatedProduct == null)
                throw new ArgumentException("No related product found with the specified id");

            //a vendor should have access only to his products
            if (_workContext.CurrentVendor != null)
            {
                var product = _productService.GetProductById(relatedProduct.ProductId1);
                if (product != null && product.VendorId != _workContext.CurrentVendor.Id)
                {
                    return Content("This is not your product");
                }
            }

            relatedProduct.DisplayOrder = model.DisplayOrder;
            _productService.UpdateRelatedProduct(relatedProduct);

            return new NullJsonResult();
        }

        [HttpPost]
        public ActionResult AuctionProductDelete(int id)
        {
            //if (!_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
            //    return AccessDeniedView();

            var auction = _auctionService.GetAuctionById(id);
            if (auction == null)
                throw new ArgumentException("No auction found with the specified id");

            auction.ProductId = 0;

            return new NullJsonResult();
        }
    }
}
