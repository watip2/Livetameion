using Nop.Core;
using Nop.Data;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Nop.Plugin.Tameion.BridgePay.Infrastructure
{
    public class BridgePayContext : DbContext, IDbContext
    {
        public BridgePayContext(string nameOrConnectionString) : base(nameOrConnectionString) { }

        #region Implementation of IDbContext

        #endregion

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            //modelBuilder.Configurations.Add(new VendorrrMap());
            //modelBuilder.Configurations.Add(new PayoutMethodMap());
            //modelBuilder.Configurations.Add(new VendorPayoutMethodMap());
            //modelBuilder.Configurations.Add(new VendorBusinessTypeMap());
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
            Database.SetInitializer<BridgePayContext>(null);
            Database.ExecuteSqlCommand(CreateDatabaseInstallationScript());
            SaveChanges();
        }

        public void Uninstall()
        {
            //var dbScript = "DROP TABLE VendorPayoutMethods";
            //Database.ExecuteSqlCommand(dbScript);

            //dbScript = "DROP TABLE VendorBusinessTypes";
            //Database.ExecuteSqlCommand(dbScript);

            //dbScript = "DROP TABLE PayoutMethods";
            //Database.ExecuteSqlCommand(dbScript);

            //dbScript = "DROP TABLE Vendorrr";
            //Database.ExecuteSqlCommand(dbScript);

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
