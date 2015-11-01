using Nop.Core.Data;
using Nop.Core.Domain.ExtendedModels;
using Nop.Core.Domain.Vendors;
using Nop.Core.Infrastructure;
using Nop.Plugin.Misc.VendorMembership.Domain;
using Nop.Plugin.Misc.VendorMembership.DTOs;
using Nop.Plugin.Misc.VendorMembership.Helpers;
using Nop.Plugin.Misc.VendorMembership.ViewModels;
using Nop.Services.Catalog;
using Nop.Services.Vendors;
using Nop.Web.Framework.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nop.Plugin.Misc.VendorMembership.Controllers
{
    public class VendorMembershipController : BasePluginController
    {
        private IRepository<Vendor> _vendorRepo;
        private List<Nop.Plugin.Misc.VendorMembership.DTOs.Category> _categories;
        private IRepository<VendorBusinessType> _vendorBusinessTypeRepository;

        public VendorMembershipController() { }

        public VendorMembershipController(IRepository<Vendor> vendorRepo, IRepository<VendorBusinessType> vendorBusinessTypeRepository)
        {
            _vendorRepo = vendorRepo;
            _categories = new List<DTOs.Category>();
            _vendorBusinessTypeRepository = vendorBusinessTypeRepository;
        }

        [HttpGet]
        public ActionResult Register()
        {
            VendorRegisterViewModel model = new VendorRegisterViewModel();
            model.Countries = VendorRegistrationHelper.GetCountriesNames();

            var categoryService = EngineContext.Current.Resolve<ICategoryService>();
            var AllCategories = categoryService.GetAllCategories();

            foreach (var EachCategory in AllCategories)
            {
                var cat = new Nop.Plugin.Misc.VendorMembership.DTOs.Category();
                cat.CategoryId = EachCategory.Id;
                cat.Name = EachCategory.Name;
                
                if (EachCategory.ParentCategoryId != 0)
                {
                    var ParentCategory = _categories.SingleOrDefault(c => c.CategoryId.Equals(EachCategory.ParentCategoryId));
                    if (ParentCategory != null)
                    {
                        if (ParentCategory.ChildrenCategories == null)
                        {
                            ParentCategory.ChildrenCategories = new List<DTOs.Category>();
                        }
                        ParentCategory.ChildrenCategories.Add(cat);
                    }
                }
                else
                {
                    _categories.Add(cat);
                }
            }

            model.SelectedItems = new int[] { 1, 3 };
            model.Options = new List<SelectListItem>();
            foreach (var _category in _categories)
            {
                model.Options.Add(new SelectListItem{ Value = _category.CategoryId.ToString(), Text = _category.Name });
                if (_category.ChildrenCategories != null && _category.ChildrenCategories.Count > 0)
                {
                    foreach (var _childCategory in _category.ChildrenCategories)
                    {
                        model.Options.Add(new SelectListItem { Value = _childCategory.CategoryId.ToString(), Text = "--" + _childCategory.Name });
                    }
                }
            }

            //categoryService.
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(VendorRegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                Vendor vendor = new Vendor();
                vendor.Name = model.Name;
                vendor.AttentionTo = model.AttentionTo;
                vendor.City = model.City;
                vendor.Password = model.Password;
                vendor.Country = model.Country;
                vendor.Email = model.Email;
                vendor.EnableGoogleAnalytics =  model.EnableGoogleAnalytics;
                vendor.GoogleAnalyticsAccountNumber = model.GoogleAnalyticsAccountNumber;
                vendor.LogoImage = model.LogoImage;
                vendor.PhoneNumber = model.PhoneNumber;
                vendor.PreferredShippingCarrier = model.PreferredShippingCarrier;
                vendor.PreferredSubdomainName = model.PreferredSubdomainName;
                vendor.StateProvince = model.StateProvince;
                vendor.StreetAddressLine1 = model.StreetAddressLine1;
                vendor.StreetAddressLine2 = model.StreetAddressLine2;
                vendor.ZipPostalCode = model.ZipPostalCode;

                var vendorServiceCore = EngineContext.Current.Resolve<IVendorService>();
                vendorServiceCore.InsertVendor(vendor);
                
                foreach (var BusinessTypeId in model.BusinessTypeIds)
                {
                    var vb = new VendorBusinessType();
                    var vendorServiceExt = new VendorService(_vendorBusinessTypeRepository);

                    vb.VendorId = vendor.Id;
                    vb.BusinessTypeId = BusinessTypeId;
                    vendorServiceExt.InsertVendorBusinessType(vb);
                }
                
                return RedirectToAction("Dashboard", "VendorMembership");
            }

            model.Countries = VendorRegistrationHelper.GetCountriesNames();
            return View(model);
        }

        [HttpGet]
        public ActionResult Dashboard()
        {
            return View();
        }

        [HttpPost]
        public JsonResult CheckSubdomainAvailability(string Subdomain)
        {
            var VendorResponse = new VendorResponse();
            var VendorService = EngineContext.Current.Resolve<IVendorService>();
            var Vendors = VendorService.GetVendorsForSubdomainAvailability(Subdomain);
            if (Vendors.Count() > 0)
            {
                VendorResponse.Message = "This domain is already taken.";
            }
            else
            {
                VendorResponse.Message = "This domain is available.";
            }
            
            return Json(VendorResponse);
        }
    }
}
