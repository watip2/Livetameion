using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.GroupDeals.Models
{
    public static class GroupdealExtensions
    {
        public static int GetTotalStockQuantity(this GroupDeal groupdeal,
            bool useReservedQuantity = true, int warehouseId = 0)
        {
            if (groupdeal == null)
                throw new ArgumentNullException("groupdeal");

            //if (groupdeal.ManageInventoryMethod != ManageInventoryMethod.ManageStock)
            //{
            //    //We can calculate total stock quantity when 'Manage inventory' property is set to 'Track inventory'
            //    return 0;
            //}

            //if (groupdeal.UseMultipleWarehouses)
            //{
            //    var pwi = groupdeal.ProductWarehouseInventory;
            //    if (warehouseId > 0)
            //    {
            //        pwi = pwi.Where(x => x.WarehouseId == warehouseId).ToList();
            //    }
            //    var result = pwi.Sum(x => x.StockQuantity);
            //    if (useReservedQuantity)
            //    {
            //        result = result - pwi.Sum(x => x.ReservedQuantity);
            //    }
            //    return result;
            //}

            return groupdeal.StockQuantity;
        }
    }
}
