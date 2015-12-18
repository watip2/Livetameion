using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.VendorMembership.Domain
{
    public class VendorAttributes
    {
        public static string ShopName { get { return "AttentionTo"; } }
        public static string Password { get { return "Password"; } }
        public static string Email { get { return "Email"; } }
        public static string EnableGoogleAnalytics { get { return "EnableGoogleAnalytics"; } }
        public static string GoogleAnalyticsAccountNumber { get { return "GoogleAnalyticsAccountNumber"; } }
        public static string LogoImage { get { return "LogoImage"; } }
        public static string PreferredShippingCarrier { get { return "PreferredShippingCarrier"; } }
        public static string PreferredSubdomainName { get { return "PreferredSubdomainName"; } }
        public static string HasPaidGroupDeals { get { return "HasPaidGroupDeals"; } }
    }
}
