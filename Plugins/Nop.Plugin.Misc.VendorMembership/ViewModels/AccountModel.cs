using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.VendorMembership.ViewModels
{
    public class AccountModel
    {
        public string Name { get; set; }
        public string AttentionTo { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public string VacationMode { get; set; }
        public DateTime VacationEndsAt { get; set; }
    }
}
