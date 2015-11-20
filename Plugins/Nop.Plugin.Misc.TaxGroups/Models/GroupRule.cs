using Nop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.TaxGroups.Models
{
    public class GroupRule : BaseEntity
    {
        public string Name { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public DateTime UpdatedOnUtc { get; set; }
        public bool Deleted { get; set; }
        public bool Active { get; set; }
        public int DisplayOrder { get; set; }
        public string SeName { get; set; }
    }
}
