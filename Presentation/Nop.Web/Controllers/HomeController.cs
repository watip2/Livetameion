using System.Web.Mvc;
using Nop.Web.Framework.Security;
using Nop.Core.Infrastructure;
using Nop.Services.Catalog;
using Nop.Core.Domain.Catalog;
using System;
using Nop.Core.Data;
using Nop.Plugin.Misc.TrialTracker.Domain;
using Nop.Core.Domain.Vendors;

namespace Nop.Web.Controllers
{
    public partial class HomeController : BasePublicController
    {
        [NopHttpsRequirement(SslRequirement.No)]
        public ActionResult Index()
        {
            return View();
        }
    }
}
