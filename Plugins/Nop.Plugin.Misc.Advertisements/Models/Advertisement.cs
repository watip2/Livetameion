using Nop.Core;
using Nop.Core.Domain.Vendors;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.Advertisements.Models
{
    public class Advertisement : BaseEntity
    {
        public string Name { get; set; }
        public int VendorId { get; set; }
    }
}
