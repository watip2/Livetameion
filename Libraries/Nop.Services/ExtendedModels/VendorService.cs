using System;
using System.Linq;
using Nop.Core;
using Nop.Core.Data;
using Nop.Core.Domain.Vendors;
using Nop.Services.Events;
using System.Collections.Generic;
using Nop.Plugin.Misc.VendorMembership.Domain;

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

        public IEnumerable<Vendor> GetVendorsForSubdomainAvailability(string Subdomain)
        {
            return _vendorRepository.Table.Where(v => v.PreferredSubdomainName.Contains(Subdomain)).ToList();
        }
        #endregion
    }
}