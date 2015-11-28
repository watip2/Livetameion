using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Nop.Services.Payments;
using Nop.Web.Framework.Controllers;
using Nop.Core;
using Nop.Services.Stores;
using Nop.Services.Configuration;
using Nop.Services.Localization;
using Nop.Plugin.Tameion.BridgePay.Models;
using System.Linq;

namespace Nop.Plugin.Tameion.BridgePay.Controller
{
    public class BridgePayController : BasePaymentController
    {
        private readonly IWorkContext _workContext;
        private readonly IStoreService _storeService;
        private readonly ISettingService _settingService;
        private readonly ILocalizationService _localizationService;

        public BridgePayController(IWorkContext workContext,
            IStoreService storeService,
            ISettingService settingService,
            ILocalizationService localizationService)
        {
            this._workContext = workContext;
            this._storeService = storeService;
            this._settingService = settingService;
            this._localizationService = localizationService;
        }

        [AdminAuthorize]
        [ChildActionOnly]
        public ActionResult Configure()
        {
            //load settings for a chosen store scope
            var storeScope = this.GetActiveStoreScopeConfiguration(_storeService, _workContext);
            var authorizeNetPaymentSettings = _settingService.LoadSetting<BridgePaySettings>(storeScope);

            var model = new ConfigurationModel();
            model.UseSandbox = authorizeNetPaymentSettings.UseSandbox;
            model.TransactModeId = Convert.ToInt32(authorizeNetPaymentSettings.TransactMode);
            model.TransactionKey = authorizeNetPaymentSettings.TransactionKey;
            model.Username = authorizeNetPaymentSettings.Username;
            model.Password = authorizeNetPaymentSettings.Password;
            model.MerchantKey = authorizeNetPaymentSettings.MerchantKey;
            model.AdditionalFee = authorizeNetPaymentSettings.AdditionalFee;
            model.AdditionalFeePercentage = authorizeNetPaymentSettings.AdditionalFeePercentage;
            //model.TransactModeValues = authorizeNetPaymentSettings.TransactMode.ToSelectList();

            model.ActiveStoreScopeConfiguration = storeScope;
            if (storeScope > 0)
            {
                model.UseSandbox_OverrideForStore = _settingService.SettingExists(authorizeNetPaymentSettings, x => x.UseSandbox, storeScope);
                model.TransactModeId_OverrideForStore = _settingService.SettingExists(authorizeNetPaymentSettings, x => x.TransactMode, storeScope);
                model.TransactionKey_OverrideForStore = _settingService.SettingExists(authorizeNetPaymentSettings, x => x.TransactionKey, storeScope);
                model.Username_OverrideForStore = _settingService.SettingExists(authorizeNetPaymentSettings, x => x.Username, storeScope);
                model.Password_OverrideForStore = _settingService.SettingExists(authorizeNetPaymentSettings, x => x.Password, storeScope);
                model.MerchantKey_OverrideForStore = _settingService.SettingExists(authorizeNetPaymentSettings, x => x.MerchantKey, storeScope);
                model.AdditionalFee_OverrideForStore = _settingService.SettingExists(authorizeNetPaymentSettings, x => x.AdditionalFee, storeScope);
                model.AdditionalFeePercentage_OverrideForStore = _settingService.SettingExists(authorizeNetPaymentSettings, x => x.AdditionalFeePercentage, storeScope);
            }

            return View("~/Plugins/Tameion.BridgePay/Views/BridgePay/Configure.cshtml", model);
        }

