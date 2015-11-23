using Nop.Core;
using Nop.Core.Data;
using Nop.Plugin.Misc.GroupDeals.DataAccess;
using Nop.Plugin.Misc.GroupDeals.Models;
using Nop.Plugin.Misc.GroupDeals.Services;
using Nop.Plugin.Misc.GroupDeals.ViewModels;
using Nop.Services.Catalog;
using Nop.Services.Helpers;
using Nop.Services.Media;
using Nop.Services.Vendors;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Kendoui;
using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Nop.Services.Seo;
using System.Diagnostics;
using Nop.Plugin.Misc.GroupDeals.Enums;
using Nop.Plugin.Misc.GroupDeals.Helpers;
using System.ComponentModel.DataAnnotations;
using Nop.Services.Directory;
using Nop.Core.Domain.Directory;
using Nop.Services.Localization;

namespace Nop.Plugin.Misc.GroupDeals.Areas.Vendor.Controllers
{
    public class GroupdealsController : BasePluginController
    {
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly IRepository<GroupdealPicture> _groupdealPictureRepo;
        private IGroupDealService _groupdealService;
        private IVendorService _vendorService;
        private readonly IWorkContext _workContext;
        private IPictureService _pictureService;
        private IProductService _productService;
        private IUrlRecordService _urlRecordService;
        private ICurrencyService _currencyService;
        private CurrencySettings _currencySettings;
        private ILocalizationService _localizationService;

        public GroupdealsController(
            IRepository<GroupDeal> groupDealRepo,
            IDateTimeHelper dateTimeHelper,
            IRepository<GroupdealPicture> groupdealPictureRepo,
            IGroupDealService groupdealService,
            IVendorService vendorService,
            IWorkContext workContext,
            IPictureService pictureService,
            IUrlRecordService urlRecordService,
            ICurrencyService currencyService,
            CurrencySettings currencySettings,
            ILocalizationService localizationService)
        {
            _dateTimeHelper = dateTimeHelper;
            _groupdealPictureRepo = groupdealPictureRepo;
            _groupdealService = groupdealService;
            _vendorService = vendorService;
            _workContext = workContext;
            _pictureService = pictureService;
            _urlRecordService = urlRecordService;
            _currencyService = currencyService;
            _currencySettings = currencySettings;
            _localizationService = localizationService;
        }

        [AcceptVerbs("GET")]
        public ActionResult Index()
        {
            return View();
        }

        [AcceptVerbs("POST")]
        public ActionResult List(DataSourceRequest command)
        {
            var groupDeals = _groupdealService.GetAllGroupdeals().ToList();
            var groupDealViewModels = new List<GroupDealViewModel>();
            foreach (var gd in groupDeals)
            {
                var gdvm = new ModelsMapper().CreateMap<GroupDeal, GroupDealViewModel>(gd);
                if (gd.SpecialPriceStartDateTimeUtc < gd.SpecialPriceEndDateTimeUtc)
                {
                    gdvm.GroupdealStatusName = PluginHelper.GetAttribute<DisplayAttribute>(GroupdealStatus.Running).Name;
                }
                else 
                {
                    gdvm.GroupdealStatusName = PluginHelper.GetAttribute<DisplayAttribute>(GroupdealStatus.Ended).Name;
                }
                groupDealViewModels.Add(gdvm);
            }
            
            var gridModel = new DataSourceResult
            {
                Data = groupDealViewModels,
                Total = groupDealViewModels.Count
            };

            return Json(gridModel);
        }

        [AcceptVerbs("GET")]
        public ActionResult Details(int id)
        {
            return View();
        }

        [AcceptVerbs("GET")]
        [ActionName("Edit")]
        public ActionResult Edit(int id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("id");
            }
            var groupdeal = _groupdealService.GetById(id);
            var model = new ModelsMapper().CreateMap<GroupDeal, GroupDealViewModel>(groupdeal);
            model.GroupdealPictureViewModel = new GroupdealPictureViewModel();

            var vendors = _vendorService.GetAllVendors();
            model.AvailableVendors = new List<SelectListItem>();
            foreach (var vendor in vendors)
            {
                model.AvailableVendors.Add(new SelectListItem
                {
                    Text = vendor.Name,
                    Value = vendor.Id.ToString()
                });
            }
            PrepareGroupdealViewModel(model, groupdeal, false, false);

