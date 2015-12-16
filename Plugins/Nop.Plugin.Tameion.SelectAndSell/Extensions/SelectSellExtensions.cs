using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Vendors;
using Nop.Core.Infrastructure;
using Nop.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Tameion.SelectAndSell.Extensions
{
    public static class SelectSellExtensions
    {
        public static void SetReferenceProductId(this Vendor vendor, int productId)
        {
            var IdsList = GetProductIds(vendor);
            if (!IdsList.Contains(productId))
            {
                IdsList.Add(productId);
            }

            var joined = string.Join(",", IdsList.Select(id => id.ToString()));
            var genericAttributeService = EngineContext.Current.Resolve<IGenericAttributeService>();
            genericAttributeService.SaveAttribute<string>(vendor, "ReferenceProductIds", joined);
        }

        public static List<int> GetReferenceProductIds(this Vendor vendor)
        {
            return GetProductIds(vendor);
        }

        private static List<int> GetProductIds(Vendor vendor)
        {
            var genericAttributeService = EngineContext.Current.Resolve<IGenericAttributeService>();
            var productIds = vendor.GetAttribute<string>("ReferenceProductIds", genericAttributeService);
            var IdsList = new List<int>();
            foreach (var pId in productIds.Split(','))
            {
                IdsList.Add(int.Parse(pId));
            }
            return IdsList;
        }
    }
}
