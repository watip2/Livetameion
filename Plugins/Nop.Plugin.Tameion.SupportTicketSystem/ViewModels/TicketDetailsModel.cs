using Nop.Plugin.Tameion.SupportTicketSystem.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Tameion.SupportTicketSystem.ViewModels
{
    public class TicketDetailsModel
    {
        public TicketModel TicketModel { get; set; }
        public ICollection<Reply> Replies { get; set; }
    }
}
