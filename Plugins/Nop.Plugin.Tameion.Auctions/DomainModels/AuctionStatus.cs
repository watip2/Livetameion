using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Tameion.Auctions.DomainModels
{
    public enum AuctionStatus
    {
        [Display(Name="Pending")]
        Pending,
        [Display(Name = "Active")]
        Active,
        [Display(Name = "Closed")]
        Closed
    }
}
