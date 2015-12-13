using Nop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Tameion.SupportTicketSystem.DomainModels
{
    public class Ticket : SupportTicket
    {
        public string Name { get; set; }
        public TicketStatus Status { get; set; }

        public ICollection<Reply> Replies { get; set; }
    }
}
