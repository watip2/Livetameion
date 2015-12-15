using Nop.Plugin.Tameion.Auctions.ViewModels;
using Nop.Web.Framework.Controllers;
using System.Web.Mvc;

namespace Nop.Plugin.Tameion.Auctions.Areas.Vendor.Controllers
{
    //[AdminAuthorize]
    public class AuctionsController : BasePluginController
    {
        public AuctionsController()
        {

        }

        [AcceptVerbs("GET")]
        public ActionResult Index()
        {
            return View();
        }

        [AcceptVerbs("GET")]
        public ActionResult Details(int Id)
        {
            return View();
        }

        [AcceptVerbs("GET")]
        public ActionResult Create()
        {
            return View();
        }

        [AcceptVerbs("POST")]
        public ActionResult Create(AuctionModel model)
        {
            return View();
        }

        [AcceptVerbs("GET")]
        public ActionResult Edit(int Id)
        {
            return View();
        }

        [AcceptVerbs("PUT")]
        public ActionResult Edit(AuctionModel model)
        {
            return View();
        }

        [AcceptVerbs("DELETE")]
        public ActionResult Delete(AuctionModel model)
        {
            return View();
        }
    }
}
