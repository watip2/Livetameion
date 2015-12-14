using Nop.Core.Data;
using Nop.Plugin.Tameion.SupportTicketSystem.DomainModels;
using Nop.Services.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Tameion.SupportTicketSystem.Interfaces
{
    public class TicketService : ITicketService
    {
        private readonly IRepository<Ticket> _ticketRepo;
        private readonly IRepository<Reply> _replyRepo;
        private readonly IEventPublisher _eventPublisher;

        public TicketService(
            IRepository<Ticket> ticketRepo,
            IRepository<Reply> replyRepo,
            IEventPublisher eventPublisher)
        {
            _ticketRepo = ticketRepo;
            _replyRepo = replyRepo;
            _eventPublisher = eventPublisher;
        }
        
        public Ticket GetTicketById(int ticketId)
        {
            return _ticketRepo.GetById(ticketId);
        }

        public Reply GetReplyById(int replyId)
        {
            return _replyRepo.GetById(replyId);
        }

        public IEnumerable<Ticket> GetAllTickets()
        {
            return _ticketRepo.Table.ToList();
        }

        public IEnumerable<Reply> GetAllReplies()
        {
            return _replyRepo.Table.ToList();
        }

        public void InsertTicket(Ticket ticket)
        {
            if (ticket == null)
                throw new ArgumentNullException("ticket");

            _ticketRepo.Insert(ticket);

            _eventPublisher.EntityInserted(ticket);
        }

        public void InsertReply(Reply reply)
        {
            if (reply == null)
                throw new ArgumentNullException("reply");

            _replyRepo.Insert(reply);

            _eventPublisher.EntityInserted(reply);
        }

        public void UpdateReply(Reply reply)
        {
            if (reply == null)
                throw new ArgumentNullException("reply");

            _replyRepo.Update(reply);

            _eventPublisher.EntityUpdated(reply);
        }

        public void DeleteTicket(Ticket ticket)
        {
            if (ticket == null)
                throw new ArgumentNullException("ticket");

            _ticketRepo.Delete(ticket);

            _eventPublisher.EntityDeleted(ticket);
        }

        public void DeleteReply(Reply reply)
        {
            if (reply == null)
                throw new ArgumentNullException("reply");

            _replyRepo.Delete(reply);

            _eventPublisher.EntityDeleted(reply);
        }

        public void UpdateTicket(Ticket ticket)
        {
            if (ticket == null)
                throw new ArgumentNullException("ticket");

            _ticketRepo.Update(ticket);

            _eventPublisher.EntityUpdated(ticket);
        }

        public ICollection<Reply> GetRepliesByTicketId(int ticketId)
        {
            return _replyRepo.Table.Where(rep => rep.TicketId == ticketId).ToList();
        }
    }
}
