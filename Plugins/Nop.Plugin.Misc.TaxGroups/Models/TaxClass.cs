using Nop.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.TaxGroups.Models
{
    public class TaxClass : BaseEntity
    {
        public string Name { get; set; }
        
        public ICollection<MemberGroup> MemberGroups { get; set; }
    }
}
