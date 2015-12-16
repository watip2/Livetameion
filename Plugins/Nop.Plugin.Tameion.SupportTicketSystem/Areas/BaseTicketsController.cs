using Nop.Core;
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
        private readonly IWorkContext _workContext;

        public BaseTicketsController(ITicketService ticketService,
            IWorkContext workContext)
        {
            _ticketService = ticketService;
            _workContext = workContext;
        }

        [HttpGet]
        public ActionResult Index()
        {
            AutoMapperWebConfiguration.Configure();
            var tickets =
                AutoMapper.Mapper.Map<IEnumerable<Ticket>, TicketsListModel>(_ticketService.GetAllTickets());

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
            AutoMapperWebConfiguration.Configure();
            if (ModelState.IsValid)
            {
                var ticket = AutoMapper.Mapper.Map<TicketModel, Ticket>(model);
                ticket.CreatedOnUtc = DateTime.UtcNow;
                ticket.Status = TicketStatus.Open;
                ticket.VendorId = _workContext.CurrentVendor!=null?_workContext.CurrentVendor.Id:-1;

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
            var replies = _ticketService.GetRepliesByTicketId(ticket.Id);
            
            var model = new TicketDetailsModel
            {
                TicketModel = ticketModel,
                Replies = replies,
                TicketId = ticket.Id
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
