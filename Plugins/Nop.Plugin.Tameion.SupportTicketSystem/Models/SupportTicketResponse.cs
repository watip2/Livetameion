using Nop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Tameion.SupportTicketSystem.Models
{
    public class SupportTicketResponse : SupportTicket
    {
        public int SupportTicketTopicId { get; set; }
        public virtual SupportTicketTopic SupportTicketTopic { get; set; }
    }
}
