using Nop.Core.Data;
using Nop.Plugin.Tameion.SupportTicketSystem.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Tameion.SupportTicketSystem.Interfaces
{
    public interface ITicketService
    {
        Ticket GetTicketById(int ticketId);
        Reply GetReplyById(int replyId);
        IEnumerable<Ticket> GetAllTickets();
        IEnumerable<Reply> GetAllReplies();
        void InsertTicket(Ticket ticket);
        void InsertReply(Reply reply);
        void DeleteTicket(Ticket ticket);
        void DeleteReply(Reply reply);
        void UpdateTicket(Ticket ticket);
        void UpdateReply(Reply reply);
        ICollection<Reply> GetRepliesByTicketId(int ticketId);
    }
}
