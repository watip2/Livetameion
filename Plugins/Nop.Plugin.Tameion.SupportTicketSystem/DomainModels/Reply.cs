using Nop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Tameion.SupportTicketSystem.DomainModels
{
    public class Reply : SupportTicket
    {
        public int TicketId { get; set; }
        public virtual Ticket Ticket { get; set; }
    }
}
