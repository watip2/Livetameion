using Nop.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Nop.Plugin.Tameion.Auctions.DomainModels
{
    public class Auction : BaseEntity
    {
        [Key]//, ForeignKey("GroupDeal")]
        public new int Id { get; set; }
        public string Name { get; set; }
        public int ProductId { get; set; }
        public int VendorId { get; set; }
        public string ShortDescription { get; set; }
        public string FullDescription { get; set; }
        public decimal StartingPrice { get; set; }
        public decimal ReservedAmount { get; set; }
        public AuctionStatus Status { get; set; }
        public DateTime StartingDate { get; set; }
        public bool Published { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }

        public virtual ICollection<Bid> Bids { get; set; }
        //public virtual GroupDeal GroupDeal { get; set; }

        public Auction()
        {
            CreatedOnUtc = DateTime.MinValue;
            UpdatedOnUtc = DateTime.MinValue;
            StartingDate = DateTime.MinValue;
        }
    }
}
