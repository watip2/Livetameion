using Nop.Core.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nop.Plugin.Misc.VendorMembership.ViewModels
{
    public class AccountModel
    {
        public AccountModel()
        {
            this.AvailableCountries = new List<SelectListItem>();
            this.AvailableStates = new List<SelectListItem>();
            this.ShippingAddress = new Address();
        }

        [Display(Name = "Shop Name")]
        public string ShopName { get; set; }
        [Display(Name = "Vendor Name")]
        public string AttentionTo { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }
        [Display(Name = "Vacation Mode")]
        public string VacationMode { get; set; }
        [Display(Name = "Vacation Ends At")]
        public DateTime VacationEndsAt { get; set; }
        public Address ShippingAddress { get; set; }
        public List<SelectListItem> AvailableCountries { get; set; }
        public List<SelectListItem> AvailableStates { get; set; }
    }
}
