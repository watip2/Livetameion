using Nop.Core.Data;
using Nop.Core.Plugins;
using Nop.Plugin.Misc.TaxGroups.Helpers;
using Nop.Plugin.Misc.TaxGroups.Infrastructure;
using Nop.Plugin.Misc.TaxGroups.Models;
using Nop.Web.Framework.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Routing;

namespace Nop.Plugin.Misc.TaxGroups
{
    public class TaxGroupsPlugin : BasePlugin, IAdminMenuPlugin
    {
        private TaxGroupsContext _taxGroupContext;
        
        public TaxGroupsPlugin(
            TaxGroupsContext taxGroupContext
        )
        {
            _taxGroupContext = taxGroupContext;
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
                SystemName = "Misc.TaxGroups",
                Title = "GroupS & Taxes",
                Visible = true,
                RouteValues = new RouteValueDictionary() { { "area", null } },
                ChildNodes = new List<SiteMapNode>
                {
                    //Group Rules menu
                    new SiteMapNode()
                    {
                        SystemName = "Misc.TaxGroups",
                        Title = "Group Rules",
                        Visible = true,
                        RouteValues = new RouteValueDictionary() { { "area", null } },
                        ChildNodes = new List<SiteMapNode> 
                        { 
                            new SiteMapNode
                            {
                                SystemName = "Misc.TaxGroups",
                                Title = "Manage Rules",
                                Url = "/GroupRules/Index",
                                ControllerName = "GroupRules",
                                ActionName = "Index",
                                Visible = true,
                                RouteValues = new RouteValueDictionary() { { "area", null } },
                            },
                            new SiteMapNode()
                            {
                                SystemName = "Misc.TaxGroups",
                                Title = "Add New Rule",
                                Url = "/GroupRules/AddNew",
                                ControllerName = "GroupRules",
                                ActionName = "AddNew",
                                Visible = true,
                                RouteValues = new RouteValueDictionary() { { "area", null } },
                            }
                        }
                    },

                    //Taxes menu
                    new SiteMapNode()
                    {
                        SystemName = "Misc.TaxGroups",
                        Title = "Taxes",
                        Visible = true,
                        RouteValues = new RouteValueDictionary() { { "area", null } },
                        ChildNodes = new List<SiteMapNode>
                        {
                            new SiteMapNode
                            {
                                SystemName = "Misc.TaxGroups",
                                Title = "Manage Tax Rules",
                                Visible = true,
                                RouteValues = new RouteValueDictionary() { { "area", null } },
                            },
                            new SiteMapNode
                            {
                                SystemName = "Misc.TaxGroups",
                                Title = "Manage Tax Zones & Rates",
                                Visible = true,
                                RouteValues = new RouteValueDictionary() { { "area", null } },
                            },
                            new SiteMapNode
                            {
                                SystemName = "Misc.TaxGroups",
                                Title = "Tax Classes",
                                Visible = true,
                                RouteValues = new RouteValueDictionary() { { "area", null } },
                            }
                        }
                    }
                }
            };

            var pluginNode = rootNode.ChildNodes.FirstOrDefault(x => x.SystemName == "Misc.TaxGroups");
            if (pluginNode != null)
                pluginNode.ChildNodes.Add(RootMenu);
            else
                rootNode.ChildNodes.Add(RootMenu);
        }

        public override Core.Plugins.PluginDescriptor PluginDescriptor { get; set; }

        public override void Install()
        {
            try
            {
                _taxGroupContext.Install();
            }
            catch (Exception e) { }
            base.Install();
        }

        public override void Uninstall()
        {
            try
            {
                _taxGroupContext.Uninstall();
            }
            catch (Exception e) { }
            base.Uninstall();
        }
    }
}
