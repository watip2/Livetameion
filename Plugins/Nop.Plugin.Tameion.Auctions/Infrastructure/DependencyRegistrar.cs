using Autofac;
using Autofac.Core;
using Nop.Core.Data;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Data;
using Nop.Plugin.Tameion.Auctions.DomainModels;
using Nop.Plugin.Tameion.Auctions.Services;
using Nop.Web.Framework.Mvc;

namespace Nop.Plugin.Tameion.Auctions.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        private const string AUCTIONS_CONTEXT_NAME = "nop_object_context_product_view_auctions";
        
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
            this.RegisterPluginDataContext<AuctionsContext>(builder, AUCTIONS_CONTEXT_NAME);
            
            // repositories
            //override required repository with our custom context
            builder.RegisterType<EfRepository<Auction>>()
                .As<IRepository<Auction>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>(AUCTIONS_CONTEXT_NAME))
                .InstancePerLifetimeScope();

            //override required repository with our custom context
            builder.RegisterType<EfRepository<Bid>>()
                .As<IRepository<Bid>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>(AUCTIONS_CONTEXT_NAME))
                .InstancePerLifetimeScope();
            ///////////////////////////////////////////////////////////////////////////////////////////////

            // services
            builder.RegisterType<AuctionService>().As<IAuctionService>().InstancePerLifetimeScope();
            builder.RegisterType<BidService>().As<IBidService>().InstancePerLifetimeScope();
            ///////////////////////////////////////////////////////////////////////////////////////////////
        }

        public int Order
        {
            get { return 1; }
        }
    }
}
