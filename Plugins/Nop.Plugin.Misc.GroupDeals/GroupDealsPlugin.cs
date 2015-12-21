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
using Nop.Services.Localization;

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
                //_groupDealsContext.Install();
            }
            catch (Exception) {  }

            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.GroupDeals.Fields.Required", "This is a required field.");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.GroupDeals.Fields.NameLabel", "GroupDeal Name");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.GroupDeals.Fields.NameLabel.Hint", "Please provide name.");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.GroupDeals.Fields.CountryLabel", "Country");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.GroupDeals.Fields.CountryLabel.Hint", "Please provide country.");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.GroupDeals.Fields.StateOrProvinceLabel", "State/Province");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.GroupDeals.Fields.StateOrProvinceLabelHint", "Please provide state/province");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.GroupDeals.Fields.CityLabel", "City");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.GroupDeals.Fields.CityLabelHint", "Please provide city.");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.GroupDeals.Fields.FinePrintLabel", "Fine Print");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.GroupDeals.Fields.FinePrintLabelHint", "Please provide fine print.");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.GroupDeals.Fields.StatusLabel", "Status");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.GroupDeals.Fields.StatusLabelHint", "Please provide status.");

            base.Install();
        }

        public override void Uninstall()
        {
            try
            {
                //_groupDealsContext.Uninstall();
            }
            catch (Exception) { }

            this.DeletePluginLocaleResource("Plugins.Widgets.GroupDeals.Fields.Required");
            this.DeletePluginLocaleResource("Plugins.Widgets.GroupDeals.Fields.NameLabel");
            this.DeletePluginLocaleResource("Plugins.Widgets.GroupDeals.Fields.NameLabel.Hint");
            this.DeletePluginLocaleResource("Plugins.Widgets.GroupDeals.Fields.CountryLabel");
            this.DeletePluginLocaleResource("Plugins.Widgets.GroupDeals.Fields.CountryLabel.Hint");
            this.DeletePluginLocaleResource("Plugins.Widgets.GroupDeals.Fields.StateOrProvinceLabel");
            this.DeletePluginLocaleResource("Plugins.Widgets.GroupDeals.Fields.StateOrProvinceLabelHint");
            this.DeletePluginLocaleResource("Plugins.Widgets.GroupDeals.Fields.CityLabel");
            this.DeletePluginLocaleResource("Plugins.Widgets.GroupDeals.Fields.CityLabelHint");
            this.DeletePluginLocaleResource("Plugins.Widgets.GroupDeals.Fields.FinePrintLabel");
            this.DeletePluginLocaleResource("Plugins.Widgets.GroupDeals.Fields.FinePrintLabelHint");
            this.DeletePluginLocaleResource("Plugins.Widgets.GroupDeals.Fields.StatusLabel");
            this.DeletePluginLocaleResource("Plugins.Widgets.GroupDeals.Fields.StatusLabelHint");

            base.Uninstall();
        }

        public override PluginDescriptor PluginDescriptor { get; set; }
    }
}
