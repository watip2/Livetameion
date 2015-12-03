using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Seo;
using Nop.Core.Domain.Vendors;
using Nop.Core.Infrastructure;
using Nop.Plugin.Misc.GroupDeals.Enums;
using Nop.Services.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nop.Plugin.Misc.GroupDeals.Models
{
    public class GroupDeal : Product
    {
        public GroupDeal()
        {
            // datetime fields default values
            CreatedOnUtc = DateTime.MinValue;
            UpdatedOnUtc = DateTime.MinValue;
            AvailableEndDateTimeUtc = DateTime.MinValue;
            AvailableStartDateTimeUtc = DateTime.MinValue;
            PreOrderAvailabilityStartDateTimeUtc = DateTime.MinValue;
            SpecialPriceStartDateTimeUtc = DateTime.MinValue;
            SpecialPriceEndDateTimeUtc = DateTime.MinValue;
        }

        // generic attributes
        [NotMapped]
        public bool Active { get; set; }
        [NotMapped]
        public string SeName { get; set; }
        [NotMapped]
        public string CouponCode { get; set; }
        [NotMapped]
        public string Country { get; set; }
        [NotMapped]
        public string StateOrProvince { get; set; }
        [NotMapped]
        public string City { get; set; }
        [NotMapped]
        public string FinePrint { get; set; }
    }
}