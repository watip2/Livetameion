using System;
using System.Collections.Generic;
using Nop.Plugin.Tameion.Auctions.DomainModels;
using Nop.Core.Data;
using Nop.Services.Events;
using System.Linq;
using Nop.Core.Domain.Catalog;

namespace Nop.Plugin.Tameion.Auctions.Services
{
    public class AuctionService : IAuctionService
    {
        private readonly IRepository<Auction> _auctionRepo;
        private readonly IEventPublisher _eventPublisher;
        private readonly IRepository<Product> _productRepo;

        public AuctionService(IRepository<Auction> auctionRepo,
            IEventPublisher eventPublisher,
            IRepository<Product> productRepo)
        {
            _auctionRepo = auctionRepo;
            _eventPublisher = eventPublisher;
            _productRepo = productRepo;
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

        public Product GetAuctionedProductByAuctionId(int auctionId)
        {
            var auction = this.GetAuctionById(auctionId);
            var product = _productRepo.GetById(auction.ProductId);
            return product;
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

        public void InsertAuctionedProduct(int auctionId, int productId)
        {
            var auction = this.GetAuctionById(auctionId);
            auction.ProductId = productId;
            this.UpdateAuction(auction);
        }

        public Bid GetHighestBidForAuction(Auction auction)
        {
            return auction.Bids.OrderBy(b => b.Amount).LastOrDefault();
        }
    }
}
