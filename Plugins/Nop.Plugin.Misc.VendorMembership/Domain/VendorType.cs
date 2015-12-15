using Nop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.VendorMembership.Domain
{
    public class VendorType : BaseEntity
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
    }
}
