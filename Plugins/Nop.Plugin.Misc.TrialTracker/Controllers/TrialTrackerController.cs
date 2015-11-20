using Nop.Core.Data;
using Nop.Core.Domain.Messages;
using Nop.Plugin.Misc.TrialTracker.Domain;
using Nop.Services.Catalog;
using Nop.Services.Configuration;
using Nop.Services.Messages;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Kendoui;
using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nop.Plugin.Misc.TrialTracker.Controllers
{
    public class TrialTrackerController : BasePluginController
    {
        private IRepository<TrialTrackerRecord> _trialRepo;
        private IProductService _productService;
        private ISettingService _settings;
        private INewsLetterSubscriptionService _mailingService;

        public TrialTrackerController(IRepository<TrialTrackerRecord> trialRepo, IProductService productService, ISettingService settingService, INewsLetterSubscriptionService mailingService)
        {
            _trialRepo = trialRepo;
            _productService = productService;
            _settings = settingService;
            _mailingService = mailingService;
        }

        [Authorize]
        public ActionResult Manage()
        {
            return View();
        }

        public ActionResult NewEntry()
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewEntry([Bind(Include="CustomerName,CustomerEmail,ProductId")]TrialTrackerRecord vm)
        {
            if (ModelState.IsValid)
            {
                var product = _productService.GetProductById(vm.ProductId);
                
                vm.ProductName = product.Name;
                vm.DownloadDate = DateTime.Now.ToString("MM/dd/yyyy");

                // check if they should be auto added to mailing list
                if (_settings.GetSettingByKey<bool>("AutoAddTrialEmail"))
                {
                    vm.OnMailingList = true;
                    NewsLetterSubscription subscriber = new NewsLetterSubscription
                    {
                        Active = true,
                        CreatedOnUtc = DateTime.Now,
                        Email = vm.CustomerEmail
                    };
                    _mailingService.InsertNewsLetterSubscription(subscriber);
                }
                else 
                {
                    vm.OnMailingList = false;
                }
                
                _trialRepo.Insert(vm);
                return View("_DownloadLink", vm);
            }
            else
            {
                return View();
            }
        }

        [HttpPost]
        public ActionResult GetTrials(DataSourceRequest command)
        {
            var trials = _trialRepo.Table.ToList();

            var gridModel = new DataSourceResult
            {
                Data = trials,
                Total = trials.Count
            };

            return Json(gridModel);
        }

        [HttpPost]
        public ActionResult UpdateTrial(TrialTrackerRecord trialUpdate)
        {
            var trial = _trialRepo.GetById(trialUpdate.TrialTrackerId);

            trial.ProductName = trialUpdate.ProductName;
            trial.CustomerName = trialUpdate.CustomerName;
            trial.CustomerEmail = trialUpdate.CustomerEmail;
            trial.DownloadDate = trialUpdate.DownloadDate;
            trial.OnMailingList = trialUpdate.OnMailingList;

            if (trial.OnMailingList)
            {
                NewsLetterSubscription subscriber = new NewsLetterSubscription
                {
                    Active = true,
                    CreatedOnUtc = DateTime.Now,
                    Email = trial.CustomerEmail
                };
                _mailingService.InsertNewsLetterSubscription(subscriber);
            }
            else
            {
                NewsLetterSubscription deleteSubscriber = _mailingService.GetAllNewsLetterSubscriptions().Where(x => x.Email == trial.CustomerEmail).FirstOrDefault();
                if (deleteSubscriber != null)
                {
                    _mailingService.DeleteNewsLetterSubscription(deleteSubscriber);
                }
            }

            _trialRepo.Update(trial);

            return new NullJsonResult();
        }

        [HttpPost]
        public ActionResult DeleteTrial(DataSourceRequest command, int TrialTrackerId)
        {
            var trial = _trialRepo.GetById(TrialTrackerId);
            _trialRepo.Delete(trial);

            return new NullJsonResult();
        }

        [HttpPost]
        public ActionResult AutoAdd(string add)
        {
            if (add == "true")
            {
                _settings.SetSetting<bool>("AutoAddTrialEmail", true);
            }
            else 
            {
                _settings.SetSetting<bool>("AutoAddTrialEmail", false);
            }

            return Content("success");
        }
    }
}
