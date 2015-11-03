using Nop.Core;
using Nop.Core.Domain.Vendors;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.VendorMembership.Domain
{
    public class VendorPayoutMethod : BaseEntity
    {
        //[ForeignKey("Vendor")]
        public int VendorId { get; set; }
        //[ForeignKey("PayoutMethod")]
        public int PayoutMethodId { get; set; }

        // this navigational creates plural table PayoutMethods
        [ForeignKey("PayoutMethodId")]
        public virtual PayoutMethod PayoutMethod { get; set; }
        // this navigational creates plural table Tests
        //public virtual Test Test { get; set; }
        [ForeignKey("VendorId")]
        public virtual Vendor Vendor  { get; set; }
    }
}
