using Nop.Core.Data;
using Nop.Core.Domain.Messages;
using Nop.Core.Events;
using Nop.Core.Plugins;
using Nop.Plugin.Misc.VendorMembership.Data;
using Nop.Plugin.Misc.VendorMembership.Domain;
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

namespace Nop.Plugin.Misc.VendorMembership
{
    public class VendorMembershipPlugin : BasePlugin, IAdminMenuPlugin, IConsumer<EntityDeleted<NewsLetterSubscription>>
    {
        private VendorrrObjectContext _context;
        private IRepository<Vendorrr> _vendorrrRepo;
        private ISettingService _settings;

        public VendorMembershipPlugin(VendorrrObjectContext context, IRepository<Vendorrr> vendorrrRepo, ISettingService commonSettings)
        {
            _context = context;
            _vendorrrRepo = vendorrrRepo;
            _settings = commonSettings;
        }

        public void ManageSiteMap(SiteMapNode rootNode)
        {
            // if plugin is not installed, run database creation script
            if (!this.IsPluginInstalled())
            {
                this.Install();
            }
            //var menuItem = new SiteMapNode()
            //{
            //    SystemName = "Misc.VendorMembership",
            //    Title = "Vendor Membership",
            //    Url = "/VendorMembership/Register",
            //    ControllerName = "VendorMembership",
            //    ActionName = "Register",
            //    Visible = true,
            //    RouteValues = new RouteValueDictionary() { { "area", null } },
            //};

            //var pluginNode = rootNode.ChildNodes.FirstOrDefault(x => x.SystemName == "Misc.VendorMembership");
            //if (pluginNode != null)
            //    pluginNode.ChildNodes.Add(menuItem);
            //else
            //    rootNode.ChildNodes.Add(menuItem);
        }

        public bool Authenticate()
        {
            return true;
        }

        public Web.Framework.Menu.SiteMapNode BuildMenuItem()
        {
            SiteMapNode node = new SiteMapNode { Visible = true, Title = "Vendor Membership", Url = "/VendorMembership/Register" };
            return node;
        }

        public override void Install()
        {
            _context.Install();

            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.VendorMembership.NameLabel", "Your Name");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.VendorMembership.NameLabel.Hint", "Please provide a name.");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.VendorMembership.NameRequired", "Name is required.");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.VendorMembership.EmailLabel", "Your Email");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.VendorMembership.EmailLabel.Hint", "Please provide an email address.");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.VendorMembership.EmailRequired", "Email address is required.");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.VendorMembership.EmailFormat", "Invalid email address.");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.VendorMembership.SubmitFormMessage", "Please provide a name and email.");
            this.AddOrUpdatePluginLocaleResource("Plugins.Widgets.VendorMembership.ThankYouMessage", "Thanks for your interest! Download link is below.");

            _settings.SetSetting<bool>("AutoAddEmailSubscription", false);
            base.Install();
        }

        public override void Uninstall()
        {
            _context.Uninstall();

            this.DeletePluginLocaleResource("Plugins.Widgets.VendorMembership.NameLabel");
            this.DeletePluginLocaleResource("Plugins.Widgets.VendorMembership.NameLabel.Hint");
            this.DeletePluginLocaleResource("Plugins.Widgets.VendorMembership.NameRequired");
            this.DeletePluginLocaleResource("Plugins.Widgets.VendorMembership.EmailLabel");
            this.DeletePluginLocaleResource("Plugins.Widgets.VendorMembership.EmailLabel.Hint");
            this.DeletePluginLocaleResource("Plugins.Widgets.VendorMembership.EmailRequired");
            this.DeletePluginLocaleResource("Plugins.Widgets.VendorMembership.EmailFormat");
            this.DeletePluginLocaleResource("Plugins.Widgets.VendorMembership.SubmitFormMessage");
            this.DeletePluginLocaleResource("Plugins.Widgets.VendorMembership.ThankYouMessage");

            base.Uninstall();
        }

        public override PluginDescriptor PluginDescriptor { get; set; }

        public bool IsPluginInstalled()
        {
            var pluginFinder = Nop.Core.Infrastructure.EngineContext.Current.Resolve<IPluginFinder>();

            // check plugin is installed
            var pluginDescriptor = pluginFinder.GetPluginDescriptorBySystemName("Misc.VendorMembership");

            return (pluginDescriptor != null);
        }

        public void HandleEvent(EntityDeleted<NewsLetterSubscription> eventMessage)
        {
            Vendorrr entity = _vendorrrRepo.Table.Where(x => x.Email == eventMessage.Entity.Email).FirstOrDefault();
            entity.OnMailingList = false;
            _vendorrrRepo.Update(entity);
        }
    }
}
