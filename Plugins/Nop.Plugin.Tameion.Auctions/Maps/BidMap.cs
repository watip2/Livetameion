using Nop.Plugin.Tameion.Auctions.DomainModels;
using System.Data.Entity.ModelConfiguration;

namespace Nop.Plugin.Tameion.Auctions.Maps
{
    public class BidMap : EntityTypeConfiguration<Bid>
    {
        public BidMap()
        {
            // the Install() method of ObjectContext class will create table with the following name
            // this table name is overwritten by the name of navigational property in parent class
            ToTable("Bids");
            HasKey(b => b.Id);

            Property(b => b.Amount);
            Property(b => b.AuctionId);
            Property(b => b.CustomerId);
        }
    }
}
