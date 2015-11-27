using System.Web.Mvc;
using Nop.Web.Framework;
using Nop.Web.Framework.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Nop.Plugin.Tameion.BridgePay.Models
{
    public class ConfigurationModel : BaseNopModel
    {
        public int ActiveStoreScopeConfiguration { get; set; }

        [NopResourceDisplayName("Plugins.Payments.AuthorizeNet.Fields.UseSandbox")]
        public bool UseSandbox { get; set; }
        public bool UseSandbox_OverrideForStore { get; set; }

        public int TransactModeId { get; set; }
        public bool TransactModeId_OverrideForStore { get; set; }
        [NopResourceDisplayName("Plugins.Payments.AuthorizeNet.Fields.TransactModeValues")]
        public SelectList TransactModeValues { get; set; }

        [NopResourceDisplayName("Plugins.Payments.AuthorizeNet.Fields.TransactionKey")]
        public string TransactionKey { get; set; }
        public bool TransactionKey_OverrideForStore { get; set; }

        [Display(Name = "Username")]
        public string Username { get; set; }
        public bool Username_OverrideForStore { get; set; }

        [Display(Name = "Password")]
        public string Password { get; set; }
        public bool Password_OverrideForStore { get; set; }

        [Display(Name = "Merchant Key")]
        public string MerchantKey { get; set; }
        public bool MerchantKey_OverrideForStore { get; set; }

        [NopResourceDisplayName("Plugins.Payments.AuthorizeNet.Fields.AdditionalFee")]
        public decimal AdditionalFee { get; set; }
        public bool AdditionalFee_OverrideForStore { get; set; }

        [NopResourceDisplayName("Plugins.Payments.AuthorizeNet.Fields.AdditionalFeePercentage")]
        public bool AdditionalFeePercentage { get; set; }
        public bool AdditionalFeePercentage_OverrideForStore { get; set; }
    }
}