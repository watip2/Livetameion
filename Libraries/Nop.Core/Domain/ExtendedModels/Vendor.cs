using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Seo;

namespace Nop.Core.Domain.Vendors
{
    public partial class Vendor : BaseEntity, ILocalizedEntity, ISlugSupported
    {
        public string PhoneNumber { get; set; }
        public string Password { get; set; }
        public bool EnableGoogleAnalytics { get; set; }
        public string GoogleAnalyticsAccountNumber  { get; set; }
        public string PreferredShippingCarrier  { get; set; }
        public string PreferredSubdomainName { get; set; }
        public string AttentionTo { get; set; }
        public string StreetAddressLine1  { get; set; }
        public string StreetAddressLine2 { get; set; }
        public string ZipPostalCode { get; set; }
        public string City  { get; set; }
        public string StateProvince { get; set; }
        public string Country  { get; set; }
        public string LogoImage { get; set; }
    }
}
