using Nop.Plugin.Misc.VendorMembership.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.VendorMembership.Data
{
    public class VendorrrMap : EntityTypeConfiguration<Vendorrr>
    {
        public VendorrrMap()
        {
            ToTable("Vendorrr");
            HasKey(v => v.VendorrrId);

            Property(v => v.Name);
            Property(v => v.Email);
            Property(v => v.OnMailingList);
        }
    }
}
