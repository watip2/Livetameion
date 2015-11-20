using Autofac;
using Autofac.Core;
using Nop.Core.Data;
using Nop.Core.Infrastructure;
using Nop.Core.Infrastructure.DependencyManagement;
using Nop.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nop.Web.Framework.Mvc;
using Nop.Core.Domain.Vendors;
using Nop.Plugin.Misc.Advertisements.Models;

namespace Nop.Plugin.Misc.Advertisements.Infrastructure
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        private const string ADS_CONTEXT_NAME = "nop_object_context_product_view_ads";
        
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
            this.RegisterPluginDataContext<AdvertisementsContext>(builder, ADS_CONTEXT_NAME);
            
            // repositories
            //override required repository with our custom context
            builder.RegisterType<EfRepository<Advertisement>>()
                .As<IRepository<Advertisement>>()
                .WithParameter(ResolvedParameter.ForNamed<IDbContext>(ADS_CONTEXT_NAME))
                .InstancePerLifetimeScope();
            ///////////////////////////////////////////////////////////////////////////////////////////////
            
            // services
            //builder.RegisterType<GroupDealService>().As<IGroupDealService>().InstancePerLifetimeScope();
            ///////////////////////////////////////////////////////////////////////////////////////////////
        }

        public int Order
        {
            get { return 1; }
        }
    }
}
