using Nop.Admin.Controllers;
using Nop.Admin.Models.Catalog;
using Nop.Core.Domain.Catalog;
using Nop.Core.Infrastructure;
using Nop.Plugin.Misc.GroupDeals.Services;
using Nop.Plugin.Misc.VendorMembership.Controllers;
using Nop.Services.Common;
using Nop.Web.Framework.Kendoui;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;

namespace Nop.Plugin.Misc.GroupDeals.ActionFilters
{
    public class ActionFilters : ActionFilterAttribute, IActionFilter, IFilterProvider
    {
        private IGenericAttributeService _genericAttributeService;
        private IGroupDealService _groupDealService;

        public IEnumerable<System.Web.Mvc.Filter> GetFilters(ControllerContext controllerContext, ActionDescriptor actionDescriptor)
        {
            if ((controllerContext.Controller is ProductController || controllerContext.Controller is ProductsController) &&
                actionDescriptor.ActionName.Equals("ProductList", 
                StringComparison.InvariantCultureIgnoreCase))
            {
                return new List<System.Web.Mvc.Filter>() { new System.Web.Mvc.Filter(this, FilterScope.Action, 0) };
            }
            return new List<System.Web.Mvc.Filter>();
        }
        
        public override void OnActionExecuting(ActionExecutingContext filterContext)

        {
            // Create object parameter.
            //filterContext.ActionParameters["person"] = new Person("John", "Smith");
            
            //_vendorService = EngineContext.Current.Resolve<IIndVendorService>();

            //HttpCookie vendor_email_cookie = filterContext.HttpContext.Request.Cookies.Get("current_vendor_email");
            //HttpCookie vendor_password_cookie = filterContext.HttpContext.Request.Cookies.Get("current_vendor_password");

            //if (!_vendorService.LoginCookiesAreValid(vendor_email_cookie, vendor_password_cookie))
            //{
            //    RedirectToLoginPage(filterContext);
            //}
            //else
            //{
            //    if (!_vendorService.IsVendorAuthenticated(vendor_email_cookie.Value, vendor_password_cookie.Value))
            //    {
            //        RedirectToLoginPage(filterContext);
            //    }
            //    else
            //    {
            //        filterContext.Controller.ViewBag.CurrentVendorEmail = vendor_email_cookie.Value;
            //    }
            //}
        }

        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            // tutorial sources for changing JsonResult or ActionResult data
            /*
                http://stackoverflow.com/questions/10416951/change-the-model-in-onactionexecuting-event
                http://stackoverflow.com/questions/3570886/asp-net-mvc-return-viewresult
                http://stackoverflow.com/questions/29693402/changing-filtercontext-result-in-onresultexecuting
            */
            base.OnActionExecuted(filterContext);

            var result = filterContext.Result as ViewResultBase;
            var result1 = filterContext.Result as JsonResult;
            var d = result1.Data as DataSourceResult;
            IEnumerable products = d.Data.AsQueryable() as IEnumerable;
            
            _genericAttributeService = EngineContext.Current.Resolve<IGenericAttributeService>();
            _groupDealService = EngineContext.Current.Resolve<IGroupDealService>();

            var productModels = new List<ProductModel>();

            foreach (ProductModel productModel in products)
            {
                var groupDeal = _groupDealService.GetGroupDealProductById(productModel.Id);
                if (groupDeal == null)
                {
                    productModels.Add(productModel);
                }
            }
            
            var gridModel = new DataSourceResult();
            gridModel.Data = productModels;            
            gridModel.Total = productModels.Count;
            filterContext.Result = new JsonResult{ Data = gridModel };

            if (result == null)
            {
                // The controller action didn't return a view result 
                // => no need to continue any further
                //return;
            }
            
            //var gridModel = result.Model as DataSourceResult;
            //if (gridModel == null)
            {
                // there's no model or the model was not of the expected type 
                // => no need to continue any further
                //return;
            }

            // modify some property value
            //gridModel.Foo = "bar";
        }

        private void RedirectToLoginPage(ActionExecutingContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
            {
				area = "Vendor",
				controller = "Products",
                action = "Login"
            }));
        }
    }
}
