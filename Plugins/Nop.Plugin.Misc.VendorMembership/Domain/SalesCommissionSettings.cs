using Nop.Core.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.VendorMembership.Domain
{
    public class SalesCommissionSettings : ISettings
    {
        public decimal SimpleProductFee { get; set; }
        public decimal GroupedProductFee { get; set; }
        public decimal GroupDealFee { get; set; }
        public decimal RestaurantFee { get; set; }
        public decimal VehicleFee { get; set; }
    }
}
