using Nop.Admin.Controllers;
using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Infrastructure;
using Nop.Plugin.Tameion.SupportTicketSystem.DomainModels;
using Nop.Plugin.Tameion.SupportTicketSystem.Interfaces;
using Nop.Services.Catalog;
using Nop.Web.Framework.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nop.Plugin.Tameion.SupportTicketSystem.Areas.Vendor.Controllers
{
    public class TicketsController : BaseTicketsController
    {
        public TicketsController(ITicketService ticketService,
            IWorkContext workContext) : base(ticketService, workContext)
        { }
    }
}
