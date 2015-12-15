using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nop.Plugin.Tameion.BridgePay.Models
{
    public class PaymentInfoModel : BaseNopModel
    {
        public PaymentInfoModel()
        {
            CreditCardTypes = new List<SelectListItem>();
            ExpireMonths = new List<SelectListItem>();
            ExpireYears = new List<SelectListItem>();
        }

        [Display(Name = "Select Credit Card")]
        [AllowHtml]
        public string CreditCardType { get; set; }
        [Display(Name = "Select Credit Card")]
        public IList<SelectListItem> CreditCardTypes { get; set; }

        [Display(Name = "Cardholder Name")]
        [AllowHtml]
        public string CardholderName { get; set; }

        [Display(Name = "Card Number")]
        [AllowHtml]
        public string CardNumber { get; set; }

        [Display(Name = "Expiration Date")]
        [AllowHtml]
        public string ExpireMonth { get; set; }

        [Display(Name = "Expiration Date")]
        [AllowHtml]
        public string ExpireYear { get; set; }
        public IList<SelectListItem> ExpireMonths { get; set; }
        public IList<SelectListItem> ExpireYears { get; set; }

        [Display(Name = "Card Code")]
        [AllowHtml]
        public string CardCode { get; set; }
    }
}
