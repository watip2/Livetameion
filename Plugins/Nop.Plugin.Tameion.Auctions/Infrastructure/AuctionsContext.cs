using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Discounts;
using Nop.Data;
using Nop.Data.Mapping.Catalog;
using Nop.Data.Mapping.Discounts;
using Nop.Plugin.Tameion.Auctions.DomainModels;
using Nop.Plugin.Tameion.Auctions.Helpers;
using Nop.Plugin.Tameion.Auctions.Maps;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Nop.Plugin.Tameion.Auctions.Infrastructure
{
    public class AuctionsContext : DbContext, IDbContext
    {
        public AuctionsContext(string nameOrConnectionString) : base(nameOrConnectionString) { }

        #region Implementation of IDbContext

        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            /*
             * The purpose of adding the following mappings is to find the table name in database for data access,
             * OR create table with name defined in map file. For example, if we put the following line:
             * modelBuilder.Configurations.Add(new Nop.Data.Mapping.Vendors.VendorMap());
             * then the context will find OR create the Vendor table in database (and this table name is used by NOP
             * by default). If we don't put the above line, then the context will find the Vendors (plural) table,
             * which is then defined by context itself.
             * It means, if we want to use the default table names defined by NOP, for a core entity, then add it's
             * core map class to this context like we did below for vendor
             */

            modelBuilder.Configurations.Add(new AuctionMap());
            modelBuilder.Configurations.Add(new BidMap());
            modelBuilder.Configurations.Add(new Nop.Data.Mapping.Vendors.VendorMap());
            modelBuilder.Configurations.Add(new Nop.Data.Mapping.Catalog.CategoryMap());
            modelBuilder.Configurations.Add(new Nop.Data.Mapping.Media.PictureMap());
            
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
            modelBuilder.Ignore<Nop.Core.Domain.Orders.OrderNote>();
            modelBuilder.Ignore<Nop.Core.Domain.Orders.OrderItem>();
            modelBuilder.Ignore<Nop.Core.Domain.Vendors.Vendor>();
            modelBuilder.Ignore<Nop.Core.Domain.Catalog.Category>();
            modelBuilder.Ignore<Nop.Core.Domain.Shipping.Warehouse>();
            modelBuilder.Ignore<Nop.Core.Domain.Directory.StateProvince>();
            modelBuilder.Ignore<Nop.Core.Domain.Shipping.Shipment>();
            modelBuilder.Ignore<Nop.Core.Domain.Shipping.ShipmentItem>();
            modelBuilder.Ignore<Nop.Core.Domain.Media.Picture>();
            modelBuilder.Ignore<Core.Domain.Catalog.ProductAttributeMapping>();

            modelBuilder.Configurations.Add(new ProductAttributeMappingMap());
            modelBuilder.Ignore<Core.Domain.Catalog.ProductAttributeMapping>();

            modelBuilder.Configurations.Add(new ProductCategoryMap());
            modelBuilder.Ignore<Core.Domain.Catalog.ProductCategory>();

            modelBuilder.Configurations.Add(new ProductManufacturerMap());
            modelBuilder.Ignore<ProductManufacturer>();

            modelBuilder.Configurations.Add(new ProductPictureMap());
            modelBuilder.Ignore<ProductPicture>();

            modelBuilder.Configurations.Add(new ProductReviewMap());
            modelBuilder.Ignore<ProductReview>();

            modelBuilder.Configurations.Add(new ProductSpecificationAttributeMap());
            modelBuilder.Ignore<ProductSpecificationAttribute>();

            modelBuilder.Configurations.Add(new ProductTagMap());
            modelBuilder.Ignore<ProductTag>();

            modelBuilder.Configurations.Add(new ProductAttributeCombinationMap());
            modelBuilder.Ignore<ProductAttributeCombination>();

            modelBuilder.Configurations.Add(new TierPriceMap());
            modelBuilder.Ignore<TierPrice>();

            modelBuilder.Configurations.Add(new DiscountMap());
            modelBuilder.Ignore<Discount>();

            modelBuilder.Configurations.Add(new ProductWarehouseInventoryMap());
            modelBuilder.Ignore<ProductWarehouseInventory>();
            
            //modelBuilder.Entity<GroupDealProduct>().Ignore(gdp =>gdp.ProductAttributeMappings);

            base.OnModelCreating(modelBuilder);
        }

        public string CreateDatabaseInstallationScript()
        {
            return ((IObjectContextAdapter)this).ObjectContext.CreateDatabaseScript();
        }

        public void Install()
        {
            Database.SetInitializer<AuctionsContext>(null);
            Database.ExecuteSqlCommand(CreateDatabaseInstallationScript());
            SaveChanges();
        }

        public void Uninstall()
        {
            var dbScript = "DROP TABLE " + PluginHelper.GetTableName<Bid>(this);
            Database.ExecuteSqlCommand(dbScript);

            dbScript = "DROP TABLE " + PluginHelper.GetTableName<Auction>(this);
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
