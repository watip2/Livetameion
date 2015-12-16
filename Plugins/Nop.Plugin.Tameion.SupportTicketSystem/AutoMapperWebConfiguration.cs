using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Plugin.Tameion.SupportTicketSystem.DomainModels;
using Nop.Plugin.Tameion.SupportTicketSystem.ViewModels;

namespace Nop.Plugin.Tameion.SupportTicketSystem
{
    public static class AutoMapperWebConfiguration
    {
        public static void Configure()
        {
            AutoMapper.Mapper.CreateMap<Ticket, TicketModel>().ForMember(x => x.CreatedOn, y => y.MapFrom(z => z.CreatedOnUtc));
            AutoMapper.Mapper.CreateMap<TicketModel, Ticket>().ForMember(x => x.CreatedOnUtc, y => y.MapFrom(z => z.CreatedOn));
            AutoMapper.Mapper.CreateMap<IEnumerable<Ticket>, TicketsListModel>().ForMember(x => x.Tickets, y => y.MapFrom(z => z));
        }
    }
}
