using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.GroupDeals.Models
{
    public static class GroupDealAttributes
    {
        public static string IsGroupDeal { get { return "IsGroupDeal"; } } // string
        public static string Active { get { return "Active"; } } // string
        public static string SeName { get { return "SeName"; } } // string
        public static string CouponCode { get { return "CouponCode"; } } // string
        public static string Country { get { return "Country"; } } // string
        public static string StateOrProvince { get { return "StateOrProvince"; } } // string
        public static string City { get { return "City"; } } // string
        public static string FinePrint { get { return "FinePrint"; } } // string
    }
}