        [HttpPost]
        [AdminAuthorize]
        [ChildActionOnly]
        public ActionResult Configure(ConfigurationModel model)
        {
            if (!ModelState.IsValid)
                return Configure();

            //load settings for a chosen store scope
            var storeScope = this.GetActiveStoreScopeConfiguration(_storeService, _workContext);
            var bridgePaySettings = _settingService.LoadSetting<BridgePaySettings>(storeScope);

            //save settings
            bridgePaySettings.UseSandbox = model.UseSandbox;
            bridgePaySettings.TransactMode = (TransactionMode)model.TransactModeId;
            bridgePaySettings.TransactionKey = model.TransactionKey;
            bridgePaySettings.Username = model.Username;
            bridgePaySettings.Password = model.Password;
            bridgePaySettings.MerchantKey = model.MerchantKey;
            bridgePaySettings.AdditionalFee = model.AdditionalFee;
            bridgePaySettings.AdditionalFeePercentage = model.AdditionalFeePercentage;

            /* We do not clear cache after each setting update.
             * This behavior can increase performance because cached settings will not be cleared 
             * and loaded from database after each update */
            if (model.UseSandbox_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(bridgePaySettings, x => x.UseSandbox, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(bridgePaySettings, x => x.UseSandbox, storeScope);

            //if (model.TransactModeId_OverrideForStore || storeScope == 0)
            //    _settingService.SaveSetting(bridgePaySettings, x => x.TransactMode, storeScope, false);
            //else if (storeScope > 0)
            //    _settingService.DeleteSetting(bridgePaySettings, x => x.TransactMode, storeScope);

            if (model.TransactionKey_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(bridgePaySettings, x => x.TransactionKey, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(bridgePaySettings, x => x.TransactionKey, storeScope);

            if (model.Username_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(bridgePaySettings, x => x.Username, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(bridgePaySettings, x => x.Username, storeScope);

            if (model.Password_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(bridgePaySettings, x => x.Password, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(bridgePaySettings, x => x.Password, storeScope);

            if (model.MerchantKey_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(bridgePaySettings, x => x.MerchantKey, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(bridgePaySettings, x => x.MerchantKey, storeScope);

            if (model.AdditionalFee_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(bridgePaySettings, x => x.AdditionalFee, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(bridgePaySettings, x => x.AdditionalFee, storeScope);

            if (model.AdditionalFeePercentage_OverrideForStore || storeScope == 0)
                _settingService.SaveSetting(bridgePaySettings, x => x.AdditionalFeePercentage, storeScope, false);
            else if (storeScope > 0)
                _settingService.DeleteSetting(bridgePaySettings, x => x.AdditionalFeePercentage, storeScope);

            //now clear settings cache
            _settingService.ClearCache();

            SuccessNotification(_localizationService.GetResource("Admin.Plugins.Saved"));

            return Configure();
        }

        [NonAction]
        public override ProcessPaymentRequest GetPaymentInfo(FormCollection form)
        {
            var paymentInfo = new ProcessPaymentRequest();
            return paymentInfo;
        }

        [NonAction]
        public override IList<string> ValidatePaymentForm(FormCollection form)
        {
            //var warnings = new List(); return warnings;
            throw new NotImplementedException();
        }

        [ChildActionOnly]
        public ActionResult PaymentInfo()
        {
            var model = new PaymentInfoModel();

            //CC types
            model.CreditCardTypes.Add(new SelectListItem
            {
                Text = "Visa",
                Value = "Visa",
            });
            model.CreditCardTypes.Add(new SelectListItem
            {
                Text = "Master card",
                Value = "MasterCard",
            });
            model.CreditCardTypes.Add(new SelectListItem
            {
                Text = "Discover",
                Value = "Discover",
            });
            model.CreditCardTypes.Add(new SelectListItem
            {
                Text = "Amex",
                Value = "Amex",
            });

            //years
            for (int i = 0; i < 15; i++)
            {
                string year = Convert.ToString(DateTime.Now.Year + i);
                model.ExpireYears.Add(new SelectListItem
                {
                    Text = year,
                    Value = year,
                });
            }

            //months
            for (int i = 1; i <= 12; i++)
            {
                string text = (i < 10) ? "0" + i : i.ToString();
                model.ExpireMonths.Add(new SelectListItem
                {
                    Text = text,
                    Value = i.ToString(),
                });
            }

            //set postback values
            var form = this.Request.Form;
            model.CardholderName = form["CardholderName"];
            model.CardNumber = form["CardNumber"];
            model.CardCode = form["CardCode"];
            var selectedCcType = model.CreditCardTypes.FirstOrDefault(x => x.Value.Equals(form["CreditCardType"], StringComparison.InvariantCultureIgnoreCase));
            if (selectedCcType != null)
                selectedCcType.Selected = true;
            var selectedMonth = model.ExpireMonths.FirstOrDefault(x => x.Value.Equals(form["ExpireMonth"], StringComparison.InvariantCultureIgnoreCase));
            if (selectedMonth != null)
                selectedMonth.Selected = true;
            var selectedYear = model.ExpireYears.FirstOrDefault(x => x.Value.Equals(form["ExpireYear"], StringComparison.InvariantCultureIgnoreCase));
            if (selectedYear != null)
                selectedYear.Selected = true;

            return View("~/Plugins/Tameion.BridgePay/Views/BridgePay/PaymentInfo.cshtml", model);
        }

    }
}
