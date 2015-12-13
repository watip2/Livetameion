using Nop.Core.Data;
using Nop.Plugin.Tameion.SupportTicketSystem.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Tameion.SupportTicketSystem.Interfaces
{
    public class SupportTicketService : ISupportTicketService
    {
        IRepository<Ticket> _ticketRepo;
        IRepository<Reply> _responseRepo;

        public SupportTicketService(
            IRepository<Ticket> ticketRepo,
            IRepository<Reply> responseRepo)
        {
            _ticketRepo = ticketRepo;
            _responseRepo = responseRepo;
        }
        
        public Ticket GetTicketTopicById(int ticketId)
        {
            return _ticketRepo.GetById(ticketId);
        }

        public Reply GetTicketResponseById(int responseId)
        {
            return _responseRepo.GetById(responseId);
        }

        public IEnumerable<Ticket> GetAllTicketTopics()
        {
            return _ticketRepo.Table.ToList();
        }

        public IEnumerable<Reply> GetAllTicketResponses()
        {
            return _responseRepo.Table.ToList();
        }

        public void InsertTicketTopic(Ticket ticketTopic)
        {
            _ticketRepo.Insert(ticketTopic);
        }

        public void InsertTicketResponse(Reply ticketResponse)
        {
            _responseRepo.Insert(ticketResponse);
        }

        public void DeleteTicketTopic(Ticket ticketTopic)
        {
            _ticketRepo.Delete(ticketTopic);
        }

        public void DeleteTicketResponse(Reply ticketResponse)
        {
            _responseRepo.Delete(ticketResponse);
        }
    }
}
