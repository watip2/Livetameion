using Nop.Core.Data;
using Nop.Plugin.Misc.GroupDeals.DataAccess;
using Nop.Plugin.Misc.GroupDeals.Models;
using Nop.Plugin.Misc.GroupDeals.Services;
using Nop.Plugin.Misc.GroupDeals.ViewModels;
using Nop.Services.Helpers;
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

namespace Nop.Plugin.Misc.GroupDeals.Controllers
{
    public class VendorGroupDealsController : BasePluginController
    {
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly IRepository<GroupdealPicture> _groupdealPictureRepo;
        private IGroupDealService _groupdealService;
        private IVendorService _vendorService;

        public VendorGroupDealsController(
            IRepository<GroupDeal> groupDealRepo,
            IDateTimeHelper dateTimeHelper,
            IRepository<GroupdealPicture> groupdealPictureRepo,
            IGroupDealService groupdealService,
            IVendorService vendorService)
        {
            _dateTimeHelper = dateTimeHelper;
            _groupdealPictureRepo = groupdealPictureRepo;
            _groupdealService = groupdealService;
            _vendorService = vendorService;
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
        public ActionResult Edit(int id)
        {
            if (id == null)
            { 
                // exception    
            }

            return View();
        }

        [AcceptVerbs("POST")]
        public ActionResult Edit(GroupDeal model)
        {
            var groupDeal = _groupdealService.GetGroupDealById(model.Id);

            groupDeal.Name = model.Name;
            groupDeal.CreatedOnUtc = model.CreatedOnUtc;
            groupDeal.UpdatedOnUtc = model.UpdatedOnUtc;
            groupDeal.Country = model.Country;
            groupDeal.StateOrProvince = model.StateOrProvince;
            _groupdealService.UpdateGroupdeal(groupDeal);

            return new NullJsonResult();
        }

        [HttpPost]
        public ActionResult Delete(DataSourceRequest command, int id)
        {
            _groupdealService.DeleteGroupdeal(_groupdealService.GetGroupDealById(id));
            return new NullJsonResult();
        }

        [HttpGet]
        [ActionName("AddNew")]
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
        [ActionName("AddNew")]
        [ValidateAntiForgeryToken]
        public ActionResult Create(GroupDealViewModel model)
        {
            if (ModelState.IsValid)
            {
                var groupDeal = new GroupDeal();

                groupDeal.Name = model.Name;
                groupDeal.ShortDescription = model.ShortDescription;
                groupDeal.FullDescription = model.FullDescription;
                groupDeal.CreatedOnUtc = DateTime.UtcNow;
                groupDeal.UpdatedOnUtc = DateTime.UtcNow;
                groupDeal.AvailableEndDateTimeUtc = DateTime.Parse("01/01/2016");
                groupDeal.AvailableStartDateTimeUtc = DateTime.UtcNow;
                groupDeal.Active = true;
                groupDeal.DisplayOrder = 1;

                _groupdealService.InsertGroupDeal(groupDeal);
                
                return RedirectToAction("Index");
            }
            
            return View("CreateGroupDeal", model);
        }
    }
}
