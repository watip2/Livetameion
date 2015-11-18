using Nop.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.TaxGroups.Models
{
    public class MemberGroup : BaseEntity
    {
        public string Name { get; set; }
        public int TaxClassId { get; set; }

        [ForeignKey("TaxClassId")]
        public TaxClass TaxClass { get; set; }
    }
}
