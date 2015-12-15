using Nop.Core.Domain.Vendors;
using Nop.Plugin.Misc.VendorMembership.Domain;
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
        bool AreLoginCookiesValid(HttpCookie email, HttpCookie password);
        Vendor GetVendorByEmail(string email);
        bool IsVendorLoggedIn();
        Vendor GetLoggedInVendor();

        #region VendoeType
        void InsertVendorType(VendorType vendorType);
        void UpdateVendorType(VendorType vendorType);
        void DeleteVendorType(VendorType vendorType);
        VendorType GetVendorTypeById(int vendorTypeId);
        IList<VendorType> GetAllVendorTypes();
        #endregion

        #region VendorVendorTypes
        void InsertVendorVendorType(VendorVendorType vendorVendorType);
        void UpdateVendorVendorType(VendorVendorType vendorVendorType);
        void DeleteVendorVendorType(VendorVendorType vendorVendorType);
        VendorVendorType GetVendorVendorTypeById(int vendorVendorTypeId);
        IList<VendorVendorType> GetAllVendorVendorTypes();
        #endregion
    }
}
