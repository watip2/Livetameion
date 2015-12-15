using Nop.Core;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Payments;
using Nop.Core.Domain.Shipping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.VendorMembership.Domain
{
    public class Invoice : BaseEntity
    {
        public int OrderId { get; set; }
        public int VendorId { get; set; }
        public string DeliveryMethod { get; set; }
        public decimal Commission { get; set; }
        public bool IsCommissionPaid { get; set; }
        public int StoreId { get; internal set; }
        public decimal OrderTotal { get; internal set; }
        public OrderStatus OrderStatus { get; internal set; }
        public PaymentStatus PaymentStatus { get; internal set; }
        public ShippingStatus ShippingStatus { get; internal set; }
        public Address BillingAddress { get; internal set; }
        public DateTime CreatedOnUtc { get; internal set; }
        public string PaymentMethodSystemName { get; set; }
    }
}
