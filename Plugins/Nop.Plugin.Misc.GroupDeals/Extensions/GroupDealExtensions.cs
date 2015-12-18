using Nop.Core.Domain.Catalog;
using Nop.Core.Infrastructure;
using Nop.Plugin.Misc.GroupDeals.Models;
using Nop.Services.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.GroupDeals.Extensions
{
    public static class GroupDealExtensions
    {
        public static bool IsGroupDeal(this Product product)
        {
            var genericAttributeService = EngineContext.Current.Resolve<IGenericAttributeService>();
            return product.GetAttribute<bool>(GroupDealAttributes.IsGroupDeal);
        }

        public static void SetIsGroupDeal(this Product product, bool isGroupDeal)
        {
            var genericAttributeService = EngineContext.Current.Resolve<IGenericAttributeService>();
            genericAttributeService.SaveAttribute<bool>(product, GroupDealAttributes.IsGroupDeal, isGroupDeal);
        }
    }
}
