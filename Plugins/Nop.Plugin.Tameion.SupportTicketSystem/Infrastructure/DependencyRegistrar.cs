using Autofac;
using Autofac.Core;
using Nop.Core.Data;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Data;
using Nop.Web.Framework.Mvc;
using Nop.Plugin.Tameion.SupportTicketSystem.Infrastructure;
using Nop.Plugin.Tameion.SupportTicketSystem.DomainModels;
using Nop.Plugin.Tameion.SupportTicketSystem.Interfaces;

namespace Nop.Plugin.Misc.GroupDeals.DataAccess
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        private const string SUPPORT_TICKET_SYSTEM_CONTEXT_NAME = "nop_object_context_SUPPORT_TICKET_SYSTEM";
        
        /*
         * Don't register core models here, they should stay registered in NOP core context
         * The below Register() mehtod is called at the following moments:
         * 1. When application starts
         * 2. During plugin installation
         * 3. During plugin uninstallation
         */
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            //data context
            this.RegisterPluginDataContext<SupportTicketSystemContext>(builder, SUPPORT_TICKET_SYSTEM_CONTEXT_NAME);
            
            // repositories
            //override required repository with our custom context
            builder.RegisterType<EfRepository<SupportTicketTopic>>()
                .As<IRepository<SupportTicketTopic>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>(SUPPORT_TICKET_SYSTEM_CONTEXT_NAME))
                .InstancePerLifetimeScope();

            //override required repository with our custom context
            builder.RegisterType<EfRepository<SupportTicketResponse>>()
                .As<IRepository<SupportTicketResponse>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>(SUPPORT_TICKET_SYSTEM_CONTEXT_NAME))
                .InstancePerLifetimeScope();
            ///////////////////////////////////////////////////////////////////////////////////////////////
            
            // services
            builder.RegisterType<SupportTicketService>().As<ISupportTicketService>().InstancePerLifetimeScope();
            ///////////////////////////////////////////////////////////////////////////////////////////////
        }

        public int Order
        {
            get { return 1; }
        }
    }
}
