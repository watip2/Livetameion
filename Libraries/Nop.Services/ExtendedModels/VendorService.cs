using System;
using System.Linq;
using Nop.Core;
using Nop.Core.Data;
using Nop.Core.Domain.Vendors;
using Nop.Services.Events;
using System.Collections.Generic;
//using Nop.Plugin.Misc.VendorMembership.Domain;

namespace Nop.Services.Vendors
{
    /// <summary>
    /// Vendor service
    /// </summary>
    public partial class VendorService : IVendorService
    {
        #region Fields
        //private readonly IRepository<VendorBusiness> _vendorBusinessTypeRepository;
        #endregion

        #region Ctor
        //public VendorService(IRepository<VendorBusinessType> vendorBusinessTypeRepository)
        //{
        //    _vendorBusinessTypeRepository = vendorBusinessTypeRepository;
        //}
        //#endregion

        //#region Methods
        //public void InsertVendorBusinessType(VendorBusinessType vb)
        //{
        //    _vendorBusinessTypeRepository.Insert(vb);
        //}

        public Vendor GetVendorByHost(string Subdomain)
        {
            // Verify the host name.
            if (string.IsNullOrEmpty(Subdomain))
            {
                throw new ArgumentNullException("Subdomain");
            }

            // If the host name has a port number, strip it out.
            int portNumberIndex = Subdomain.LastIndexOf(':');
            if (portNumberIndex > 0)
            {
                Subdomain = Subdomain.Substring(0, portNumberIndex);
            }
            
            return _vendorRepository.Table.SingleOrDefault(v => v.PreferredSubdomainName.Contains(Subdomain));
        }
        #endregion
    }
}