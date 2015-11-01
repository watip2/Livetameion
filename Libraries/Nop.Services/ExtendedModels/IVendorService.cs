using Nop.Core;
using Nop.Core.Domain.ExtendedModels;
using Nop.Core.Domain.Vendors;
using System.Collections.Generic;

namespace Nop.Services.Vendors
{
    public partial interface IVendorService
    {
        void InsertVendorBusinessType(VendorBusinessType vb);
        IEnumerable<Vendor> GetVendorsForSubdomainAvailability(string Subdomain);
    }
}