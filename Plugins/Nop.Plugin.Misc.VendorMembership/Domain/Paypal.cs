using Nop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.VendorMembership.Domain
{
    public class Paypal : BaseEntity
    {
        public string Email { get; set; }

        public virtual PayoutMethod PayoutMethod { get; set; }
    }
}
