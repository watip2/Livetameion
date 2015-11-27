using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Tameion.BridgePay.Models
{
    public enum TransactionMode
    {
        [Display(Name = "Authorize")]
        Authorize = 1,
        [Display(Name = "Authorize and Capture")]
        AuthorizeAndCapture = 2
    }
}
