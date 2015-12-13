using Nop.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Tameion.SupportTicketSystem.DomainModels
{
    public abstract class SupportTicket : BaseEntity
    {
        public string Message { get; set; }
        public DateTime CreatedOnUtc { get; set; }
        public int CustomerId { get; set; }
    }
}
