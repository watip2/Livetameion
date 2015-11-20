using Nop.Core.Infrastructure;
using Nop.Plugin.Misc.Advertisements.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.Advertisements.Infrastructure
{
    public class EfStartUpTask : IStartupTask
    {
        public void Execute()
        {
            //It's required to create this class AND to set initializer to null (for SQL Server Compact).
            //otherwise, you'll get something like "The model backing the 'CONTEXT_CLASS' context has changed since the database was created. Consider using Code First Migrations to update the database"
            Database.SetInitializer<AdvertisementsContext>(null);
        }

        public int Order
        {
            //ensure that this task is run first 
            get { return 0; }
        }
    }
}
