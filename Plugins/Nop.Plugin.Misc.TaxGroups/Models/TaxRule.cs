using Nop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.TaxGroups.Models
{
    public class TaxRule : BaseEntity
    {
        public string Name { get; set; }

        //public ICollection<CustomerTaxClass> CustomerTaxClasses { get; set; }
        //public ICollection<VendorTaxClass> VendorTaxClasses { get; set; }
        //public ICollection<ProductTaxClass> ProductTaxClasses { get; set; }
    }
}
