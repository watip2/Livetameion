using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.TaxGroups.Helpers
{
    public class PluginHelper
    {
        public static bool IsPluginInstalled()
        {
            var pluginFinder = Nop.Core.Infrastructure.EngineContext.Current.Resolve<Nop.Core.Plugins.IPluginFinder>();

            // check plugin is installed
            var pluginDescriptor = pluginFinder.GetPluginDescriptorBySystemName("Misc.TaxGroups");

            return (pluginDescriptor != null);
        }

        public static string GetTableName<T>(DbContext context) where T : class
        {
            string sql = ((IObjectContextAdapter)context).ObjectContext.CreateObjectSet<T>().ToTraceString();
            Regex regex = new Regex("FROM (?<table>.*) AS");
            Match match = regex.Match(sql);
            string tableName = match.Groups["table"].Value;
            return tableName;
        }
    }
}
