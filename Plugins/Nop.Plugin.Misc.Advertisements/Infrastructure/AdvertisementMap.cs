using Nop.Plugin.Misc.Advertisements.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.Advertisements.Infrastructure
{
    public class AdvertisementMap : EntityTypeConfiguration<Advertisement>
    {
        public AdvertisementMap()
        {
            // the Install() method of ObjectContext class will create table with the following name
            // this table name is overwritten by the name of navigational property in parent class
            ToTable("Advertisements");
            HasKey(gd => gd.Id);

            Property(gd => gd.Name);
            Property(gd => gd.VendorId);
        }
    }
}
