using Nop.Plugin.Misc.VendorMembership.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.VendorMembership.Mapping
{
    public class VendorTypeMap : EntityTypeConfiguration<VendorType>
    {
        public VendorTypeMap()
        {
            ToTable("VendorTypes");
            HasKey(i => i.Id);

            Property(i => i.Name);
            Property(i => i.Price);
        }
    }
}
