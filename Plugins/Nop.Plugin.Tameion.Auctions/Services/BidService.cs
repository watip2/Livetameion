
using System;
using System.Collections.Generic;
using Nop.Plugin.Tameion.Auctions.DomainModels;
using Nop.Services.Events;

namespace Nop.Plugin.Tameion.Auctions.Services
{
    public class BidService : IBidService
    {
        private readonly AuctionService _auctionService;
        private readonly IBidService _bidService;
        private readonly IEventPublisher _eventPublisher;

        public BidService(AuctionService auctionService,
            IEventPublisher eventPublisher,
            IBidService bidService)
        {
            _auctionService = auctionService;
            _eventPublisher = eventPublisher;
            _bidService = bidService;
        }

        public void DeleteBid(Bid Bid)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Bid> GetAllBids()
        {
            throw new NotImplementedException();
        }

        public Bid GetBidById(int bidId)
        {
            throw new NotImplementedException();
        }

        public void InsertBid(Bid Bid)
        {
            if (Bid == null)
                throw new ArgumentNullException("Bid");

            var auction = _auctionService.GetAuctionById(Bid.AuctionId);
            if (Bid.Amount > _auctionService.GetHighestBidForAuction(auction).Amount)
            {
                _bidService.InsertBid(Bid);
            }
            else
            {
                throw new Exception("Bid amount too low.");
            }

            _eventPublisher.EntityInserted(Bid);
        }

        public void UpdateBid(Bid Bid)
        {
            throw new NotImplementedException();
        }
    }
}
