using Nop.Plugin.Misc.VendorMembership.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.VendorMembership.Mapping
{
    public class TestMap : EntityTypeConfiguration<Test>
    {
        public TestMap()
        {
            // the Install() method of ObjectContext class will create table with the following name
            ToTable("Tests");
            HasKey(v => v.Id);

            Property(v => v.Name);

            //this.HasMany<VendorPayoutMethod>(pm => pm.VendorPayoutMethods)
            //    .WithRequired(vpm => vpm.Test)
            //    .HasForeignKey(vpm => vpm.TestId).WillCascadeOnDelete(true);

            //this.HasMany(t => t.VendorPayoutMethods)
                //.WithMany();
            //    .WithRequired();
                //.Map(m => m.ToTable("VendorPayoutMethodMap"));

            //Property(v => v.VendorId);
            //Property(v => v.PayoutMethodId);

            //this.HasRequired(pm => pm.PayoutMethod)
            //    .WithMany()
            //    .HasForeignKey(pm => pm.PayoutMethodId);

            //this.HasRequired(pm => pm.Vendor)
            //    .WithMany()
            //    .HasForeignKey(pm => pm.VendorId);
        }
    }
}
