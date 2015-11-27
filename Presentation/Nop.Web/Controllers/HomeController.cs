using System.Web.Mvc;
using Nop.Web.Framework.Security;
using Nop.Plugin.Tameion.SupportTicketSystem.Models;
using System;
using Nop.Core.Infrastructure;
using Nop.Plugin.Tameion.SupportTicketSystem.Interfaces;
using Nop.Plugin.Misc.GroupDeals.Services;
using Nop.Core.Domain.Vendors;
using Nop.Plugin.Misc.VendorMembership.Services;

namespace Nop.Web.Controllers
{
    public partial class HomeController : BasePublicController
    {
        [NopHttpsRequirement(SslRequirement.No)]
        public ActionResult Index()
        {
            var ticket = new SupportTicketTopic
            {
                Name = "ticket name",
                CustomerId = 1,
                CreatedOnUtc = DateTime.Now,
                Message = "ticket message",
                Status = SupportTicketStatus.Open
            };
            
            var vService = EngineContext.Current.Resolve<IIndVendorService>();
            vService.InsertVendor(new Vendor
            {
                Name = "sohail",
                Active = true,
                Email = "sohail@yahoo.com",
                Deleted = false,
                DisplayOrder = 1
            });

            return View();
        }
    }
}
