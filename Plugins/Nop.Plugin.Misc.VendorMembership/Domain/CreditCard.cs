using Nop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.VendorMembership.Domain
{
    public class CreditCard : BaseEntity
    {
        public int CreditCardId { get; set; }
        public string Number { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
