using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Reflection;

namespace Nop.Plugin.Misc.GroupDeals.Helpers
{
    public class PluginHelper
    {
        public static bool IsPluginInstalled()
        {
            var pluginFinder = Nop.Core.Infrastructure.EngineContext.Current.Resolve<Nop.Core.Plugins.IPluginFinder>();

            // check plugin is installed
            var pluginDescriptor = pluginFinder.GetPluginDescriptorBySystemName("Misc.GroupDeals");

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

        public static TAttribute GetAttribute<TAttribute>(Enum enumValue)
            where TAttribute : Attribute
        {
            return enumValue.GetType()
                .GetMember(enumValue.ToString())
                .First()
                .GetCustomAttribute<TAttribute>();
        }
    }
}
