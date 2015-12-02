using Nop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Tameion.Auctions.DomainModels
{
    public class Bid : BaseEntity
    {
        public int AuctionId { get; set; }
        public int CustomerId { get; set; }
        public decimal Amount { get; set; }

        public virtual Auction Auction { get; set; }
    }
}
