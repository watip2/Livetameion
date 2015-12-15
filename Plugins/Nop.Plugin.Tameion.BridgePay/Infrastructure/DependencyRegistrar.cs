using Autofac;
using Autofac.Core;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Data;
using Nop.Web.Framework.Mvc;

namespace Nop.Plugin.Tameion.BridgePay.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        private const string BRIDGE_PAY_CONTEXT_NAME = "nop_object_context_product_view_bridge_pay";

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
            this.RegisterPluginDataContext<BridgePayContext>(builder, BRIDGE_PAY_CONTEXT_NAME);

            //override required repository with our custom context
            //builder.RegisterType<EfRepository<Vendorrr>>()
            //    .As<IRepository<Vendorrr>>()
            //    .WithParameter(ResolvedParameter.ForNamed<IDbContext>(BRIDGE_PAY_CONTEXT_NAME))
            //    .InstancePerLifetimeScope();
            ///////////////////////////////////////////////////////////////////////////////////////////////

            // services
            //builder.RegisterType<IndVendorService>().As<IIndVendorService>().InstancePerLifetimeScope();
            ///////////////////////////////////////////////////////////////////////////////////////////////
        }

        public int Order
        {
            get { return 1; }
        }
    }
}
