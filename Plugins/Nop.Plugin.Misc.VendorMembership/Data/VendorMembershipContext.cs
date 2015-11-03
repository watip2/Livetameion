using Nop.Core;
using Nop.Data;
using Nop.Plugin.Misc.VendorMembership.Data;
using Nop.Plugin.Misc.VendorMembership.Domain;
using Nop.Plugin.Misc.VendorMembership.Helpers;
using Nop.Plugin.Misc.VendorMembership.Mapping;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.VendorMembership.Data
{
    public class VendorMembershipContext : DbContext, IDbContext
    {
        public VendorMembershipContext(string nameOrConnectionString) : base(nameOrConnectionString) { }

        #region Implementation of IDbContext

        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new VendorrrMap());
            modelBuilder.Configurations.Add(new PayoutMethodMap());
            modelBuilder.Configurations.Add(new Nop.Data.Mapping.Vendors.VendorMap());
            modelBuilder.Configurations.Add(new VendorPayoutMethodMap());
            modelBuilder.Configurations.Add(new VendorBusinessTypeMap());
            modelBuilder.Configurations.Add(new Nop.Data.Mapping.Catalog.CategoryMap());

            // We cannot VendorMap core model, so defining the foreignkey here
            //modelBuilder.Entity<Nop.Core.Domain.Vendors.Vendor>()
            //    .HasMany<VendorPayoutMethod>(v => v.VendorPayoutMethods)
            //    .WithRequired(vpm => vpm.Vendor)
            //    .HasForeignKey(vpm => vpm.VendorId).WillCascadeOnDelete(true);

            /*
             * Unable to determine the principal end of an association between the types
             * 'Nop.Core.Domain.Customers.RewardPointsHistory' and 'Nop.Core.Domain.Orders.Order'.
             * The principal end of this association must be explicitly configured using either
             * the relationship fluent API or data annotations
             * To avoid above error, use one of the following in OnModelCreating() method:
             * modelBuilder.Ignore<Nop.Core.Domain.Customers.RewardPointsHistory>();
             * OR
             * modelBuilder.Entity<Nop.Core.Domain.Customers.RewardPointsHistory>().HasRequired(p => p.UsedWithOrder).WithOptional();
             */
            
            modelBuilder.Ignore<Nop.Core.Domain.Customers.RewardPointsHistory>();
            modelBuilder.Ignore<Nop.Core.Domain.Common.Address>();
            modelBuilder.Ignore<Nop.Core.Domain.Customers.Customer>();
            modelBuilder.Ignore<Nop.Core.Domain.Customers.CustomerRole>();
            modelBuilder.Ignore<Nop.Core.Domain.Discounts.Discount>();
            modelBuilder.Ignore<Nop.Core.Domain.Discounts.DiscountUsageHistory>();
            modelBuilder.Ignore<Nop.Core.Domain.Orders.GiftCard>();
            modelBuilder.Ignore<Nop.Core.Domain.Orders.GiftCardUsageHistory>();
            modelBuilder.Ignore<Nop.Core.Domain.Catalog.Manufacturer>();
            modelBuilder.Ignore<Nop.Core.Domain.Orders.Order>();

            if (!PluginHelper.IsPluginInstalled())
            {
                //modelBuilder.Ignore<Nop.Core.Domain.Vendors.Vendor>();
                //modelBuilder.Ignore<Nop.Core.Domain.Catalog.Category>();
            }
            
            base.OnModelCreating(modelBuilder);
        }

        public string CreateDatabaseInstallationScript()
        {
            return ((IObjectContextAdapter)this).ObjectContext.CreateDatabaseScript();
        }

        public void Install()
        {
            //It's required to set initializer to null (for SQL Server Compact).
            //otherwise, you'll get something like "The model backing the 'your context name' context has changed since the database was created. Consider using Code First Migrations to update the database"
            Database.SetInitializer<VendorMembershipContext>(null);
            Database.ExecuteSqlCommand(CreateDatabaseInstallationScript());

            /* plugin's context is not creating foreign key reference to Vendor core model (Vendor.Id)
             * So, we are creating manually
             */
            this.Database.ExecuteSqlCommand(
                "ALTER TABLE [dbo].[VendorPayoutMethods]  WITH CHECK ADD  CONSTRAINT [Vendor_VendorPayoutMethods] FOREIGN KEY([VendorId])" +
                "REFERENCES [dbo].[Vendor] ([Id])" +
                "ON DELETE CASCADE"
            );

            SaveChanges();
        }

        public void Uninstall()
        {
            var dbScript = "DROP TABLE VendorPayoutMethods";
            Database.ExecuteSqlCommand(dbScript);

            dbScript = "DROP TABLE VendorBusinessTypes";
            Database.ExecuteSqlCommand(dbScript);

            dbScript = "DROP TABLE PayoutMethods";
            Database.ExecuteSqlCommand(dbScript);

            dbScript = "DROP TABLE Vendorrr";
            Database.ExecuteSqlCommand(dbScript);

            SaveChanges();
        }

        public new IDbSet<TEntity> Set<TEntity>() where TEntity : BaseEntity
        {
            return base.Set<TEntity>();
        }

        public System.Collections.Generic.IList<TEntity> ExecuteStoredProcedureList<TEntity>(string commandText, params object[] parameters) where TEntity : BaseEntity, new()
        {
            throw new System.NotImplementedException();
        }
        
        public System.Collections.Generic.IEnumerable<TElement> SqlQuery<TElement>(string sql, params object[] parameters)
        {
            throw new System.NotImplementedException();
        }

        public int ExecuteSqlCommand(string sql, bool doNotEnsureTransaction = false, int? timeout = null, params object[] parameters)
        {
            throw new System.NotImplementedException();
        }

        public bool AutoDetectChangesEnabled
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Detach(object entity)
        {
            throw new NotImplementedException();
        }

        public bool ProxyCreationEnabled
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
    }
}
