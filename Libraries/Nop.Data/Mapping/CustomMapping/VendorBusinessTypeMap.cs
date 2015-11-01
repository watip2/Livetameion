using Nop.Core.Domain.ExtendedModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Data.Mapping.CustomMapping
{
    public partial class VendorBusinessTypeMap : NopEntityTypeConfiguration<VendorBusinessType>
    {
        public VendorBusinessTypeMap()
        {
            this.ToTable("Vendor_BusinessType_Map");
            this.HasKey(vb => vb.Id);

            this.HasRequired(vb => vb.BusinessType)
                .WithMany()
                .HasForeignKey(vb => vb.BusinessTypeId);

            this.HasRequired(vb => vb.Vendor)
                .WithMany(v => v.VendorBusinessTypes)
                .HasForeignKey(vb => vb.VendorId);
        }
    }
}
