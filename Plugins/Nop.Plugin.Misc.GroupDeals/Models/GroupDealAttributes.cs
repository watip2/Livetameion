using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.GroupDeals.Models
{
    public static class GroupDealAttributes
    {
        public static string Name { get { return "Name"; } } // string
        public static string ShortDescription { get { return "ShortDescription"; } } // string
        public static string FullDescription { get { return "FullDescription"; } } // string
        public static string PreOrderAvailabilityStartDateTimeUtc { get { return "PreOrderAvailabilityStartDateTimeUtc"; } } // DateTime
        public static string SpecialPriceStartDateTimeUtc { get { return "SpecialPriceStartDateTimeUtc"; } } // DateTime
        public static string SpecialPriceEndDateTimeUtc { get { return "SpecialPriceEndDateTimeUtc"; } } // DateTime
        public static string AvailableStartDateTimeUtc { get { return "AvailableStartDateTimeUtc"; } } // DateTime
        public static string AvailableEndDateTimeUtc { get { return "AvailableEndDateTimeUtc"; } } // DateTime
        public static string Country { get { return "Country"; } } // string
        public static string StateOrProvince { get { return "StateOrProvince"; } } // string
        public static string City { get { return "City"; } } // string
        public static string StockQuantity { get { return "StockQuantity"; } } // int
        public static string DisplayStockAvailability { get { return "DisplayStockAvailability"; } } // bool
        public static string DisplayStockQuantity { get { return "DisplayStockQuantity"; } } // bool
        public static string MinStockQuantity { get { return "MinStockQuantity"; } } // int
        //public static string LowStockActivityId { get { return "LowStockActivityId"; } }
        public static string AllowBackInStockSubscriptions { get { return "AllowBackInStockSubscriptions"; } } // bool
        public static string Price { get { return "Price"; } } // decimal
        public static string OldPrice { get { return "OldPrice"; } } // decimal
        public static string GroupDealCost { get { return "Cost"; } } // decimal
        public static string SpecialPrice { get { return "SpecialPrice"; } } // decimal
        
        //public bool VisibleIndividually { get; set; }
        //public string AdminComment { get; set; }
        //public int ProductTemplateId { get; set; }
        //public bool ShowOnHomePage { get; set; }
        //public string MetaKeywords { get; set; }
        //public string MetaDescription { get; set; }
        //public string MetaTitle { get; set; }
        //public bool AllowCustomerReviews { get; set; }
        //public int ApprovedRatingSum { get; set; }
        //public int NotApprovedRatingSum { get; set; }
        //public int ApprovedTotalReviews { get; set; }
        //public int NotApprovedTotalReviews { get; set; }
        //public bool SubjectToAcl { get; set; }
        //public bool LimitedToStores { get; set; }
        //public string Sku { get; set; }
        //public string ManufacturerPartNumber { get; set; }
        //public string Gtin { get; set; }
        //public bool IsGiftCard { get; set; }
        //public int GiftCardTypeId { get; set; }
        //public bool RequireOtherProducts { get; set; }
        //public string RequiredProductIds { get; set; }
        //public bool AutomaticallyAddRequiredProducts { get; set; }
        //public bool IsDownload { get; set; }
        //public int DownloadId { get; set; }
        //public bool UnlimitedDownloads { get; set; }
        //public int MaxNumberOfDownloads { get; set; }
        //public int? DownloadExpirationDays { get; set; }
        //public int DownloadActivationTypeId { get; set; }
        //public bool HasSampleDownload { get; set; }
        //public int SampleDownloadId { get; set; }
        //public bool HasUserAgreement { get; set; }
        //public string UserAgreementText { get; set; }
        //public bool IsRecurring { get; set; }
        //public int RecurringCycleLength { get; set; }
        //public int RecurringCyclePeriodId { get; set; }
        //public int RecurringTotalCycles { get; set; }
        //public bool IsRental { get; set; }
        //public int RentalPriceLength { get; set; }
        //public int RentalPricePeriodId { get; set; }
        //public bool IsShipEnabled { get; set; }
        //public bool IsFreeShipping { get; set; }
        //public bool ShipSeparately { get; set; }
        //public decimal AdditionalShippingCharge { get; set; }
        //public int DeliveryDateId { get; set; }
        //public bool IsTaxExempt { get; set; }
        //public int TaxCategoryId { get; set; }
        //public bool IsTelecommunicationsOrBroadcastingOrElectronicServices { get; set; }
        //public int ManageInventoryMethodId { get; set; }
        //public bool UseMultipleWarehouses { get; set; }
        //public int WarehouseId { get; set; }
        //public int NotifyAdminForQuantityBelow { get; set; }
        //public int BackorderModeId { get; set; }
        //public int OrderMinimumQuantity { get; set; }
        //public int OrderMaximumQuantity { get; set; }
        //public string AllowedQuantities { get; set; }
        //public bool AllowAddingOnlyExistingAttributeCombinations { get; set; }
        //public bool DisableBuyButton { get; set; }
        //public bool DisableWishlistButton { get; set; }
        //public bool AvailableForPreOrder { get; set; }
        //public bool CallForPrice { get; set; }
        //public decimal Price { get; set; }
        //public decimal OldPrice { get; set; }
        //public decimal ProductCost { get; set; }
        //public decimal? SpecialPrice { get; set; }
        //public bool CustomerEntersPrice { get; set; }
        //public decimal MinimumCustomerEnteredPrice { get; set; }
        //public decimal MaximumCustomerEnteredPrice { get; set; }
        //public bool BasepriceEnabled { get; set; }
        //public decimal BasepriceAmount { get; set; }
        //public int BasepriceUnitId { get; set; }
        //public decimal BasepriceBaseAmount { get; set; }
        //public int BasepriceBaseUnitId { get; set; }
        //public bool HasTierPrices { get; set; }
        //public bool HasDiscountsApplied { get; set; }
        //public decimal Weight { get; set; }
        //public decimal Length { get; set; }
        //public decimal Width { get; set; }
        //public decimal Height { get; set; }
        //public bool Published { get; set; }
    }
}
