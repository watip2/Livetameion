using Nop.Core;
using Nop.Plugin.Misc.GroupDeals.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Tameion.Auctions.DomainModels
{
    public class Auction : BaseEntity
    {
        [Key, ForeignKey("GroupDeal")]
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

        public virtual ICollection<Bid> Bids { get; set; }
        public virtual GroupDeal GroupDeal { get; set; }
    }
}
