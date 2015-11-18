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

namespace Nop.Plugin.Misc.GroupDeals.Controllers
{
    public class VendorGroupDealsController : BasePluginController
    {
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly IRepository<GroupdealPicture> _groupdealPictureRepo;
        private IGroupDealService _groupdealService;
        private IVendorService _vendorService;
        private readonly IWorkContext _workContext;
        private IPictureService _pictureService;
        private IProductService _productService;
        private IUrlRecordService _urlRecordService;

        public VendorGroupDealsController(
            IRepository<GroupDeal> groupDealRepo,
            IDateTimeHelper dateTimeHelper,
            IRepository<GroupdealPicture> groupdealPictureRepo,
            IGroupDealService groupdealService,
            IVendorService vendorService,
            IWorkContext workContext,
            IPictureService pictureService,
            IUrlRecordService urlRecordService)
        {
            _dateTimeHelper = dateTimeHelper;
            _groupdealPictureRepo = groupdealPictureRepo;
            _groupdealService = groupdealService;
            _vendorService = vendorService;
            _workContext = workContext;
            _pictureService = pictureService;
            _urlRecordService = urlRecordService;
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

            var gridModel = new DataSourceResult
            {
                Data = groupDeals,
                Total = groupDeals.Count
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
        [ActionName("CreateGroupdeal")]
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
            
            return View("CreateGroupDeal", model);
        }

        [HttpPost]
        [ActionName("CreateGroupdeal")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GroupDealViewModel model)
        {
            Debugger.Break();
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
            
            return View("CreateGroupDeal", model);
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
    }
}
