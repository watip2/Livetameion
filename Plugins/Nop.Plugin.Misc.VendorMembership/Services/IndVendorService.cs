using Nop.Core.Data;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Vendors;
using Nop.Services.Common;
using Nop.Services.Events;
using Nop.Services.Vendors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Nop.Plugin.Misc.VendorMembership.Services
{
    public class IndVendorService : Nop.Services.Vendors.VendorService, Nop.Plugin.Misc.VendorMembership.Services.IIndVendorService
    {
        private IRepository<Vendor> _vendorRepository;
        private IEventPublisher _eventPublisher;
        private IRepository<GenericAttribute> _genericAttributeRepo;

        public IndVendorService(IRepository<Vendor> vendorRepository,
            IEventPublisher eventPublisher,
            IRepository<GenericAttribute> genericAttributeRepo)
            : base(vendorRepository, eventPublisher)
        {
            _vendorRepository = vendorRepository;
            _eventPublisher = eventPublisher;
            _genericAttributeRepo = genericAttributeRepo;
        }

        public override void InsertVendor(Vendor vendor)
        {
            base.InsertVendor(vendor);
        }

        public override void UpdateVendor(Vendor vendor)
        {
            base.UpdateVendor(vendor);
        }

        public override Core.IPagedList<Vendor> GetAllVendors(string name = "", int pageIndex = 0, int pageSize = 2147483647, bool showHidden = false)
        {
            return base.GetAllVendors(name, pageIndex, pageSize, showHidden);
        }

        public override Vendor GetVendorById(int vendorId)
        {
            return base.GetVendorById(vendorId);
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        public override void DeleteVendor(Vendor vendor)
        {
            base.DeleteVendor(vendor);
        }

        public bool IsVendorAuthenticated(string email, string password)
        {
            var vendor = _vendorRepository.Table.SingleOrDefault(v => v.Email.Equals(email));
            return (vendor != null);
        }

        //////////////////
        //private readonly IRepository<VendorBusiness> _vendorBusinessTypeRepository;
        
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

            var genericAttribute = _genericAttributeRepo.Table.SingleOrDefault(at => at.KeyGroup.Equals("Vendor") && at.Key.Equals("PreferredSubdomainName") && at.Value.Equals(Subdomain));
            if (genericAttribute != null)
            {
                var vendorId = genericAttribute.EntityId;
                return _vendorRepository.GetById(vendorId);
            }
            
            return null;
        }

        public bool AreLoginCookiesValid(HttpCookie email, HttpCookie password)
        {
            return (
                email != null &&
                password != null &&
                !String.IsNullOrWhiteSpace(email.Value) &&
                !String.IsNullOrWhiteSpace(password.Value));
        }

		public Vendor GetVendorByEmail(string email)
		{
			return _vendorRepository.Table.SingleOrDefault(v => v.Email == email);
		}
        
        public bool IsVendorLoggedIn()
        {
            throw new NotImplementedException();
        }

        public Vendor GetLoggedInVendor()
        {
            throw new NotImplementedException();
        }
    }
}
