using Nop.Plugin.Misc.VendorMembership.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.VendorMembership.Mapping
{
    class VendorAddressMap : EntityTypeConfiguration<VendorAddress>
    {
        public VendorAddressMap()
        {
            ToTable("VendorAddresses");
            HasKey(va => va.Id);

            Property(va => va.VendorId);
            Property(va => va.AddressId);
        }
    }
}
