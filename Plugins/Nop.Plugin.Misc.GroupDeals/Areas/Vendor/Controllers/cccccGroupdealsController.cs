using Nop.Core;
using Nop.Core.Data;
using Nop.Plugin.Misc.GroupDeals.Models;
using Nop.Plugin.Misc.GroupDeals.ViewModels;
using Nop.Services.Catalog;
using Nop.Services.Media;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Kendoui;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nop.Plugin.Misc.GroupDeals.Areas.Vendor.Controllers
{
    public class cccccGroupdealsController : BasePluginController
    {
        private readonly IRepository<GroupDeal> _groupDealRepo;
        private readonly IWorkContext _workContext;
        private readonly IProductService _productService;
        private readonly IPictureService _pictureService;

        public cccccGroupdealsController(
            IRepository<GroupDeal> groupDealRepo,
            IWorkContext workContext,
            IProductService productService,
            IPictureService pictureService)
        {
            _groupDealRepo = groupDealRepo;
            _workContext = workContext;
            _productService = productService;
            _pictureService = pictureService;
        }

        [AcceptVerbs("GET")]
        [ActionName("Index")]
        public string Index()
        {
            return "vendor group deal view";
            //return View();
        }

        [AcceptVerbs("GET")]
        [ActionName("Details")]
        public ActionResult Details(int id)
        {
            var model = new GroupDeal();
            return View("GroupDealTemplate.Simple");
        }

        [HttpPost]
        public ActionResult GroupDealPictureList(DataSourceRequest command, int groupDealId)
        {
            //if (!_permissionService.Authorize(StandardPermissionProvider.ManageProducts))
            //    return AccessDeniedView();

            //a vendor should have access only to his products
            if (_workContext.CurrentVendor != null)
            {
                var groupdeal = _groupDealRepo.GetById(groupDealId);
                if (groupdeal != null && groupdeal.VendorId != _workContext.CurrentVendor.Id)
                {
                    return Content("This is not your product");
                }
            }

            var productPictures = _productService.GetProductPicturesByProductId(groupDealId);
            var groupdealPicturesModel = productPictures
                .Select(x =>
                {
                    var picture = _pictureService.GetPictureById(x.PictureId);
                    if (picture == null)
                        throw new Exception("Picture cannot be loaded");
                    var m = new GroupdealPictureViewModel
                    {
                        Id = x.Id,
                        GroupdealId = x.ProductId,
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
                Data = groupdealPicturesModel,
                Total = groupdealPicturesModel.Count
            };

            return Json(gridModel);
        }
    }
}
