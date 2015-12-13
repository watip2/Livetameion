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
    public class BaseRepliesController : BasePluginController
    {
        private readonly ITicketService _ticketService;
        private readonly IWorkContext _workContext;

        public BaseRepliesController(ITicketService ticketService,
            IWorkContext workContext)
        {
            _ticketService = ticketService;
            _workContext = workContext;
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View("Create");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(ReplyModel model)
        {
            if (ModelState.IsValid)
            {
                var reply = new ModelsMapper().CreateMap<ReplyModel, Reply>(model);
                reply.VendorId = _workContext.CurrentVendor.Id;
                _ticketService.InsertReply(reply);
                return RedirectToAction("Details", "Tickets", new { Id = model.TicketId });
            }

            return View("Create");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View("Edit");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(Reply model)
        {
            return View("Edit");
        }

        public ActionResult Delete()
        {
            return View();
        }
    }
}
