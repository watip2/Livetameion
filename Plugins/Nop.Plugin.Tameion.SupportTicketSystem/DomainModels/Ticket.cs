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
        public string Title { get; set; }
        public int TicketStatusId { get; set; }

        public TicketStatus Status
        {
            get { return (TicketStatus)this.TicketStatusId; }
            set { this.TicketStatusId = (int)value; }
        }
        public ICollection<Reply> Replies { get; set; }
    }
}
