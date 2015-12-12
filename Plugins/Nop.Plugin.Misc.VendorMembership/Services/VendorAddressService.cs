using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Plugin.Misc.VendorMembership.Domain;
using Nop.Services.Events;
using Nop.Core.Data;

namespace Nop.Plugin.Misc.VendorMembership.Services
{
    public class VendorAddressService : IVendorAddressService
    {
        IRepository<VendorAddress> _vendorAddressRepo;
        IEventPublisher _eventPublisher;
        public VendorAddressService(
            IRepository<VendorAddress> vendorAddressRepo,
            IEventPublisher eventPublisher)
        {
            _vendorAddressRepo = vendorAddressRepo;
            _eventPublisher = eventPublisher;
        }

        public void DeleteVendorAddress(VendorAddress vendorAddress)
        {
            if (vendorAddress == null)
                throw new ArgumentNullException();

            _vendorAddressRepo.Delete(vendorAddress);

            _eventPublisher.EntityDeleted(vendorAddress);
        }

        public VendorAddress GetVendorAddressByAddressId(int addressId)
        {
            throw new NotImplementedException();
        }

        public VendorAddress GetVendorAddressById(int id)
        {
            throw new NotImplementedException();
        }

        public VendorAddress GetVendorAddressByVendorId(int vendorId)
        {
            return _vendorAddressRepo.Table.SingleOrDefault(va => va.VendorId == vendorId && va.AddressType == AddressType.Address);
        }

        public void InsertVendorAddress(VendorAddress vendorAddress)
        {
            if (vendorAddress == null)
                throw new ArgumentNullException();

            _vendorAddressRepo.Insert(vendorAddress);

            _eventPublisher.EntityInserted(vendorAddress);
        }

        public void UpdateVendorAddress(VendorAddress vendorAddress)
        {
            if (vendorAddress == null)
                throw new ArgumentNullException();

            _vendorAddressRepo.Update(vendorAddress);

            _eventPublisher.EntityUpdated(vendorAddress);
        }
    }
}
