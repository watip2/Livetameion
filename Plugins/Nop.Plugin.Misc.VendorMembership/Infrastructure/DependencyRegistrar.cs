using Autofac;
using Autofac.Core;
using Nop.Core.Data;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Data;
using Nop.Web.Framework.Mvc;
using Nop.Plugin.Misc.VendorMembership.Data;
using Nop.Plugin.Misc.VendorMembership.Domain;
using Nop.Plugin.Misc.VendorMembership.Services;

namespace Nop.Plugin.Misc.VendorMembership.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        private const string VENDOR_MEMBERSHIP_CONTEXT_NAME = "nop_object_context_product_view_vendor_membership";
        
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
            this.RegisterPluginDataContext<VendorMembershipContext>(builder, VENDOR_MEMBERSHIP_CONTEXT_NAME);
            
            //override required repository with our custom context
            builder.RegisterType<EfRepository<Vendorrr>>()
                .As<IRepository<Vendorrr>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>(VENDOR_MEMBERSHIP_CONTEXT_NAME))
                .InstancePerLifetimeScope();

            //override required repository with our custom context
            builder.RegisterType<EfRepository<PayoutMethod>>()
                .As<IRepository<PayoutMethod>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>(VENDOR_MEMBERSHIP_CONTEXT_NAME))
                .InstancePerLifetimeScope();

            //override required repository with our custom context
            builder.RegisterType<EfRepository<VendorPayoutMethod>>()
                .As<IRepository<VendorPayoutMethod>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>(VENDOR_MEMBERSHIP_CONTEXT_NAME))
                .InstancePerLifetimeScope();

            //override required repository with our custom context
            builder.RegisterType<EfRepository<VendorBusinessType>>()
                .As<IRepository<VendorBusinessType>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>(VENDOR_MEMBERSHIP_CONTEXT_NAME))
                .InstancePerLifetimeScope();

            //override required repository with our custom context
            builder.RegisterType<EfRepository<Invoice>>()
                .As<IRepository<Invoice>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>(VENDOR_MEMBERSHIP_CONTEXT_NAME))
                .InstancePerLifetimeScope();

            //override required repository with our custom context
            builder.RegisterType<EfRepository<VendorAddress>>()
                .As<IRepository<VendorAddress>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>(VENDOR_MEMBERSHIP_CONTEXT_NAME))
                .InstancePerLifetimeScope();

            ///////////////////////////////////////////////////////////////////////////////////////////////

            // services
            builder.RegisterType<IndVendorService>().As<IIndVendorService>().InstancePerLifetimeScope();
            builder.RegisterType<InvoiceService>().As<IInvoiceService>().InstancePerLifetimeScope();
            builder.RegisterType<MtProductService>().As<IMtProductService>().InstancePerLifetimeScope();
            builder.RegisterType<VendorAddressService>().As<IVendorAddressService>().InstancePerLifetimeScope();
            ///////////////////////////////////////////////////////////////////////////////////////////////
        }

        public int Order
        {
            get { return 1; }
        }
    }
}
