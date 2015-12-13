using Nop.Plugin.Tameion.SupportTicketSystem.DomainModels;
using System.Data.Entity.ModelConfiguration;

namespace Nop.Plugin.Tameion.SupportTicketSystem.Infrastructure
{
    public class ReplyMap : EntityTypeConfiguration<Reply>
    {
        public ReplyMap()
        {
            // the Install() method of ObjectContext class will create table with the following name
            // this table name is overwritten by the name of navigational property in parent class
            ToTable("SupportTicketReplies");
            HasKey(st => st.Id);

            Property(st => st.Message);
            Property(st => st.CreatedOnUtc);
            Property(st => st.CustomerId);
            Property(st => st.TicketId);
        }
    }
}
