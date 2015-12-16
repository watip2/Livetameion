using Nop.Plugin.Misc.VendorMembership.ViewModels;
using Nop.Services.Configuration;
using Nop.Web.Framework.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nop.Plugin.Misc.VendorMembership.Areas.Admin.Controllers
{
    public class MultitenancyController : BasePluginController
    {
        private readonly ISettingService _settings;

        public MultitenancyController(ISettingService settings)
        {
            _settings = settings;
        }

        [HttpGet]
        public ActionResult Settings()
        {
            var model = new SettingsModel();
            model.CommissionPercentage = _settings.GetSettingByKey<int>("CommissionPercentage");
            return View(model);
        }

        [HttpPost]
        public ActionResult Settings(SettingsModel model)
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
