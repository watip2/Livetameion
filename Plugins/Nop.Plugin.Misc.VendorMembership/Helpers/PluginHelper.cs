using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.VendorMembership.Helpers
{
    public class PluginHelper
    {
        public static bool IsPluginInstalled()
        {
            var pluginFinder = Nop.Core.Infrastructure.EngineContext.Current.Resolve<Nop.Core.Plugins.IPluginFinder>();

            // check plugin is installed
            var pluginDescriptor = pluginFinder.GetPluginDescriptorBySystemName("Misc.VendorMembership");

            return (pluginDescriptor != null);
        }
    }
}
