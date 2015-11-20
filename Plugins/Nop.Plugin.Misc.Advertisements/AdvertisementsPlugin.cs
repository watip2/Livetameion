using Nop.Core.Data;
using Nop.Core.Domain.Messages;
using Nop.Core.Events;
using Nop.Core.Plugins;
using Nop.Services.Configuration;
using Nop.Services.Events;
using Nop.Web.Framework.Menu;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.Routing;
using Nop.Services.Localization;
using Nop.Plugin.Misc.Advertisements.Infrastructure;
using Nop.Plugin.Misc.Advertisements.Models;
using Nop.Plugin.Misc.Advertisements.Helpers;

namespace Nop.Plugin.Misc.Advertisements
{
    public class AdvertisementsPlugin : BasePlugin, IAdminMenuPlugin
    {
        private IRepository<Advertisement> _adsRepo;
        private AdvertisementsContext _adsContext;

        public AdvertisementsPlugin(
            IRepository<Advertisement> adsRepo,
            AdvertisementsContext adsContext
        )
        {
            _adsRepo = adsRepo;
            _adsContext = adsContext;
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
                SystemName = "Misc.Advertisements",
                Title = "Advertisements",
                Url = "/VendorAds/Index",
                ControllerName = "VendorAds",
                ActionName = "Index",
                Visible = true,
                RouteValues = new RouteValueDictionary() { { "area", null } },
            };

            var ManageAds = new SiteMapNode()
            {
                SystemName = "Misc.Advertisements",
                Title = "Manage Advertisements",
                Url = "/VendorAds/Index",
                ControllerName = "VendorAds",
                ActionName = "Index",
                Visible = true,
                RouteValues = new RouteValueDictionary() { { "area", null } },
            };

            var AddNew = new SiteMapNode()
            {
                SystemName = "Misc.Advertisements",
                Title = "Add New",
                Url = "/VendorAds/AddNew",
                ControllerName = "VendorAds",
                ActionName = "AddNew",
                Visible = true,
                RouteValues = new RouteValueDictionary() { { "area", null } },
            };

            List<SiteMapNode> SubMenus = new List<SiteMapNode>();
            SubMenus.Add(ManageAds);
            SubMenus.Add(AddNew);
            RootMenu.ChildNodes = SubMenus;

            var pluginNode = rootNode.ChildNodes.FirstOrDefault(x => x.SystemName == "Misc.Advertisements");
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
                _adsContext.Install();
            }
            catch (Exception e) { }
            base.Install();
        }

        public override void Uninstall()
        {
            try
            {
                _adsContext.Uninstall();
            }
            catch (Exception e) { }
            base.Uninstall();
        }
    }
}
