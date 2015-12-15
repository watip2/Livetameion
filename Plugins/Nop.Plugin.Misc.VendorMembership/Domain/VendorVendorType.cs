using Nop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.VendorMembership.Domain
{
    public class VendorVendorType : BaseEntity
    {
        public int VendorTypeId { get; set; }
        public int VendorId { get; set; }
    }
}