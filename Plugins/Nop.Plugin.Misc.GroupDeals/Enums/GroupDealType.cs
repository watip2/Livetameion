using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.GroupDeals.Enums
{
    public enum GroupDealType
    {
        [Display(Name = "Virtual Group Deal")]
        VirtualGroupDeal,
        [Display(Name = "Simple Group Deal")]
        SimpleGroupDeal,
        [Display(Name = "Configurable Group Deal")]
        ConfigurableGroupDeal,
        [Display(Name = "Bundle Group Deal")]
        BundleGroupDeal
    }
}
