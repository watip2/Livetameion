using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Vendors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Core.Domain.ExtendedModels
{
    public class VendorBusinessType : BaseEntity
    {
        public int VendorId { get; set; }
        public int BusinessTypeId { get; set; }
        public virtual Category BusinessType { get; set; }
        public virtual Vendor Vendor { get; set; }
    }
}