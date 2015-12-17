using Nop.Plugin.Misc.VendorMembership.ViewModels;
using Nop.Services.Configuration;
using Nop.Web.Framework.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nop.Plugin.Misc.VendorMembership.Controllers
{
    [AdminAuthorize]
    public class SettingsController : BasePluginController
    {
        private readonly ISettingService _settings;

        public SettingsController(ISettingService settings)
        {
            _settings = settings;
        }

        [HttpGet]
        public ActionResult Index()
        {
            var model = new SettingsModel();
            model.CommissionPercentage = _settings.GetSettingByKey<int>("CommissionPercentage");
            return View(model);
        }

        [HttpPost]
        public ActionResult Index(SettingsModel model)
        {
            if (ModelState.IsValid)
            {
                _settings.SetSetting<int>("CommissionPercentage", model.CommissionPercentage);
                RedirectToAction("Index");
            }

            return View(model);
        }
    }
}
