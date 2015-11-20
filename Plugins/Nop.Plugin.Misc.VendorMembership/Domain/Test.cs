using Nop.Core;
using Nop.Core.Domain.Vendors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.VendorMembership.Domain
{
    public class Test : BaseEntity
    {
        public string Name { get; set; }
        
        //public int VendorId { get; set; }
        //public int PayoutMethodId { get; set; }
        
        //public virtual ICollection<Vendor> Vendor { get; set; }
        //public virtual ICollection<PayoutMethod> PayoutMethod { get; set; }

        // this navigational property overwrites the table name defined in centeral linking mapping class
        public virtual ICollection<VendorPayoutMethod> VendorPayoutMethods { get; set; }
    }
}
