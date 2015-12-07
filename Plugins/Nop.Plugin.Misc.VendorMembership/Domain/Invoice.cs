using Nop.Core;
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
    }
}
