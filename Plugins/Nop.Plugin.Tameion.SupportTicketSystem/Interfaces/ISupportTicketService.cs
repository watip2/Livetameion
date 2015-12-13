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
        SupportTicketTopic GetTicketTopicById(int ticketId);
        SupportTicketResponse GetTicketResponseById(int ticketId);
        IEnumerable<SupportTicketTopic> GetAllTicketTopics();
        IEnumerable<SupportTicketResponse> GetAllTicketResponses();
        void InsertTicketTopic(SupportTicketTopic ticketTopic);
        void InsertTicketResponse(SupportTicketResponse ticketResponse);
        void DeleteTicketTopic(SupportTicketTopic ticketTopic);
        void DeleteTicketResponse(SupportTicketResponse ticketResponse);
    }
}
