using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.GroupDeals.Enums
{
    public enum GroupdealStatus
    {
        [Display(Name = "Queued")]
        Queued,
        [Display(Name = "Running")]
        Running,
        [Display(Name = "Ended")]
        Ended,
        [Display(Name = "Disabled")]
        Disabled,
        [Display(Name = "Pending Approval")]
        PendingApproval
    }
}
