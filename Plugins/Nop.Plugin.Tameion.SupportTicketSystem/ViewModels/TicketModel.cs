using Nop.Plugin.Tameion.SupportTicketSystem.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Tameion.SupportTicketSystem.ViewModels
{
    public class TicketModel
    {
        public string Title { get; set; }
        public int TicketStatusId { get; set; }
        public TicketStatus Status { get; set; }
        public string Message { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
