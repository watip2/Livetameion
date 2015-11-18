using Nop.Plugin.Misc.GroupDeals.Enums;
using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nop.Plugin.Misc.GroupDeals.ViewModels
{
    public class GroupDealViewModel
    {
        public int Id { get; set; }
        public int VendorId { get; set; }
        public List<SelectListItem> AvailableVendors { get; set; }
        public string Name { get; set; }
        [AllowHtml]
        public string ShortDescription { get; set; }
        [AllowHtml]
        public string FullDescription { get; set; }
        public List<SelectListItem> AvailableAttributeSets { get; set; }
        public string SeName { get; set; }
        public int AttributeSetId { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
        public bool AllowBackInStockSubscriptions { get; set; }
        public DateTime AvailableStartDateTimeUtc { get; set; }
        public DateTime AvailableEndDateTimeUtc { get; set; }
        public List<SelectListItem> AvailableCountries { get; set; }
        public string Country { get; set; }
        public string StateOrProvince { get; set; }
        public string City { get; set; }
        public decimal Price { get; set; }
        public decimal OldPrice { get; set; }
        public decimal GroupDealCost { get; set; }
        public decimal SpecialPrice { get; set; }
        public int StockQuantity { get; set; }
        public bool DisplayStockAvailability { get; set; }
        public bool DisplayStockQuantity { get; set; }
        public int MinStockQuantity { get; set; }
        public bool ShowOnHomePage { get; set; }
        public bool Published { get; set; }
        public string CouponCode { get; set; }
        public DateTime SpecialPriceStartDateTimeUtc { get; set; }
        public DateTime SpecialPriceEndDateTimeUtc { get; set; }
        public int GroupdealStatusId { get; set; }
        public GroupdealStatus GroupdealStatus
        {
            get { return (GroupdealStatus)this.GroupdealStatusId; }
            set { this.GroupdealStatusId = (int)value; }
        }
        public string GroupdealStatusName { get; set; }
        public string PrimaryStoreCurrencyCode { get; set; }

        public GroupdealPictureViewModel GroupdealPictureViewModel { get; set; }
    }
}
