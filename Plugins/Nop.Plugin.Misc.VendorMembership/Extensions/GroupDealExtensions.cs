using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Vendors;
using Nop.Core.Infrastructure;
using Nop.Plugin.Misc.VendorMembership.Domain;
using Nop.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.VendorMembership.Extensions
{
    public static class VendorMembershipExtensions
    {
        public static bool HasPaidGroupDeals(this Vendor vendor)
        {
            var genericAttributeService = EngineContext.Current.Resolve<IGenericAttributeService>();
            return vendor.GetAttribute<bool>(VendorAttributes.HasPaidGroupDeals, genericAttributeService);
        }

        public static void SetHasPaidGroupDeals(this Vendor vendor, bool hasPaidGroupDeals)
        {
            var genericAttributeService = EngineContext.Current.Resolve<IGenericAttributeService>();
            genericAttributeService.SaveAttribute<bool>(vendor, VendorAttributes.HasPaidGroupDeals, hasPaidGroupDeals);
        }

        public static void SetShopName(this Vendor vendor, string shopName)
        {
            var genericAttributeService = EngineContext.Current.Resolve<IGenericAttributeService>();
            genericAttributeService.SaveAttribute<string>(vendor, VendorAttributes.ShopName, shopName);
        }

        public static string GetShopName(this Vendor vendor)
        {
            var genericAttributeService = EngineContext.Current.Resolve<IGenericAttributeService>();
            return vendor.GetAttribute<string>(VendorAttributes.ShopName, genericAttributeService);
        }
    }
}
