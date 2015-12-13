using Nop.Admin.Controllers;
using Nop.Core.Domain.Catalog;
using Nop.Core.Infrastructure;
using Nop.Plugin.Tameion.SupportTicketSystem.DomainModels;
using Nop.Services.Catalog;
using Nop.Web.Framework.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nop.Plugin.Tameion.SupportTicketSystem.Areas.Admin.Controllers
{
    public class SupportTicketTopicsController : BasePluginController
    {
        public SupportTicketTopicsController()
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
        public ActionResult Create(SupportTicketTopic model)
        {
            return View("CreateTicket");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View("EditTicket");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(SupportTicketTopic model)
        {
            return View("EditTicket");
        }

        public ActionResult Delete()
        {
            return View();
        }
    }
}
