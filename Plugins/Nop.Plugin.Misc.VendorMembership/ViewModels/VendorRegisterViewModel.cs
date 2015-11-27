using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Vendors;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nop.Plugin.Misc.VendorMembership.ViewModels
{
    public class VendorRegisterViewModel
    {
        [Display(Name="Shop Name")]
        [Required]
        public string Name { get; set; }

        [Display(Name = "Email Address")]
        [Required]
        public string Email { get; set; }

        [Display(Name = "Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Enable Google Analytics")]
        public bool EnableGoogleAnalytics { get; set; }

        [Display(Name = "Google Analytics Account Number")]
        public string GoogleAnalyticsAccountNumber { get; set; }

        //[Display(Name = "Preferred Shipping Carrier")]
        //public PreferredShippingCarrier PreferredShippingCarrier { get; set; }

        [Display(Name = "Preferred Subdomain Name")]
		[Required(ErrorMessage = "This is a required field")]
        public string PreferredSubdomainName { get; set; }

        [Display(Name = "Attention To")]
        [Required(ErrorMessage = "This is a required field")]
        public string AttentionTo { get; set; }

        [Display(Name = "Street Address Line 1")]
        public string StreetAddressLine1 { get; set; }

        [Display(Name = "Street Address Line 2")]
        public string StreetAddressLine2 { get; set; }

        [Display(Name = "Zip/Postal Code")]
        public string ZipPostalCode { get; set; }

        [Display(Name = "City")]
        public string City { get; set; }

        [Display(Name = "State/Province")]
        [Required(ErrorMessage = "This is a required field")]
        public string StateProvince { get; set; }

        [Display(Name = "Country")]
        public string Country { get; set; }

        [Display(Name = "Logo Image")]
        public string LogoImage { get; set; }

        /* ===== Payout Fields ===== */
        [Display(Name = "Payout Method")]
        public PayoutMethod PayoutMethod { get; set; }

        [Display(Name = "Paypal Email")]
        public string PaypalEmail  { get; set; }

        [Display(Name = "Payout Additional Details")]
        public string PayoutAdditionalDetails { get; set; }

        [Display(Name = "Bank Name")]
        public string BankName { get; set; }

        [Display(Name = "Bank Branch Location")]
        public string BankBranchLocation  { get; set; }

        [Display(Name = "Bank Payee Name(as per account) ")]
        public string BankPayeeName { get; set; }

        [Display(Name = "Bank Account Number")]
        public string BankAccountNumber  { get; set; }

        [Display(Name = "Bank SWIFT BIC")]
        public string BankSWIFTBIC { get; set; }

        public IEnumerable<string> Countries { get; set; }

        public string MembershipOptions { get; set; }

        public int[] BusinessTypeIds { get; set; }
        
        public int[] SelectedItems { get; set; }

        public List<SelectListItem> Options { get; set; }

        ///// <summary>
        ///// Gets or sets the description
        ///// </summary>
        //public string Description { get; set; }

        ///// <summary>
        ///// Gets or sets the admin comment
        ///// </summary>
        //public string AdminComment { get; set; }

        ///// <summary>
        ///// Gets or sets a value indicating whether the entity is active
        ///// </summary>
        //public bool Active { get; set; }

        ///// <summary>
        ///// Gets or sets a value indicating whether the entity has been deleted
        ///// </summary>
        //public bool Deleted { get; set; }

        ///// <summary>
        ///// Gets or sets the display order
        ///// </summary>
        //public int DisplayOrder { get; set; }


        ///// <summary>
        ///// Gets or sets the meta keywords
        ///// </summary>
        //public string MetaKeywords { get; set; }

        ///// <summary>
        ///// Gets or sets the meta description
        ///// </summary>
        //public string MetaDescription { get; set; }

        ///// <summary>
        ///// Gets or sets the meta title
        ///// </summary>
        //public string MetaTitle { get; set; }

        ///// <summary>
        ///// Gets or sets the page size
        ///// </summary>
        //public int PageSize { get; set; }

        ///// <summary>
        ///// Gets or sets a value indicating whether customers can select the page size
        ///// </summary>
        //public bool AllowCustomersToSelectPageSize { get; set; }

        ///// <summary>
        ///// Gets or sets the available customer selectable page size options
        ///// </summary>
        //public string PageSizeOptions { get; set; }
    }

    public enum PayoutMethod
    {
        [Display(Name = "Paypal")]
        Paypal,

        [Display(Name = "Check")]
        Check,

        [Display(Name = "Bank Transfer")]
        BankTransfer,

        [Display(Name = "Offline")]
        Offline
    }

    public class Country
    {
        public string Id { get; set; }
        public string Name { get; set; }
    }

    public enum MembershipOptions
    {
        [Display(Name = "Non - Profit organization ")]
        NonProfitOrganization
    }
}
