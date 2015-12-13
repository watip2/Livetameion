using Nop.Plugin.Tameion.SupportTicketSystem.DomainModels;
using Nop.Plugin.Tameion.SupportTicketSystem.Interfaces;
using Nop.Plugin.Tameion.SupportTicketSystem.Models;
using Nop.Plugin.Tameion.SupportTicketSystem.ViewModels;
using Nop.Web.Framework.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nop.Plugin.Tameion.SupportTicketSystem.Areas
{
    public class BaseTicketsController : BasePluginController
    {
        private readonly ITicketService _ticketService;

        public BaseTicketsController(ITicketService ticketService)
        {
            _ticketService = ticketService;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var tickets = _ticketService.GetAllTickets();

            return View(tickets);
        }

        [HttpGet]
        public ActionResult Create()
        {
            var model = new TicketModel();
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(TicketModel model)
        {
            if (ModelState.IsValid)
            {
                var ticket = new ModelsMapper().CreateMap<TicketModel, Ticket>(model);
                ticket.CreatedOnUtc = DateTime.Now;
                ticket.Status = TicketStatus.Open;

                _ticketService.InsertTicket(ticket);
                return RedirectToAction("Index");
            }

            return View("Create");
        }

        [HttpGet]
        public ActionResult Details(int Id)
        {
            var ticket = _ticketService.GetTicketById(Id);
            var ticketModel = new ModelsMapper().CreateMap<Ticket, TicketModel>(ticket);
            
            var model = new TicketDetailsModel
            {
             TicketModel = ticketModel,
             Replies = ticket.Replies
            };

            return View(model);
        }

        [HttpGet]
        public ActionResult Edit(int Id)
        {
            return View("Edit");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(Ticket model)
        {
            return View("EditTicket");
        }

        public ActionResult Delete()
        {
            return View();
        }
    }
}
