using System;
using System.Collections.Generic;
using Nop.Plugin.Tameion.Auctions.DomainModels;
using Nop.Core.Data;
using Nop.Services.Events;
using System.Linq;

namespace Nop.Plugin.Tameion.Auctions.Services
{
    public class AuctionService : IAuctionService
    {
        private readonly IRepository<Auction> _auctionRepo;
        private readonly IEventPublisher _eventPublisher;

        public AuctionService(IRepository<Auction> auctionRepo,
            IEventPublisher eventPublisher)
        {
            _auctionRepo = auctionRepo;
            _eventPublisher = eventPublisher;
        }

        public void DeleteAuction(Auction Auction)
        {
            if (Auction == null)
                throw new ArgumentNullException("Auction");

            _auctionRepo.Delete(Auction);
            _eventPublisher.EntityDeleted(Auction);
        }

        public IEnumerable<Auction> GetAllAuctions()
        {
            return _auctionRepo.Table.ToList();
        }

        public Auction GetAuctionById(int auctionId)
        {
            return _auctionRepo.GetById(auctionId);
        }

        public IEnumerable<Auction> GetAuctionsByVendorId(int vendorId)
        {
            return _auctionRepo.Table.Where(a => a.VendorId == vendorId).ToList();
        }

        public IEnumerable<Auction> GetClosedAuctions()
        {
            return _auctionRepo.Table.Where(a => a.Status == AuctionStatus.Closed);
        }

        public IEnumerable<Auction> GetRunningAuctions()
        {
            return _auctionRepo.Table.Where(a => a.Status == AuctionStatus.Active);
        }

        public void InsertAuction(Auction Auction)
        {
            if (Auction == null)
                throw new ArgumentNullException("Auction");

            _auctionRepo.Insert(Auction);
            _eventPublisher.EntityInserted(Auction);
        }

        public void UpdateAuction(Auction Auction)
        {
            if (Auction == null)
                throw new ArgumentNullException("Auction");

            _auctionRepo.Update(Auction);
            _eventPublisher.EntityUpdated(Auction);
        }
    }
}
