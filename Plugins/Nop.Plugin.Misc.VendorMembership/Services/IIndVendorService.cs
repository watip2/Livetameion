using Nop.Core.Domain.Vendors;
using Nop.Services.Vendors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Nop.Plugin.Misc.VendorMembership.Services
{
    public interface IIndVendorService : Nop.Services.Vendors.IVendorService
    {
        Vendor GetVendorByHost(string Subdomain);
        bool IsVendorAuthenticated(string email, string password);
        bool LoginCookiesAreValid(HttpCookie email, HttpCookie password);
        Vendor GetVendorByEmail(string email);
	}
}
