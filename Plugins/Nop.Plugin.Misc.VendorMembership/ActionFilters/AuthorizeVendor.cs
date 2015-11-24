using Nop.Core.Infrastructure;
using Nop.Plugin.Misc.VendorMembership.Helpers;
using Nop.Plugin.Misc.VendorMembership.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Nop.Plugin.Misc.VendorMembership.ActionFilters
{
    public class AuthorizeVendor : ActionFilterAttribute, IActionFilter
    {
        private IIndVendorService _vendorService;
        
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _vendorService = EngineContext.Current.Resolve<IIndVendorService>();

            HttpCookie vendor_email_cookie = filterContext.HttpContext.Request.Cookies.Get("current_vendor_email");
            HttpCookie vendor_password_cookie = filterContext.HttpContext.Request.Cookies.Get("current_vendor_password");

            if (!_vendorService.LoginCookiesAreValid(vendor_email_cookie, vendor_password_cookie))
            {
                RedirectToLoginPage(filterContext);
            }
            else
            {
                if (!_vendorService.IsVendorAuthenticated(vendor_email_cookie.Value, vendor_password_cookie.Value))
                {
                    RedirectToLoginPage(filterContext);
                }
                else
                {
                    filterContext.Controller.ViewBag.CurrentVendorEmail = vendor_email_cookie.Value;
                }
            }
        }

        private void RedirectToLoginPage(ActionExecutingContext filterContext)
        {
            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
            {
                controller = "VendorMembership",
                action = "Login"
            }));
        }
    }
}
