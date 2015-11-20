using Nop.Core;
using Nop.Web.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nop.Plugin.Misc.VendorMembership.Domain
{
    public class OrdersFilter
    {
        public int FilterCategoryId { get; set; }
        public List<SelectListItem> AvailableCategories { get; set; }
        public int FilterManufacturerId { get; set; }

        [NopResourceDisplayName("Manufacturer")]
        public List<SelectListItem> AvailableManufacturers { get; set; }
        public int FilterStoreId { get; set; }
        public List<SelectListItem> AvailableStores { get; set;  }
        public int FilterVendorId { get; set; }
        public List<SelectListItem> AvailableVendors { get; set; }
        public int FilterProductType { get; set; }
        public List<SelectListItem> AvailableProductTypes { get; set; }
        public int FilterOrderId { get; set; }
        [NopResourceDisplayName("Order Date From")]
        public DateTime OrderDateTimeFromUtc { get; set; }
        [NopResourceDisplayName("Order Date To")]
        public DateTime OrderDateTimeToUtc { get; set; }
        public DateTime AvailableForShippingDateTimeFromUtc { get; set; }
        public DateTime AvailableForShippingDateTimeToUtc { get; set; }
        public string SortByStr { get; set; }
        [NopResourceDisplayName("Sorty By")]
        public List<SelectListItem> SortBy { get; set; }
        public string SortOrderStr { get; set; }
        public List<SelectListItem> SortOrder = new List<SelectListItem> { 
            new SelectListItem { Text = "Ascending", Value = "0", Selected = true },
            new SelectListItem { Text = "Descending", Value = "1" }
        };
    }
}
