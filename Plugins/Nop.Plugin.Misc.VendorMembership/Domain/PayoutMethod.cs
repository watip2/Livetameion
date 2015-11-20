using Nop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.VendorMembership.Domain
{
    public class PayoutMethod : BaseEntity
    {
        public string Name { get; set; }

        // this navigational property overwrites the table name defined in centeral linking mapping class
        public virtual ICollection<VendorPayoutMethod> VendorPayoutMethods { get; set; }
    }
}
