using Nop.Plugin.Misc.VendorMembership.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.VendorMembership.Mapping
{
    public class VendorVendorTypeMap : EntityTypeConfiguration<VendorVendorType>
    {
        public VendorVendorTypeMap()
        {
            ToTable("VendorType_Vendors");
            HasKey(i => i.Id);

            Property(i => i.VendorTypeId);
            Property(i => i.VendorId);
        }
    }
}