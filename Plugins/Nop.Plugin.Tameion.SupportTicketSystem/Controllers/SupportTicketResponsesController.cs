using Nop.Admin.Controllers;
using Nop.Plugin.Tameion.SupportTicketSystem.Models;
using Nop.Web.Framework.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nop.Plugin.Tameion.SupportTicketSystem.Controllers
{
    public class SupportTicketResponsesController : BasePluginController
    {
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Create()
        {
            return View("CreateResponse");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Create(SupportTicketResponse model)
        {
            return View("CreateResponse");
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View("EditResponse");
        }

        [HttpPost, ValidateAntiForgeryToken]
        public ActionResult Edit(SupportTicketResponse model)
        {
            return View("EditResponse");
        }

        public ActionResult Delete()
        {
            return View();
        }
    }
}
