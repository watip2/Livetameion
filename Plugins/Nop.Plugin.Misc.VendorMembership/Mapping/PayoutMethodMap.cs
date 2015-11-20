using Nop.Plugin.Misc.VendorMembership.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.VendorMembership.Mapping
{
    public class PayoutMethodMap : EntityTypeConfiguration<PayoutMethod>
    {
        public PayoutMethodMap()
        {
            // the Install() method of ObjectContext class will create table with the following name
            ToTable("PayoutMethods");
            HasKey(v => v.Id);

            Property(v => v.Name);

            this.HasMany<VendorPayoutMethod>(pm => pm.VendorPayoutMethods)
                .WithRequired(vpm => vpm.PayoutMethod)
                .HasForeignKey(vpm => vpm.PayoutMethodId).WillCascadeOnDelete(true);
        }
    }
}
