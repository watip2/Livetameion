using Nop.Core.Data;
using Nop.Core.Domain.Catalog;
using Nop.Plugin.Misc.GroupDeals.Models;
using Nop.Plugin.Misc.GroupDeals.Services;
using Nop.Services.Catalog;
using Nop.Web.Framework.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nop.Plugin.Misc.GroupDeals.Areas.Public.Controllers
{
    public class GroupDealsController : BasePluginController
    {
        private IGroupDealService _groupDealService;
        private IRepository<Product> _productRepo;

        public GroupDealsController(IGroupDealService groupDealService,
            IRepository<Product> productRepo)
        {
            _groupDealService = groupDealService;
            _productRepo = productRepo;
        }

        [AcceptVerbs("GET")]
        public ActionResult Details(int Id)
        {
            var groupDeal = _groupDealService.GetById(Id);
            //if (groupDeal == null || groupDeal.Deleted)
            //return InvokeHttp404();
            int[] ids = { 1,2,3,4,5,6,7,8,9,10,11,12,13,14,15 };
            var products = _productRepo.Table.Where(p => ids.Contains(p.Id) && p.Name.Contains("Apple"));
            return View("GroupDealTemplate.Simple", groupDeal);
        }
    }
}
