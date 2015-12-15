using Nop.Plugin.Tameion.Auctions.DomainModels;
using System.Data.Entity.ModelConfiguration;

namespace Nop.Plugin.Tameion.Auctions.Maps
{
    public class AuctionMap : EntityTypeConfiguration<Auction>
    {
        public AuctionMap()
        {
            // the Install() method of ObjectContext class will create table with the following name
            // this table name is overwritten by the name of navigational property in parent class
            ToTable("Auctions");
            HasKey(b => b.Id);

            Property(b => b.Name);
            Property(b => b.VendorId);
            Property(b => b.ProductId);
            Property(b => b.ShortDescription);
            Property(b => b.FullDescription);
            Property(b => b.StartingPrice);
            Property(b => b.ReservedAmount);
            Property(b => b.Status);
            Property(b => b.StartingDate);
            Property(b => b.Published);
        }
    }
}
