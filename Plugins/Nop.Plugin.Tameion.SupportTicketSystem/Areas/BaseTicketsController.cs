using Nop.Plugin.Tameion.SupportTicketSystem.DomainModels;
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
        public BaseTicketsController()
        {

        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View("CreateTicket");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(Ticket model)
        {
            return View("CreateTicket");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View("EditTicket");
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
