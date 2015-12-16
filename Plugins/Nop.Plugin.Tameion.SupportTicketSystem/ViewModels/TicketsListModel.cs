using System.Collections;
using System.Collections.Generic;

namespace Nop.Plugin.Tameion.SupportTicketSystem.ViewModels
{
    public class TicketsListModel
    {
         public IEnumerable<TicketModel> Tickets { get; set; }
    }
}