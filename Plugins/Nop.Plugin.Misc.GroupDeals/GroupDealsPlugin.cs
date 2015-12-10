using Nop.Core.Data;
using Nop.Core.Plugins;
using Nop.Web.Framework.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Routing;
using Nop.Plugin.Misc.GroupDeals.Models;
using Nop.Plugin.Misc.GroupDeals.DataAccess;
using Nop.Plugin.Misc.GroupDeals.Helpers;

namespace Nop.Plugin.Misc.GroupDeals
{
    public class GroupDealsPlugin : BasePlugin, IAdminMenuPlugin
    {
        private IRepository<GroupDeal> _groupDealsRepo;
        private GroupDealsContext _groupDealsContext;
        
        public GroupDealsPlugin(
            IRepository<GroupDeal> groupDealsRepo,
            GroupDealsContext groupDealsContext
        )
        {
            _groupDealsRepo = groupDealsRepo;
            _groupDealsContext = groupDealsContext;
        }

        public void ManageSiteMap(SiteMapNode rootNode)
        {
            // if plugin is not installed, run database creation script
            if (!PluginHelper.IsPluginInstalled())
            {
                this.Install();
            }

            var RootMenu = new SiteMapNode()
            {
                SystemName = "Misc.GroupDeals",
                Title = "Group Deals",
                Visible = true,
                ChildNodes = new List<SiteMapNode>
                {
                    new SiteMapNode()
                    {
                        SystemName = "Misc.GroupDeals",
                        Title = "Manage Group Deals",
                        Url = "/Groupdeals/Index",
                        ControllerName = "Groupdeals",
                        ActionName = "Index",
                        Visible = true,
                        RouteValues = new RouteValueDictionary() { { "area", "Admin" } },
                    },

                    new SiteMapNode()
                    {
                        SystemName = "Misc.GroupDeals",
                        Title = "Add New",
                        Url = "/Groupdeals/Create",
                        ControllerName = "Groupdeals",
                        ActionName = "Create",
                        Visible = true,
                        RouteValues = new RouteValueDictionary() { { "area", "Admin" } },
                    }
                }
            };

            var pluginNode = rootNode.ChildNodes.FirstOrDefault(x => x.SystemName == "Misc.GroupDeals");
            if (pluginNode != null)
                pluginNode.ChildNodes.Add(RootMenu);
            else
                rootNode.ChildNodes.Add(RootMenu);
        }

        public bool Authenticate()
        {
            return true;
        }

        public Web.Framework.Menu.SiteMapNode BuildMenuItem()
        {
            SiteMapNode node = new SiteMapNode { Visible = true, Title = "Group Deals", Url = "/Groupdeals/Index" };
            return node;
        }

        public override void Install()
        {
            try
            {
                _groupDealsContext.Install();
            }
            catch (Exception) {  }
            base.Install();
        }

        public override void Uninstall()
        {
            try
            {
                _groupDealsContext.Uninstall();
            }
            catch (Exception) { }
            base.Uninstall();
        }

        public override PluginDescriptor PluginDescriptor { get; set; }
    }
}
