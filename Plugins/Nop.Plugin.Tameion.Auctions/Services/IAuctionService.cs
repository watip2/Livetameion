
using Nop.Core.Domain.Catalog;
using Nop.Plugin.Tameion.Auctions.DomainModels;
using System.Collections.Generic;

namespace Nop.Plugin.Tameion.Auctions.Services
{
    public interface IAuctionService
    {
        Auction GetAuctionById(int auctionId);
        IEnumerable<Auction> GetAllAuctions();
        IEnumerable<Auction> GetAuctionsByVendorId(int vendorId);
        IEnumerable<Auction> GetRunningAuctions();
        IEnumerable<Auction> GetClosedAuctions();
        void InsertAuction(Auction Auction);
        void UpdateAuction(Auction Auction);
        void DeleteAuction(Auction Auction);
        Product GetAuctionedProductByAuctionId(int auctionId);
        void InsertAuctionedProduct(int auctionId, int productId);
        Bid GetHighestBidForAuction(Auction auction);
    }
}
