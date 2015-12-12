using Nop.Core.Domain.Common;
using System;
using System.Collections.Generic;
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

        public string Name { get; set; }
        public string AttentionTo { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string VacationMode { get; set; }
        public DateTime VacationEndsAt { get; set; }
        public Address ShippingAddress { get; set; }
        public List<SelectListItem> AvailableCountries { get; set; }
        public List<SelectListItem> AvailableStates { get; set; }
    }
}