            return View("EditGroupdeal", model);
        }

        [AcceptVerbs("POST"), ActionName("Edit"), ValidateAntiForgeryToken]
        [ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Edit(GroupDealViewModel model, bool continueEditing)
        {
            //var groupDeal = _groupdealService.GetGroupDealById(model.Id);

            //groupDeal.Name = model.Name;
            //groupDeal.CreatedOnUtc = model.CreatedOnUtc;
            //groupDeal.UpdatedOnUtc = model.UpdatedOnUtc;
            //groupDeal.Country = model.Country;
            //groupDeal.StateOrProvince = model.StateOrProvince;
            //_groupdealService.UpdateGroupdeal(groupDeal);

            //return new NullJsonResult();

            //if (!_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
            //    return AccessDeniedView();
            
            var groupdeal = _groupdealService.GetById(model.Id);
            if (groupdeal == null || groupdeal.Deleted)
                //No product found with the specified id
                return RedirectToAction("Index");

            //a vendor should have access only to his products
            if (_workContext.CurrentVendor != null && groupdeal.VendorId != _workContext.CurrentVendor.Id)
                return RedirectToAction("Index");

            if (ModelState.IsValid)
            {
                //a vendor should have access only to his products
                if (_workContext.CurrentVendor != null)
                {
                    model.VendorId = _workContext.CurrentVendor.Id;
                }
                //vendors cannot edit "Show on home page" property
                if (_workContext.CurrentVendor != null && model.ShowOnHomePage != groupdeal.ShowOnHomePage)
                {
                    model.ShowOnHomePage = groupdeal.ShowOnHomePage;
                }
                var prevStockQuantity = groupdeal.GetTotalStockQuantity();

                //groupdeal
                //groupdeal = model.ToEntity(groupdeal);
                model.CreatedOnUtc = groupdeal.CreatedOnUtc;
                groupdeal = new ModelsMapper().CreateMap<GroupDealViewModel, GroupDeal>(model);
                groupdeal.UpdatedOnUtc = DateTime.UtcNow;
                _groupdealService.UpdateGroupdeal(groupdeal);
                //search engine name
                model.SeName = groupdeal.ValidateSeName(model.SeName, "groupdeal.Name", true);
                _urlRecordService.SaveSlug(groupdeal, model.SeName, 0);
                ////locales
                //UpdateLocales(groupdeal, model);
                ////tags
                //SaveProductTags(groupdeal, ParseProductTags(model.ProductTags));
                ////warehouses
                //SaveProductWarehouseInventory(groupdeal, model);
                ////ACL (customer roles)
                //SaveProductAcl(groupdeal, model);
                ////Stores
                //SaveStoreMappings(groupdeal, model);
                ////picture seo names
                //UpdatePictureSeoNames(groupdeal);
                ////discounts
                //var allDiscounts = _discountService.GetAllDiscounts(DiscountType.AssignedToSkus, showHidden: true);
                //foreach (var discount in allDiscounts)
                //{
                //    if (model.SelectedDiscountIds != null && model.SelectedDiscountIds.Contains(discount.Id))
                //    {
                //        //new discount
                //        if (groupdeal.AppliedDiscounts.Count(d => d.Id == discount.Id) == 0)
                //            groupdeal.AppliedDiscounts.Add(discount);
                //    }
                //    else
                //    {
                //        //remove discount
                //        if (groupdeal.AppliedDiscounts.Count(d => d.Id == discount.Id) > 0)
                //            groupdeal.AppliedDiscounts.Remove(discount);
                //    }
                //}
                //_productService.UpdateProduct(groupdeal);
                //_productService.UpdateHasDiscountsApplied(groupdeal);
                ////back in stock notifications
                //if (groupdeal.ManageInventoryMethod == ManageInventoryMethod.ManageStock &&
                //    groupdeal.BackorderMode == BackorderMode.NoBackorders &&
                //    groupdeal.AllowBackInStockSubscriptions &&
                //    groupdeal.GetTotalStockQuantity() > 0 &&
                //    prevStockQuantity <= 0 &&
                //    groupdeal.Published &&
                //    !groupdeal.Deleted)
                //{
                //    _backInStockSubscriptionService.SendNotificationsToSubscribers(groupdeal);
                //}

                ////activity log
                //_customerActivityService.InsertActivity("EditProduct", _localizationService.GetResource("ActivityLog.EditProduct"), groupdeal.Name);

                //SuccessNotification(_localizationService.GetResource("Admin.Catalog.Products.Updated"));

                if (continueEditing)
                {
                    //selected tab
                    //SaveSelectedTabIndex();

                    return RedirectToAction("Edit", new { id = groupdeal.Id });
                }
                return RedirectToAction("Index");
            }

            //If we got this far, something failed, redisplay form
            //PrepareProductModel(model, groupdeal, false, true);
            //PrepareAclModel(model, groupdeal, true);
            //PrepareStoresMappingModel(model, groupdeal, true);
            return View("EditGroupdeal", model);
        }

        [HttpPost]
        public ActionResult Delete(DataSourceRequest command, int id)
        {
            _groupdealService.DeleteGroupdeal(_groupdealService.GetById(id));
            return new NullJsonResult();
        }

        [HttpGet]
        [ActionName("Create")]
        public ActionResult Create()
        {
            var model = new GroupDealViewModel();
            var vendors = _vendorService.GetAllVendors();
            model.AvailableVendors = new List<SelectListItem>();
            foreach (var vendor in vendors)
            {
                model.AvailableVendors.Add(new SelectListItem {
                    Text = vendor.Name,
                    Value = vendor.Id.ToString()
                });
            }
            
            return View("CreateGroupdeal", model);
        }

        [HttpPost]
        [ActionName("Create")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GroupDealViewModel model)
        {
            if (ModelState.IsValid)
            {
                var groupDeal = new ModelsMapper().CreateMap<GroupDealViewModel, GroupDeal>(model);
                groupDeal.CreatedOnUtc = DateTime.UtcNow;
                groupDeal.UpdatedOnUtc = DateTime.UtcNow;
                //groupDeal.AvailableEndDateTimeUtc = DateTime.Parse("01/01/2016");
                //groupDeal.AvailableStartDateTimeUtc = DateTime.UtcNow;
                groupDeal.Active = true;
                groupDeal.DisplayOrder = 1;
                groupDeal.SeName = "se-name-" + Guid.NewGuid();

                _groupdealService.InsertGroupDeal(groupDeal);
                
                return RedirectToAction("Index");
            }
            
            return View("CreateGroupdeal", model);
        }

        [ValidateInput(false)]
        public ActionResult GroupdealPictureAdd(int pictureId, int displayOrder,
            string overrideAltAttribute, string overrideTitleAttribute,
            int groupdealId)
        {
            //if (!_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
            //    return AccessDeniedView();

            if (pictureId == 0)
                throw new ArgumentException();

            var groupdeal = _groupdealService.GetById(groupdealId);
            if (groupdeal == null)
                throw new ArgumentException("No groupdeal found with the specified id");

            //a vendor should have access only to his products
            if (_workContext.CurrentVendor != null && groupdeal.VendorId != _workContext.CurrentVendor.Id)
                return RedirectToAction("Index");

            var picture = _pictureService.GetPictureById(pictureId);
            if (picture == null)
                throw new ArgumentException("No picture found with the specified id");

            _groupdealService.InsertGroupdealPicture(new GroupdealPicture
            {
                PictureId = pictureId,
                GroupdealId = groupdealId,
                DisplayOrder = displayOrder,
            });

            _pictureService.UpdatePicture(picture.Id,
                _pictureService.LoadPictureBinary(picture),
                picture.MimeType,
                picture.SeoFilename,
                overrideAltAttribute,
                overrideTitleAttribute);

            _pictureService.SetSeoFilename(pictureId, _pictureService.GetPictureSeName("groupdeal.Name"));

            return Json(new { Result = true }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GroupdealPictureList(DataSourceRequest command, int groupdealId)
        {
            //if (!_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
            //    return AccessDeniedView();

            //a vendor should have access only to his products
            if (_workContext.CurrentVendor != null)
            {
                var product = _groupdealService.GetById(groupdealId);
                if (product != null && product.VendorId != _workContext.CurrentVendor.Id)
                {
                    return Content("This is not your product");
                }
            }

            var groupdealPictures = _groupdealService.GetGroupdealPicturesByGroupdealId(groupdealId);
            var groupdealPicturesViewModel = groupdealPictures
                .Select(x =>
                {
                    var picture = _pictureService.GetPictureById(x.PictureId);
                    if (picture == null)
                        throw new Exception("Picture cannot be loaded");
                    var m = new GroupdealPictureViewModel
                    {
                        Id = x.Id,
                        GroupdealId = x.GroupdealId,
                        PictureId = x.PictureId,
                        PictureUrl = _pictureService.GetPictureUrl(picture),
                        OverrideAltAttribute = picture.AltAttribute,
                        OverrideTitleAttribute = picture.TitleAttribute,
                        DisplayOrder = x.DisplayOrder
                    };
                    return m;
                })
                .ToList();

            var gridModel = new DataSourceResult
            {
                Data = groupdealPicturesViewModel,
                Total = groupdealPicturesViewModel.Count
            };

            return Json(gridModel);
        }

        [HttpPost]
        public ActionResult GroupdealPictureUpdate(GroupdealPictureViewModel model)
        {
            //if (!_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
            //    return AccessDeniedView();

            var groupdealPicture = _groupdealService.GetGroupdealPictureById(model.Id);
            if (groupdealPicture == null)
                throw new ArgumentException("No product picture found with the specified id");

            //a vendor should have access only to his products
            if (_workContext.CurrentVendor != null)
            {
                var product = _productService.GetProductById(groupdealPicture.GroupdealId);
                if (product != null && product.VendorId != _workContext.CurrentVendor.Id)
                {
                    return Content("This is not your product");
                }
            }

            groupdealPicture.DisplayOrder = model.DisplayOrder;
            _groupdealService.UpdateGroupdealPicture(groupdealPicture);

            var picture = _pictureService.GetPictureById(groupdealPicture.PictureId);
            if (picture == null)
                throw new ArgumentException("No picture found with the specified id");

            _pictureService.UpdatePicture(picture.Id,
                _pictureService.LoadPictureBinary(picture),
                picture.MimeType,
                picture.SeoFilename,
                model.OverrideAltAttribute,
                model.OverrideTitleAttribute);

            return new NullJsonResult();
        }

        [HttpPost]
        public ActionResult GroupdealPictureDelete(int id)
        {
            //if (!_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
            //    return AccessDeniedView();

            var groupdealPicture = _groupdealService.GetGroupdealPictureById(id);
            if (groupdealPicture == null)
                throw new ArgumentException("No product picture found with the specified id");

            var groupdealId = groupdealPicture.GroupdealId;

            //a vendor should have access only to his products
            if (_workContext.CurrentVendor != null)
            {
                var groupdeal = _groupdealService.GetById(groupdealId);
                if (groupdeal != null && groupdeal.VendorId != _workContext.CurrentVendor.Id)
                {
                    return Content("This is not your product");
                }
            }
            var pictureId = groupdealPicture.PictureId;
            _groupdealService.DeleteGroupdealPicture(groupdealPicture);

            var picture = _pictureService.GetPictureById(pictureId);
            if (picture == null)
                throw new ArgumentException("No picture found with the specified id");
            _pictureService.DeletePicture(picture);

            return new NullJsonResult();
        }

        [NonAction]
        protected virtual void PrepareGroupdealViewModel(GroupDealViewModel gdvm, GroupDeal groupdeal,
            bool setPredefinedValues, bool excludeProperties)
        {
            if (gdvm == null)
                throw new ArgumentNullException("gdvm");

            //if (groupdeal != null)
            //{
            //    var parentGroupedProduct = _productService.GetProductById(groupdeal.ParentGroupedProductId);
            //    if (parentGroupedProduct != null)
            //    {
            //        gdvm.AssociatedToProductId = groupdeal.ParentGroupedProductId;
            //        gdvm.AssociatedToProductName = parentGroupedProduct.Name;
            //    }
            //}

            gdvm.PrimaryStoreCurrencyCode = _currencyService.GetCurrencyById(_currencySettings.PrimaryStoreCurrencyId).CurrencyCode;
            //gdvm.BaseWeightIn = _measureService.GetMeasureWeightById(_measureSettings.BaseWeightId).Name;
            //gdvm.BaseDimensionIn = _measureService.GetMeasureDimensionById(_measureSettings.BaseDimensionId).Name;
            //if (groupdeal != null)
            //{
            //    gdvm.CreatedOn = _dateTimeHelper.ConvertToUserTime(groupdeal.CreatedOnUtc, DateTimeKind.Utc);
            //    gdvm.UpdatedOn = _dateTimeHelper.ConvertToUserTime(groupdeal.UpdatedOnUtc, DateTimeKind.Utc);
            //}

            ////little performance hack here
            ////there's no need to load attributes, categories, manufacturers when creating a new product
            ////anyway they're not used (you need to save a product before you map add them)
            //if (groupdeal != null)
            //{
            //    foreach (var productAttribute in _productAttributeService.GetAllProductAttributes())
            //    {
            //        gdvm.AvailableProductAttributes.Add(new SelectListItem
            //        {
            //            Text = productAttribute.Name,
            //            Value = productAttribute.Id.ToString()
            //        });
            //    }
            //    foreach (var manufacturer in _manufacturerService.GetAllManufacturers(showHidden: true))
            //    {
            //        gdvm.AvailableManufacturers.Add(new SelectListItem
            //        {
            //            Text = manufacturer.Name,
            //            Value = manufacturer.Id.ToString()
            //        });
            //    }
            //    var allCategories = _categoryService.GetAllCategories(showHidden: true);
            //    foreach (var category in allCategories)
            //    {
            //        gdvm.AvailableCategories.Add(new SelectListItem
            //        {
            //            Text = category.GetFormattedBreadCrumb(allCategories),
            //            Value = category.Id.ToString()
            //        });
            //    }
            //}

            ////copy product
            //if (groupdeal != null)
            //{
            //    gdvm.CopyProductModel.Id = groupdeal.Id;
            //    gdvm.CopyProductModel.Name = "Copy of " + groupdeal.Name;
            //    gdvm.CopyProductModel.Published = true;
            //    gdvm.CopyProductModel.CopyImages = true;
            //}

            ////templates
            //var templates = _productTemplateService.GetAllProductTemplates();
            //foreach (var template in templates)
            //{
            //    gdvm.AvailableProductTemplates.Add(new SelectListItem
            //    {
            //        Text = template.Name,
            //        Value = template.Id.ToString()
            //    });
            //}

            //vendors
            //gdvm.IsLoggedInAsVendor = _workContext.CurrentVendor != null;
            gdvm.AvailableVendors.Add(new SelectListItem
            {
                Text = _localizationService.GetResource("Admin.Catalog.Products.Fields.Vendor.None"),
                Value = "0"
            });
            var vendors = _vendorService.GetAllVendors(showHidden: true);
            foreach (var vendor in vendors)
            {
                gdvm.AvailableVendors.Add(new SelectListItem
                {
                    Text = vendor.Name,
                    Value = vendor.Id.ToString()
                });
            }

            ////delivery dates
            //gdvm.AvailableDeliveryDates.Add(new SelectListItem
            //{
            //    Text = _localizationService.GetResource("Admin.Catalog.Products.Fields.DeliveryDate.None"),
            //    Value = "0"
            //});
            //var deliveryDates = _shippingService.GetAllDeliveryDates();
            //foreach (var deliveryDate in deliveryDates)
            //{
            //    gdvm.AvailableDeliveryDates.Add(new SelectListItem
            //    {
            //        Text = deliveryDate.Name,
            //        Value = deliveryDate.Id.ToString()
            //    });
            //}

            ////warehouses
            //var warehouses = _shippingService.GetAllWarehouses();
            //gdvm.AvailableWarehouses.Add(new SelectListItem
            //{
            //    Text = _localizationService.GetResource("Admin.Catalog.Products.Fields.Warehouse.None"),
            //    Value = "0"
            //});
            //foreach (var warehouse in warehouses)
            //{
            //    gdvm.AvailableWarehouses.Add(new SelectListItem
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
            //    if (groupdeal != null)
            //    {
            //        var pwi = groupdeal.ProductWarehouseInventory.FirstOrDefault(x => x.WarehouseId == warehouse.Id);
            //        if (pwi != null)
            //        {
            //            pwiModel.WarehouseUsed = true;
            //            pwiModel.StockQuantity = pwi.StockQuantity;
            //            pwiModel.ReservedQuantity = pwi.ReservedQuantity;
            //            pwiModel.PlannedQuantity = _shipmentService.GetQuantityInShipments(groupdeal, pwi.WarehouseId, true, true);
            //        }
            //    }
            //    gdvm.ProductWarehouseInventoryModels.Add(pwiModel);
            //}

            ////product tags
            //if (groupdeal != null)
            //{
            //    var result = new StringBuilder();
            //    for (int i = 0; i < groupdeal.ProductTags.Count; i++)
            //    {
            //        var pt = groupdeal.ProductTags.ToList()[i];
            //        result.Append(pt.Name);
            //        if (i != groupdeal.ProductTags.Count - 1)
            //            result.Append(", ");
            //    }
            //    gdvm.ProductTags = result.ToString();
            //}

            ////tax categories
            //var taxCategories = _taxCategoryService.GetAllTaxCategories();
            //gdvm.AvailableTaxCategories.Add(new SelectListItem { Text = "---", Value = "0" });
            //foreach (var tc in taxCategories)
            //    gdvm.AvailableTaxCategories.Add(new SelectListItem { Text = tc.Name, Value = tc.Id.ToString(), Selected = groupdeal != null && !setPredefinedValues && tc.Id == groupdeal.TaxCategoryId });

            ////baseprice units
            //var measureWeights = _measureService.GetAllMeasureWeights();
            //foreach (var mw in measureWeights)
            //    gdvm.AvailableBasepriceUnits.Add(new SelectListItem { Text = mw.Name, Value = mw.Id.ToString(), Selected = groupdeal != null && !setPredefinedValues && mw.Id == groupdeal.BasepriceUnitId });
            //foreach (var mw in measureWeights)
            //    gdvm.AvailableBasepriceBaseUnits.Add(new SelectListItem { Text = mw.Name, Value = mw.Id.ToString(), Selected = groupdeal != null && !setPredefinedValues && mw.Id == groupdeal.BasepriceBaseUnitId });

            ////specification attributes
            //var specificationAttributes = _specificationAttributeService.GetSpecificationAttributes();
            //for (int i = 0; i < specificationAttributes.Count; i++)
            //{
            //    var sa = specificationAttributes[i];
            //    gdvm.AddSpecificationAttributeModel.AvailableAttributes.Add(new SelectListItem { Text = sa.Name, Value = sa.Id.ToString() });
            //    if (i == 0)
            //    {
            //        //attribute options
            //        foreach (var sao in _specificationAttributeService.GetSpecificationAttributeOptionsBySpecificationAttribute(sa.Id))
            //            gdvm.AddSpecificationAttributeModel.AvailableOptions.Add(new SelectListItem { Text = sao.Name, Value = sao.Id.ToString() });
            //    }
            //}
            ////default specs values
            //gdvm.AddSpecificationAttributeModel.ShowOnProductPage = true;

            ////discounts
            //gdvm.AvailableDiscounts = _discountService
            //    .GetAllDiscounts(DiscountType.AssignedToSkus, showHidden: true)
            //    .Select(d => d.ToModel())
            //    .ToList();
            //if (!excludeProperties && groupdeal != null)
            //{
            //    gdvm.SelectedDiscountIds = groupdeal.AppliedDiscounts.Select(d => d.Id).ToArray();
            //}

            ////default values
            //if (setPredefinedValues)
            //{
            //    gdvm.MaximumCustomerEnteredPrice = 1000;
            //    gdvm.MaxNumberOfDownloads = 10;
            //    gdvm.RecurringCycleLength = 100;
            //    gdvm.RecurringTotalCycles = 10;
            //    gdvm.RentalPriceLength = 1;
            //    gdvm.StockQuantity = 10000;
            //    gdvm.NotifyAdminForQuantityBelow = 1;
            //    gdvm.OrderMinimumQuantity = 1;
            //    gdvm.OrderMaximumQuantity = 10000;

            //    gdvm.UnlimitedDownloads = true;
            //    gdvm.IsShipEnabled = true;
            //    gdvm.AllowCustomerReviews = true;
            //    gdvm.Published = true;
            //    gdvm.VisibleIndividually = true;
            //}
        }
    }
}
