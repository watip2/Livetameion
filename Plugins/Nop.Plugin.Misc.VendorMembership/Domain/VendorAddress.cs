using Nop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.VendorMembership.Domain
{
    public class VendorAddress : BaseEntity
    {
        public int VendorId { get; set; }
        public int AddressId { get; set; }
        public int AddressTypeId { get; set; }
        public AddressType AddressType
        {
            get
            {
                return (AddressType)this.AddressTypeId;
            }
            set
            {
                this.AddressTypeId = (int)value;
            }
        }
    }
}
