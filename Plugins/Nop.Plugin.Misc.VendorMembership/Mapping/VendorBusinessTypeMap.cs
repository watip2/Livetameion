using Nop.Plugin.Misc.VendorMembership.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.VendorMembership.Mapping
{
    public class VendorBusinessTypeMap : EntityTypeConfiguration<VendorBusinessType>
    {
        public VendorBusinessTypeMap()
        {
            // the Install() method of ObjectContext class will create table with the following name
            // this table name is overwritten by the name of navigational property in parent class
            ToTable("VendorBusinessTypes");
            HasKey(vpm => vpm.Id);

            Property(vpm => vpm.VendorId);
            Property(vpm => vpm.BusinessTypeId);

            // if we use [ForeignKey] data annotation, then no need of below fluent API
            //this.HasRequired(vbt => vbt.BusinessType)
            //    .WithMany(bt => bt.VendorBusinessTypes)
            //    .HasForeignKey(vbt => vbt.BusinessTypeId);

            //this.HasRequired(vbt => vbt.Vendor)
            //    .WithMany(v => v.VendorBusinessTypes)
            //    .HasForeignKey(vbt => vbt.VendorId);
        }
    }
}
