using Nop.Web.Framework.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nop.Plugin.Tameion.Auctions.Areas.Public.Controllers
{
    public class AuctionsController : BasePluginController
    {
        [AcceptVerbs("GET")]
        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        [AcceptVerbs("GET")]
        public ActionResult List()
        {
            return View();
        }
    }
}
