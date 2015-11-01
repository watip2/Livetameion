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
        public int TestId { get; set; }
        public int PayoutMethodId { get; set; }

        // Bug: this navigational creates plural table PayoutMethods
        public virtual PayoutMethod PayoutMethod { get; set; }
        // Bug: this navigational creates plural table Tests
        public virtual Test Test { get; set; }
    }
}
