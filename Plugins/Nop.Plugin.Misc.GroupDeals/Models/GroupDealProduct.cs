using Nop.Core.Domain.Catalog;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.GroupDeals.Models
{
    public class GroupDealProduct : Product
    {
        public GroupDealProduct()
        {
            // datetime fields
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
    }
}
