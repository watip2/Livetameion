using Nop.Plugin.Misc.VendorMembership.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.VendorMembership.Services
{
    public interface IVendorAddressService
    {
        void InsertVendorAddress(VendorAddress vendorAddress);
        VendorAddress GetVendorAddressById(int id);
        VendorAddress GetVendorAddressByVendorId(int vendorId);
        VendorAddress GetVendorAddressByAddressId(int addressId);
        void UpdateVendorAddress(VendorAddress vendorAddress);
        void DeleteVendorAddress(VendorAddress vendorAddress);
    }
}
