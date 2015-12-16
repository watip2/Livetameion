using Nop.Core.Plugins;
using Nop.Web.Framework.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Tameion.SelectAndSell
{
    public class PluginConfig : BasePlugin, IAdminMenuPlugin
    {
        public override PluginDescriptor PluginDescriptor { get; set; }
        
        public void ManageSiteMap(SiteMapNode rootNode)
        {
            //// if plugin is not installed, run database creation script
            //if (!PluginHelper.IsPluginInstalled())
            //{
            //    this.Install();
            //}

            //var RootMenu = new SiteMapNode()
            //{
            //    SystemName = "Misc.GroupDeals",
            //    Title = "Group Deals",
            //    Visible = true,
            //    ChildNodes = new List<SiteMapNode>
            //    {
            //        new SiteMapNode()
            //        {
            //            SystemName = "Misc.GroupDeals",
            //            Title = "Manage Group Deals",
            //            Url = "/Groupdeals/Index",
            //            ControllerName = "Groupdeals",
            //            ActionName = "Index",
            //            Visible = true,
            //            RouteValues = new RouteValueDictionary() { { "area", "Admin" } },
            //        },

            //        new SiteMapNode()
            //        {
            //            SystemName = "Misc.GroupDeals",
            //            Title = "Add New",
            //            Url = "/Groupdeals/Create",
            //            ControllerName = "Groupdeals",
            //            ActionName = "Create",
            //            Visible = true,
            //            RouteValues = new RouteValueDictionary() { { "area", "Admin" } },
            //        }
            //    }
            //};

            //var pluginNode = rootNode.ChildNodes.FirstOrDefault(x => x.SystemName == "Misc.GroupDeals");
            //if (pluginNode != null)
            //    pluginNode.ChildNodes.Add(RootMenu);
            //else
            //    rootNode.ChildNodes.Add(RootMenu);
        }

        public override void Install()
        {
            try
            {
                //_auctionsContext.Install();
            }
            catch (Exception) { }
            base.Install();
        }

        public override void Uninstall()
        {
            try
            {
                //_auctionsContext.Uninstall();
            }
            catch (Exception) { }
            base.Uninstall();
        }
    }
}
