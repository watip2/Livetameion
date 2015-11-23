﻿using Nop.Plugin.Tameion.SupportTicketSystem.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Tameion.SupportTicketSystem.Infrastructure
{
    public class SupportTicketResponseMap : EntityTypeConfiguration<SupportTicketResponse>
    {
        public SupportTicketResponseMap()
        {
            // the Install() method of ObjectContext class will create table with the following name
            // this table name is overwritten by the name of navigational property in parent class
            ToTable("SupportTicketResponses");
            HasKey(st => st.Id);

            Property(st => st.Message);
            Property(st => st.CreatedOnUtc);
            Property(st => st.CustomerId);
            Property(st => st.SupportTicketTopicId);
        }
    }
}