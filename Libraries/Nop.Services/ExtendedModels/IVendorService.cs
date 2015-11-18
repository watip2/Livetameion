using Nop.Core;
using Nop.Core.Domain.Vendors;
//using Nop.Plugin.Misc.VendorMembership.Domain;
using System.Collections.Generic;

namespace Nop.Services.Vendors
{
    public partial interface IVendorService
    {
        //void InsertVendorBusinessType(VendorBusinessType vb);
        Vendor GetVendorByHost(string Subdomain);
    }
}