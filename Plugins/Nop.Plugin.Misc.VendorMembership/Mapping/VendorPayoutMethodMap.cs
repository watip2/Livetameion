using Nop.Plugin.Misc.VendorMembership.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.VendorMembership.Mapping
{
    public class VendorPayoutMethodMap : EntityTypeConfiguration<VendorPayoutMethod>
    {
        public VendorPayoutMethodMap()
        {
            // the Install() method of ObjectContext class will create table with the following name
            // this table name is overwritten by the name of navigational property in parent class
            ToTable("VendorPayoutMethods");
            HasKey(vpm => vpm.Id);

            Property(vpm => vpm.VendorId);
            Property(vpm => vpm.PayoutMethodId);

            //this.HasRequired(vpm => vpm.PayoutMethod)
            //    .WithMany(pm => pm.VendorPayoutMethods)
            //    .HasForeignKey(vpm => vpm.PayoutMethodId);

            //this.HasRequired(vpm => vpm.Vendor)
            //    .WithMany(v => v.VendorPayoutMethods)
            //    .HasForeignKey(vpm => vpm.VendorId);
        }
    }
}
