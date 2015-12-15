using Nop.Admin.Controllers;
using Nop.Core;
using Nop.Plugin.Tameion.SupportTicketSystem.DomainModels;
using Nop.Plugin.Tameion.SupportTicketSystem.Interfaces;
using Nop.Web.Framework.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nop.Plugin.Tameion.SupportTicketSystem.Areas.Admin.Controllers
{
    public class RepliesController : BaseRepliesController
    {
        private readonly ITicketService _ticketService;
        private readonly IWorkContext _workContext;

        public RepliesController(ITicketService ticketService,
            IWorkContext workContext) : base(ticketService, workContext)
        { }
    }
}
