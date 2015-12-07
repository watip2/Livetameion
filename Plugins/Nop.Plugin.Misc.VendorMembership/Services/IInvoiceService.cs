using Nop.Plugin.Misc.VendorMembership.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.VendorMembership.Services
{
    public interface IInvoiceService
    {
        void InsertInvoice(Invoice invoice);
        Invoice GetInvoiceById(int invoiceId);
        IList<Invoice> GetAllInvoices();
        IList<Invoice> GetInvoicesByVendorId();
        void UpdateInvoice(Invoice invoice);
        void DeleteInvoice(Invoice invoice);
    }
}
