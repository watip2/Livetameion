using Nop.Core.Data;
using Nop.Plugin.Tameion.SupportTicketSystem.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Tameion.SupportTicketSystem.Interfaces
{
    public interface ISupportTicketService
    {
        Ticket GetTicketTopicById(int ticketId);
        Reply GetTicketResponseById(int ticketId);
        IEnumerable<Ticket> GetAllTicketTopics();
        IEnumerable<Reply> GetAllTicketResponses();
        void InsertTicketTopic(Ticket ticketTopic);
        void InsertTicketResponse(Reply ticketResponse);
        void DeleteTicketTopic(Ticket ticketTopic);
        void DeleteTicketResponse(Reply ticketResponse);
    }
}
