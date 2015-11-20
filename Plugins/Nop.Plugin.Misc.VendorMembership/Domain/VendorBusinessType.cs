using Nop.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.VendorMembership.Domain
{
    public class VendorBusinessType : BaseEntity
    {
        public int VendorId { get; set; }
        public int BusinessTypeId { get; set; }

        // this navigational property creates plural table Vendors
        [ForeignKey("VendorId")]
        public virtual Nop.Core.Domain.Vendors.Vendor Vendor { get; set; }
        // this navigational property creates plural table BusinessTypes
        [ForeignKey("BusinessTypeId")]
        public virtual Nop.Core.Domain.Catalog.Category BusinessType { get; set; }
    }
}
