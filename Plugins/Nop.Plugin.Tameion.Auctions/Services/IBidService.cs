
using Nop.Plugin.Tameion.Auctions.DomainModels;
using System.Collections.Generic;

namespace Nop.Plugin.Tameion.Auctions.Services
{
    public interface IBidService
    {
        Bid GetBidById(int bidId);
        IEnumerable<Bid> GetAllBids();
        void InsertBid(Bid Bid);
        void UpdateBid(Bid Bid);
        void DeleteBid(Bid Bid);
    }
}
