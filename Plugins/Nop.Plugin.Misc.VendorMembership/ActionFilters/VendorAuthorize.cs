using Nop.Core;
using Nop.Core.Infrastructure;
using Nop.Plugin.Misc.VendorMembership.Services;
using System.Web.Mvc;
using System.Web.Routing;

namespace Nop.Plugin.Misc.VendorMembership.ActionFilters
{
    public class VendorAuthorize : ActionFilterAttribute, IActionFilter
    {
        private IIndVendorService _vendorService;
        private IWorkContext _workContext;

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            _workContext = EngineContext.Current.Resolve<IWorkContext>();
            if (_workContext.CurrentVendor == null)
            {
                RedirectToLoginPage(filterContext);
            }
            else
            {
                filterContext.Controller.ViewBag.CurrentVendorEmail = _workContext.CurrentVendor.Email;
            }
            
            //_vendorService = EngineContext.Current.Resolve<IIndVendorService>();

            //HttpCookie vendor_email_cookie = filterContext.HttpContext.Request.Cookies.Get("current_vendor_email");
            //HttpCookie vendor_password_cookie = filterContext.HttpContext.Request.Cookies.Get("current_vendor_password");

            //if (!_vendorService.AreLoginCookiesValid(vendor_email_cookie, vendor_password_cookie))
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

        private void RedirectToLoginPage(ActionExecutingContext filterContext)
        {
            string actionName = filterContext.ActionDescriptor.ActionName;
            string controllerName = filterContext.ActionDescriptor.ControllerDescriptor.ControllerName;

            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new
            {
                area = "",
                controller = "Customer",
                action = "Login",
                returnUrl = "/Vendor/" + controllerName + "/" + actionName,
            }));
        }
    }
}
