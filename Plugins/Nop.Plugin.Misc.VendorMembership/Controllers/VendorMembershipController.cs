using Nop.Core.Data;
using Nop.Core.Domain.Vendors;
using Nop.Plugin.Misc.VendorMembership.Domain;
using Nop.Plugin.Misc.VendorMembership.ViewModels;
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
        
        public VendorMembershipController(IRepository<Vendor> vendorRepo)
        {
            _vendorRepo = vendorRepo;
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
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
                
                _vendorRepo.Insert(vendor);

                return RedirectToAction("Dashboard", "VendorMembership");
            }
            
            return View(model);
        }

        [HttpGet]
        public ActionResult Dashboard()
        {
            return View();
        }
    }
}
