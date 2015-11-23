using Nop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Tameion.SupportTicketSystem.Models
{
    public class SupportTicketTopic : SupportTicket
    {
        public string Name { get; set; }
        public SupportTicketStatus Status { get; set; }

        public ICollection<SupportTicketResponse> SupportTicketResponses { get; set; }
    }
}
