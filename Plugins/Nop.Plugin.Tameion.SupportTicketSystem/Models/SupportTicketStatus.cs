using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Tameion.SupportTicketSystem.Models
{
    public enum SupportTicketStatus
    {
        [Display(Name = "Open")]
        Open,
        [Display(Name = "Solved")]
        Solved,
        [Display(Name = "Closed")]
        Closed
    }

    //    After you create a support ticket, it can have one of the following statuses, depending on the work done on it:
    //•New
    //•In progress
    //•Pending your response
    //•Solved
    //•Closed

    //The following process explains how a ticket moves through these statuses:
    //1.As soon as a Rackspace Cloud customer opens a ticket, it has a status of New.
    //2.After a ticket is reviewed by the Rackspace Cloud support team, it moves to the In Progress or Solved status.
    //3.If the support team needs more information from the customer to fix the issue, the ticket might move to the Pending your response status.
    //4.After a ticket has a status of Solved, a customer can move the ticket to Closed or reopen it, which changes the status to In progress.
    //5.If a customer does not explicitly close or reopen a ticket with a Solved status, the ticket automatically goes to the Closed status in seven days.

}
