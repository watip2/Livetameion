using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Seo;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nop.Core.Domain.Vendors
{
    public partial class Vendor : BaseEntity, ILocalizedEntity, ISlugSupported
    {
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public bool EnableGoogleAnalytics { get; set; }
        public string GoogleAnalyticsAccountNumber  { get; set; }
        public PreferredShippingCarrier PreferredShippingCarrier { get; set; }
        public string PreferredSubdomainName { get; set; }
        public string AttentionTo { get; set; }
        public string StreetAddressLine1  { get; set; }
        public string StreetAddressLine2 { get; set; }
        public string ZipPostalCode { get; set; }
        public string City  { get; set; }
        public string StateProvince { get; set; }
        public string Country  { get; set; }
        public string LogoImage { get; set; }
        
        public virtual ICollection<Nop.Plugin.Misc.VendorMembership.Domain.VendorBusinessType> VendorBusinessTypes { get; set; }
        public virtual ICollection<Nop.Plugin.Misc.VendorMembership.Domain.VendorPayoutMethod> VendorPayoutMethods { get; set; }
    }

    public enum PreferredShippingCarrier
    {
        [Display(Name = "Flat Rate")]
        FlatRate,

        [Display(Name = "Free Shipping")]
        FreeShipping,

        [Display(Name = "Best Way")]
        BestWay,

        [Display(Name = "DHL (Deprecated)")]
        DHL_Deprecated,

        [Display(Name = "Federal Express")]
        FederalExpress,

        [Display(Name = "United Parcel Service")]
        UnitedParcelService,

        [Display(Name = "United States Postal Service")]
        UnitedStatesPostalService,

        [Display(Name = "DHL")]
        DHL,

        [Display(Name = "Tier Shipping")]
        TierShipping
    }
}
