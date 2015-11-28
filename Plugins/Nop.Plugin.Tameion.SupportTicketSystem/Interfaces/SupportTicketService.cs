using Nop.Core.Data;
using Nop.Plugin.Tameion.SupportTicketSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Tameion.SupportTicketSystem.Interfaces
{
    public class SupportTicketService : ISupportTicketService
    {
        IRepository<SupportTicketTopic> _ticketRepo;
        IRepository<SupportTicketResponse> _responseRepo;

        public SupportTicketService(
            IRepository<SupportTicketTopic> ticketRepo,
            IRepository<SupportTicketResponse> responseRepo)
        {
            _ticketRepo = ticketRepo;
            _responseRepo = responseRepo;
        }
        
        public SupportTicketTopic GetTicketTopicById(int ticketId)
        {
            return _ticketRepo.GetById(ticketId);
        }

        public SupportTicketResponse GetTicketResponseById(int responseId)
        {
            return _responseRepo.GetById(responseId);
        }

        public IEnumerable<SupportTicketTopic> GetAllTicketTopics()
        {
            return _ticketRepo.Table.ToList();
        }

        public IEnumerable<SupportTicketResponse> GetAllTicketResponses()
        {
            return _responseRepo.Table.ToList();
        }

        public void InsertTicketTopic(SupportTicketTopic ticketTopic)
        {
            _ticketRepo.Insert(ticketTopic);
        }

        public void InsertTicketResponse(SupportTicketResponse ticketResponse)
        {
            _responseRepo.Insert(ticketResponse);
        }

        public void DeleteTicketTopic(SupportTicketTopic ticketTopic)
        {
            _ticketRepo.Delete(ticketTopic);
        }

        public void DeleteTicketResponse(SupportTicketResponse ticketResponse)
        {
            _responseRepo.Delete(ticketResponse);
        }
    }
}
