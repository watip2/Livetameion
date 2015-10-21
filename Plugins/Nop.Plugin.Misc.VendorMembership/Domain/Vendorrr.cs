using Nop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.VendorMembership.Domain
{
    public class Vendorrr : BaseEntity
    {
        public int VendorrrId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public bool OnMailingList { get; set; }
    }
}
