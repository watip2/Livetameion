using Nop.Plugin.Misc.VendorMembership.Domain;
using System.Data.Entity.ModelConfiguration;

namespace Nop.Plugin.Misc.VendorMembership.Mapping
{
    public class InvoiceMap : EntityTypeConfiguration<Invoice>
    {
        public InvoiceMap()
        {
            ToTable("Invoices");
            HasKey(i => i.Id);

            Property(i => i.OrderId);
            Property(i => i.VendorId);

            Property(i => i.DeliveryMethod);
            Property(i => i.Commission);
            Property(i => i.IsCommissionPaid);
        }
    }
}
