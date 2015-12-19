using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Forums;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Media;
using Nop.Core.Domain.Messages;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Security;
using Nop.Core.Domain.Tax;
using Nop.Services.Authentication;
using Nop.Services.Authentication.External;
using Nop.Services.Common;
using Nop.Services.Customers;
using Nop.Services.Directory;
using Nop.Services.Helpers;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Media;
using Nop.Services.Messages;
using Nop.Services.Orders;
using Nop.Services.Seo;
using Nop.Services.Stores;
using Nop.Services.Tax;
using Nop.Web.Extensions;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Security;
using Nop.Web.Framework.Security.Captcha;
using Nop.Web.Framework.Security.Honeypot;
using Nop.Web.Models.Common;
using Nop.Web.Models.Customer;
using WebGrease.Css.Extensions;
using Nop.Web.Controllers;
using Nop.Plugin.Misc.VendorMembership.ViewModels;
using Nop.Plugin.Misc.VendorMembership.Helpers;
using Nop.Core.Infrastructure;
using Nop.Services.Catalog;
using Nop.Plugin.Misc.VendorMembership.Services;
using Nop.Core.Domain.Vendors;
using Nop.Services.Vendors;
using Nop.Plugin.Misc.VendorMembership.Domain;
using Nop.Plugin.Misc.GroupDeals.Models;
using Nop.Plugin.Misc.GroupDeals.Services;
using Nop.Plugin.Misc.VendorMembership;
using Nop.Plugin.Misc.VendorMembership.ActionFilters;
using Nop.Core.Data;
using Nop.Plugin.Misc.VendorMembership.Data;
using Nop.Plugin.Misc.VendorMembership.Extensions;

namespace Nop.Plugin.Misc.VendorMembership.Controllers
{
    public partial class AccountController : BasePublicController
    {
        #region Fields
        private readonly IAuthenticationService _authenticationService;
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly DateTimeSettings _dateTimeSettings;
        private readonly TaxSettings _taxSettings;
        private readonly ILocalizationService _localizationService;
        private readonly IWorkContext _workContext;
        private readonly IStoreContext _storeContext;
        private readonly IStoreMappingService _storeMappingService;
        private readonly ICustomerService _customerService;
        private readonly ICustomerAttributeParser _customerAttributeParser;
        private readonly ICustomerAttributeService _customerAttributeService;
        private readonly IGenericAttributeService _genericAttributeService;
        private readonly ICustomerRegistrationService _customerRegistrationService;
        private readonly ITaxService _taxService;
        private readonly RewardPointsSettings _rewardPointsSettings;
        private readonly CustomerSettings _customerSettings;
        private readonly AddressSettings _addressSettings;
        private readonly ForumSettings _forumSettings;
        private readonly OrderSettings _orderSettings;
        private readonly IAddressService _addressService;
        private readonly ICountryService _countryService;
        private readonly IStateProvinceService _stateProvinceService;
        private readonly IOrderService _orderService;
        private readonly IPictureService _pictureService;
        private readonly INewsLetterSubscriptionService _newsLetterSubscriptionService;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IOpenAuthenticationService _openAuthenticationService;
        private readonly IDownloadService _downloadService;
        private readonly IWebHelper _webHelper;
        private readonly ICustomerActivityService _customerActivityService;
        private readonly IAddressAttributeParser _addressAttributeParser;
        private readonly IAddressAttributeService _addressAttributeService;
        private readonly IAddressAttributeFormatter _addressAttributeFormatter;

        private readonly MediaSettings _mediaSettings;
        private readonly IWorkflowMessageService _workflowMessageService;
        private readonly LocalizationSettings _localizationSettings;
        private readonly CaptchaSettings _captchaSettings;
        private readonly SecuritySettings _securitySettings;
        private readonly ExternalAuthenticationSettings _externalAuthenticationSettings;
        private List<Nop.Plugin.Misc.VendorMembership.DTOs.Category> _categories;
        private readonly IIndVendorService _vendorService;
        private readonly IGroupDealService _groupDealService;
        private readonly ICategoryService _categoryService;
        private readonly IVendorAddressService _vendorAddressService;
        private readonly IProductService _productService;

        #endregion

        #region Ctor

        public AccountController(IAuthenticationService authenticationService,
            IDateTimeHelper dateTimeHelper,
            DateTimeSettings dateTimeSettings,
            TaxSettings taxSettings,
            ILocalizationService localizationService,
            IWorkContext workContext,
            IStoreContext storeContext,
            IStoreMappingService storeMappingService,
            ICustomerService customerService,
            ICustomerAttributeParser customerAttributeParser,
            ICustomerAttributeService customerAttributeService,
            IGenericAttributeService genericAttributeService,
            ICustomerRegistrationService customerRegistrationService,
            ITaxService taxService,
            RewardPointsSettings rewardPointsSettings,
            CustomerSettings customerSettings,
            AddressSettings addressSettings,
            ForumSettings forumSettings,
            OrderSettings orderSettings,
            IAddressService addressService,
            ICountryService countryService,
            IStateProvinceService stateProvinceService,
            IOrderService orderService,
            IPictureService pictureService,
            INewsLetterSubscriptionService newsLetterSubscriptionService,
            IShoppingCartService shoppingCartService,
            IOpenAuthenticationService openAuthenticationService,
            IDownloadService downloadService,
            IWebHelper webHelper,
            ICustomerActivityService customerActivityService,
            IAddressAttributeParser addressAttributeParser,
            IAddressAttributeService addressAttributeService,
            IAddressAttributeFormatter addressAttributeFormatter,
            MediaSettings mediaSettings,
            IWorkflowMessageService workflowMessageService,
            LocalizationSettings localizationSettings,
            CaptchaSettings captchaSettings,
            SecuritySettings securitySettings,
            ExternalAuthenticationSettings externalAuthenticationSettings,
            IIndVendorService vendorService,
            IGroupDealService groupDealService,
            ICategoryService categoryService,
            IVendorAddressService vendorAddressService,
            IProductService productService)
        {
            this._authenticationService = authenticationService;
            this._dateTimeHelper = dateTimeHelper;
            this._dateTimeSettings = dateTimeSettings;
            this._taxSettings = taxSettings;
            this._localizationService = localizationService;
            this._workContext = workContext;
            this._storeContext = storeContext;
            this._storeMappingService = storeMappingService;
            this._customerService = customerService;
            this._customerAttributeParser = customerAttributeParser;
            this._customerAttributeService = customerAttributeService;
            this._genericAttributeService = genericAttributeService;
            this._customerRegistrationService = customerRegistrationService;
            this._taxService = taxService;
            this._rewardPointsSettings = rewardPointsSettings;
            this._customerSettings = customerSettings;
            this._addressSettings = addressSettings;
            this._forumSettings = forumSettings;
            this._orderSettings = orderSettings;
            this._addressService = addressService;
            this._countryService = countryService;
            this._stateProvinceService = stateProvinceService;
            this._orderService = orderService;
            this._pictureService = pictureService;
            this._newsLetterSubscriptionService = newsLetterSubscriptionService;
            this._shoppingCartService = shoppingCartService;
            this._openAuthenticationService = openAuthenticationService;
            this._downloadService = downloadService;
            this._webHelper = webHelper;
            this._customerActivityService = customerActivityService;
            this._addressAttributeParser = addressAttributeParser;
            this._addressAttributeService = addressAttributeService;
            this._addressAttributeFormatter = addressAttributeFormatter;
            this._mediaSettings = mediaSettings;
            this._workflowMessageService = workflowMessageService;
            this._localizationSettings = localizationSettings;
            this._captchaSettings = captchaSettings;
            this._securitySettings = securitySettings;
            this._externalAuthenticationSettings = externalAuthenticationSettings;
            this._categories = new System.Collections.Generic.List<DTOs.Category>();
            this._vendorService = vendorService;
            this._groupDealService = groupDealService;
            this._categoryService = categoryService;
            this._vendorAddressService = vendorAddressService;
            this._productService = productService;
        }

        #endregion

        #region Utilities

        [HttpGet]
        [VendorAuthorize]
        public ActionResult Index()
        {
            if (!_workContext.CurrentVendor.HasPaidGroupDeals())
            {
                for (int i = 0; i < 10; i++)
                {
                    _groupDealService.CreateGroupDealProduct(_workContext.CurrentVendor.GetShopName() + " register", 25);
                }
                _workContext.CurrentVendor.SetHasPaidGroupDeals(true);
            }

            var accountModel = new AccountModel();
            accountModel.AttentionTo = _workContext.CurrentVendor.Name;
            accountModel.Email = _workContext.CurrentVendor.Email;
            accountModel.Password = _workContext.CurrentCustomer.Password;
            accountModel.VacationMode = null;
            accountModel.VacationEndsAt = DateTime.Parse("01/01/2017");

            // get generic attributes
            accountModel.ShopName = _workContext.CurrentVendor.GetShopName();
            
            var vendorAddress = _vendorAddressService.GetVendorAddressByVendorId(_workContext.CurrentVendor.Id);
            var address = _addressService.GetAddressById(vendorAddress.AddressId);
            accountModel.ShippingAddress.Address1 = address.Address1;
            accountModel.ShippingAddress.City = address.City;
            accountModel.ShippingAddress.ZipPostalCode = address.ZipPostalCode;
            accountModel.ShippingAddress.CountryId = address.CountryId;

            PrepareAccountModel(accountModel);

            var vendorMembershipProduct = _productService.GetProductById(88);
            _shoppingCartService.AddToCart(_workContext.CurrentCustomer, vendorMembershipProduct, ShoppingCartType.ShoppingCart, _storeContext.CurrentStore.Id);

            return View(accountModel);
        }

        [HttpPost]
        [VendorAuthorize]
        [ValidateAntiForgeryToken]
        public ActionResult Index(AccountModel model)
        {
            if (model == null)
                throw new ArgumentNullException();

            if (ModelState.IsValid)
            {
                var vendor = _vendorService.GetVendorById(_workContext.CurrentVendor.Id);
                vendor.Email = model.Email;
                vendor.Name = model.AttentionTo;
                _vendorService.UpdateVendor(vendor);

                // setting generic attributes
                vendor.SetShopName(model.ShopName);
                
                var vendorAddress = _vendorAddressService.GetVendorAddressByVendorId(vendor.Id);
                var address = _addressService.GetAddressById(vendorAddress.AddressId);
                address.Address1 = model.ShippingAddress.Address1;
                address.City = model.ShippingAddress.City;
                address.ZipPostalCode = model.ShippingAddress.ZipPostalCode;
                address.CountryId = model.ShippingAddress.CountryId;
                _addressService.UpdateAddress(address);

                RedirectToAction("Index");
            }

            PrepareAccountModel(model);
            return View(model);
        }

        protected virtual void PrepareAccountModel(AccountModel model)
        {
            //countries and states
            //if (_customerSettings.CountryEnabled)
            {
                model.AvailableCountries.Add(new SelectListItem { Text = _localizationService.GetResource("Address.SelectCountry"), Value = "0" });
                foreach (var c in _countryService.GetAllCountries())
                {
                    model.AvailableCountries.Add(new SelectListItem
                    {
                        Text = c.GetLocalized(x => x.Name),
                        Value = c.Id.ToString(),
                        Selected = c.Id == model.ShippingAddress.CountryId
                    });
                }

                //if (_customerSettings.StateProvinceEnabled)
                {
                    //states
                    if (model.ShippingAddress.CountryId != null)
                    {
                        var states = _stateProvinceService.GetStateProvincesByCountryId((int)model.ShippingAddress.CountryId).ToList();
                        if (states.Count > 0)
                        {
                            model.AvailableStates.Add(new SelectListItem { Text = _localizationService.GetResource("Address.SelectState"), Value = "0" });

                            foreach (var s in states)
                            {
                                model.AvailableStates.Add(new SelectListItem { Text = s.GetLocalized(x => x.Name), Value = s.Id.ToString(), Selected = (s.Id == model.ShippingAddress.StateProvinceId) });
                            }
                        }
                        else
                        {
                            bool anyCountrySelected = model.AvailableCountries.Any(x => x.Selected);

                            model.AvailableStates.Add(new SelectListItem
                            {
                                Text = _localizationService.GetResource(anyCountrySelected ? "Address.OtherNonUS" : "Address.SelectState"),
                                Value = "0"
                            });
                        }
                    }

                }
            }
        }
        
        [NonAction]
        protected virtual void TryAssociateAccountWithExternalAccount(Customer customer)
        {
            var parameters = ExternalAuthorizerHelper.RetrieveParametersFromRoundTrip(true);
            if (parameters == null)
                return;

            if (_openAuthenticationService.AccountExists(parameters))
                return;

            _openAuthenticationService.AssociateExternalAccountWithUser(customer, parameters);
        }

        /// <summary>
        /// Prepare custom customer attribute models
        /// </summary>
        /// <param name="customer">Customer</param>
        /// <param name="overrideAttributesXml">When specified we do not use attributes of a customer</param>
        /// <returns>A list of customer attribute models</returns>
        [NonAction]
        protected virtual IList<CustomerAttributeModel> PrepareCustomCustomerAttributes(Customer customer,
            string overrideAttributesXml = "")
        {
            if (customer == null)
                throw new ArgumentNullException("customer");

            var result = new List<CustomerAttributeModel>();

            var customerAttributes = _customerAttributeService.GetAllCustomerAttributes();
            foreach (var attribute in customerAttributes)
            {
                var attributeModel = new CustomerAttributeModel
                {
                    Id = attribute.Id,
                    Name = attribute.GetLocalized(x => x.Name),
                    IsRequired = attribute.IsRequired,
                    AttributeControlType = attribute.AttributeControlType,
                };

                if (attribute.ShouldHaveValues())
                {
                    //values
                    var attributeValues = _customerAttributeService.GetCustomerAttributeValues(attribute.Id);
                    foreach (var attributeValue in attributeValues)
                    {
                        var valueModel = new CustomerAttributeValueModel
                        {
                            Id = attributeValue.Id,
                            Name = attributeValue.GetLocalized(x => x.Name),
                            IsPreSelected = attributeValue.IsPreSelected
                        };
                        attributeModel.Values.Add(valueModel);
                    }
                }

                //set already selected attributes
                var selectedAttributesXml = !String.IsNullOrEmpty(overrideAttributesXml) ?
                    overrideAttributesXml :
                    customer.GetAttribute<string>(SystemCustomerAttributeNames.CustomCustomerAttributes, _genericAttributeService);
                switch (attribute.AttributeControlType)
                {
                    case AttributeControlType.DropdownList:
                    case AttributeControlType.RadioList:
                    case AttributeControlType.Checkboxes:
                        {
                            if (!String.IsNullOrEmpty(selectedAttributesXml))
                            {
                                //clear default selection
                                foreach (var item in attributeModel.Values)
                                    item.IsPreSelected = false;

                                //select new values
                                var selectedValues = _customerAttributeParser.ParseCustomerAttributeValues(selectedAttributesXml);
                                foreach (var attributeValue in selectedValues)
                                    foreach (var item in attributeModel.Values)
                                        if (attributeValue.Id == item.Id)
                                            item.IsPreSelected = true;
                            }
                        }
                        break;
                    case AttributeControlType.ReadonlyCheckboxes:
                        {
                            //do nothing
                            //values are already pre-set
                        }
                        break;
                    case AttributeControlType.TextBox:
                    case AttributeControlType.MultilineTextbox:
                        {
                            if (!String.IsNullOrEmpty(selectedAttributesXml))
                            {
                                var enteredText = _customerAttributeParser.ParseValues(selectedAttributesXml, attribute.Id);
                                if (enteredText.Count > 0)
                                    attributeModel.DefaultValue = enteredText[0];
                            }
                        }
                        break;
                    case AttributeControlType.ColorSquares:
                    case AttributeControlType.Datepicker:
                    case AttributeControlType.FileUpload:
                    default:
                        //not supported attribute control types
                        break;
                }

                result.Add(attributeModel);
            }


            return result;
        }

        [NonAction]
        protected virtual void PrepareCustomerInfoModel(CustomerInfoModel model, Customer customer,
            bool excludeProperties, string overrideCustomCustomerAttributesXml = "")
        {
            if (model == null)
                throw new ArgumentNullException("model");

            if (customer == null)
                throw new ArgumentNullException("customer");

            model.AllowCustomersToSetTimeZone = _dateTimeSettings.AllowCustomersToSetTimeZone;
            foreach (var tzi in _dateTimeHelper.GetSystemTimeZones())
                model.AvailableTimeZones.Add(new SelectListItem { Text = tzi.DisplayName, Value = tzi.Id, Selected = (excludeProperties ? tzi.Id == model.TimeZoneId : tzi.Id == _dateTimeHelper.CurrentTimeZone.Id) });

            if (!excludeProperties)
            {
                model.VatNumber = customer.GetAttribute<string>(SystemCustomerAttributeNames.VatNumber);
                model.FirstName = customer.GetAttribute<string>(SystemCustomerAttributeNames.FirstName);
                model.LastName = customer.GetAttribute<string>(SystemCustomerAttributeNames.LastName);
                model.Gender = customer.GetAttribute<string>(SystemCustomerAttributeNames.Gender);
                var dateOfBirth = customer.GetAttribute<DateTime?>(SystemCustomerAttributeNames.DateOfBirth);
                if (dateOfBirth.HasValue)
                {
                    model.DateOfBirthDay = dateOfBirth.Value.Day;
                    model.DateOfBirthMonth = dateOfBirth.Value.Month;
                    model.DateOfBirthYear = dateOfBirth.Value.Year;
                }
                model.Company = customer.GetAttribute<string>(SystemCustomerAttributeNames.Company);
                model.StreetAddress = customer.GetAttribute<string>(SystemCustomerAttributeNames.StreetAddress);
                model.StreetAddress2 = customer.GetAttribute<string>(SystemCustomerAttributeNames.StreetAddress2);
                model.ZipPostalCode = customer.GetAttribute<string>(SystemCustomerAttributeNames.ZipPostalCode);
                model.City = customer.GetAttribute<string>(SystemCustomerAttributeNames.City);
                model.CountryId = customer.GetAttribute<int>(SystemCustomerAttributeNames.CountryId);
                model.StateProvinceId = customer.GetAttribute<int>(SystemCustomerAttributeNames.StateProvinceId);
                model.Phone = customer.GetAttribute<string>(SystemCustomerAttributeNames.Phone);
                model.Fax = customer.GetAttribute<string>(SystemCustomerAttributeNames.Fax);

                //newsletter
                var newsletter = _newsLetterSubscriptionService.GetNewsLetterSubscriptionByEmailAndStoreId(customer.Email, _storeContext.CurrentStore.Id);
                model.Newsletter = newsletter != null && newsletter.Active;

                model.Signature = customer.GetAttribute<string>(SystemCustomerAttributeNames.Signature);

                model.Email = customer.Email;
                model.Username = customer.Username;
            }
            else
            {
                if (_customerSettings.UsernamesEnabled && !_customerSettings.AllowUsersToChangeUsernames)
                    model.Username = customer.Username;
            }

            //countries and states
            if (_customerSettings.CountryEnabled)
            {
                model.AvailableCountries.Add(new SelectListItem { Text = _localizationService.GetResource("Address.SelectCountry"), Value = "0" });
                foreach (var c in _countryService.GetAllCountries())
                {
                    model.AvailableCountries.Add(new SelectListItem
                    {
                        Text = c.GetLocalized(x => x.Name),
                        Value = c.Id.ToString(),
                        Selected = c.Id == model.CountryId
                    });
                }

                if (_customerSettings.StateProvinceEnabled)
                {
                    //states
                    var states = _stateProvinceService.GetStateProvincesByCountryId(model.CountryId).ToList();
                    if (states.Count > 0)
                    {
                        model.AvailableStates.Add(new SelectListItem { Text = _localizationService.GetResource("Address.SelectState"), Value = "0" });

                        foreach (var s in states)
                        {
                            model.AvailableStates.Add(new SelectListItem { Text = s.GetLocalized(x => x.Name), Value = s.Id.ToString(), Selected = (s.Id == model.StateProvinceId) });
                        }
                    }
                    else
                    {
                        bool anyCountrySelected = model.AvailableCountries.Any(x => x.Selected);

                        model.AvailableStates.Add(new SelectListItem
                        {
                            Text = _localizationService.GetResource(anyCountrySelected ? "Address.OtherNonUS" : "Address.SelectState"),
                            Value = "0"
                        });
                    }

                }
            }
            model.DisplayVatNumber = _taxSettings.EuVatEnabled;
            model.VatNumberStatusNote = ((VatNumberStatus)customer.GetAttribute<int>(SystemCustomerAttributeNames.VatNumberStatusId))
                .GetLocalizedEnum(_localizationService, _workContext);
            model.GenderEnabled = _customerSettings.GenderEnabled;
            model.DateOfBirthEnabled = _customerSettings.DateOfBirthEnabled;
            model.DateOfBirthRequired = _customerSettings.DateOfBirthRequired;
            model.CompanyEnabled = _customerSettings.CompanyEnabled;
            model.CompanyRequired = _customerSettings.CompanyRequired;
            model.StreetAddressEnabled = _customerSettings.StreetAddressEnabled;
            model.StreetAddressRequired = _customerSettings.StreetAddressRequired;
            model.StreetAddress2Enabled = _customerSettings.StreetAddress2Enabled;
            model.StreetAddress2Required = _customerSettings.StreetAddress2Required;
            model.ZipPostalCodeEnabled = _customerSettings.ZipPostalCodeEnabled;
            model.ZipPostalCodeRequired = _customerSettings.ZipPostalCodeRequired;
            model.CityEnabled = _customerSettings.CityEnabled;
            model.CityRequired = _customerSettings.CityRequired;
            model.CountryEnabled = _customerSettings.CountryEnabled;
            model.CountryRequired = _customerSettings.CountryRequired;
            model.StateProvinceEnabled = _customerSettings.StateProvinceEnabled;
            model.StateProvinceRequired = _customerSettings.StateProvinceRequired;
            model.PhoneEnabled = _customerSettings.PhoneEnabled;
            model.PhoneRequired = _customerSettings.PhoneRequired;
            model.FaxEnabled = _customerSettings.FaxEnabled;
            model.FaxRequired = _customerSettings.FaxRequired;
            model.NewsletterEnabled = _customerSettings.NewsletterEnabled;
            model.UsernamesEnabled = _customerSettings.UsernamesEnabled;
            model.AllowUsersToChangeUsernames = _customerSettings.AllowUsersToChangeUsernames;
            model.CheckUsernameAvailabilityEnabled = _customerSettings.CheckUsernameAvailabilityEnabled;
            model.SignatureEnabled = _forumSettings.ForumsEnabled && _forumSettings.SignaturesEnabled;

            //external authentication
            model.NumberOfExternalAuthenticationProviders = _openAuthenticationService
                .LoadActiveExternalAuthenticationMethods(_storeContext.CurrentStore.Id)
                .Count;
            foreach (var ear in _openAuthenticationService.GetExternalIdentifiersFor(customer))
            {
                var authMethod = _openAuthenticationService.LoadExternalAuthenticationMethodBySystemName(ear.ProviderSystemName);
                if (authMethod == null || !authMethod.IsMethodActive(_externalAuthenticationSettings))
                    continue;

                model.AssociatedExternalAuthRecords.Add(new CustomerInfoModel.AssociatedExternalAuthModel
                {
                    Id = ear.Id,
                    Email = ear.Email,
                    ExternalIdentifier = ear.ExternalIdentifier,
                    AuthMethodName = authMethod.GetLocalizedFriendlyName(_localizationService, _workContext.WorkingLanguage.Id)
                });
            }

            //custom customer attributes
            var customAttributes = PrepareCustomCustomerAttributes(customer, overrideCustomCustomerAttributesXml);
            customAttributes.ForEach(model.CustomerAttributes.Add);
        }

        [NonAction]
        protected virtual void PrepareCustomerRegisterModel(RegisterModel model, bool excludeProperties,
            string overrideCustomCustomerAttributesXml = "")
        {
            if (model == null)
                throw new ArgumentNullException("model");

            model.AllowCustomersToSetTimeZone = _dateTimeSettings.AllowCustomersToSetTimeZone;
            foreach (var tzi in _dateTimeHelper.GetSystemTimeZones())
                model.AvailableTimeZones.Add(new SelectListItem { Text = tzi.DisplayName, Value = tzi.Id, Selected = (excludeProperties ? tzi.Id == model.TimeZoneId : tzi.Id == _dateTimeHelper.CurrentTimeZone.Id) });

            model.DisplayVatNumber = _taxSettings.EuVatEnabled;
            //form fields
            model.GenderEnabled = _customerSettings.GenderEnabled;
            model.DateOfBirthEnabled = _customerSettings.DateOfBirthEnabled;
            model.DateOfBirthRequired = _customerSettings.DateOfBirthRequired;
            model.CompanyEnabled = _customerSettings.CompanyEnabled;
            model.CompanyRequired = _customerSettings.CompanyRequired;
            model.StreetAddressEnabled = _customerSettings.StreetAddressEnabled;
            model.StreetAddressRequired = _customerSettings.StreetAddressRequired;
            model.StreetAddress2Enabled = _customerSettings.StreetAddress2Enabled;
            model.StreetAddress2Required = _customerSettings.StreetAddress2Required;
            model.ZipPostalCodeEnabled = _customerSettings.ZipPostalCodeEnabled;
            model.ZipPostalCodeRequired = _customerSettings.ZipPostalCodeRequired;
            model.CityEnabled = _customerSettings.CityEnabled;
            model.CityRequired = _customerSettings.CityRequired;
            model.CountryEnabled = _customerSettings.CountryEnabled;
            model.CountryRequired = _customerSettings.CountryRequired;
            model.StateProvinceEnabled = _customerSettings.StateProvinceEnabled;
            model.StateProvinceRequired = _customerSettings.StateProvinceRequired;
            model.PhoneEnabled = _customerSettings.PhoneEnabled;
            model.PhoneRequired = _customerSettings.PhoneRequired;
            model.FaxEnabled = _customerSettings.FaxEnabled;
            model.FaxRequired = _customerSettings.FaxRequired;
            model.NewsletterEnabled = _customerSettings.NewsletterEnabled;
            model.AcceptPrivacyPolicyEnabled = _customerSettings.AcceptPrivacyPolicyEnabled;
            model.UsernamesEnabled = _customerSettings.UsernamesEnabled;
            model.CheckUsernameAvailabilityEnabled = _customerSettings.CheckUsernameAvailabilityEnabled;
            model.HoneypotEnabled = _securitySettings.HoneypotEnabled;
            model.DisplayCaptcha = _captchaSettings.Enabled && _captchaSettings.ShowOnRegistrationPage;

            //countries and states
            if (_customerSettings.CountryEnabled)
            {
                model.AvailableCountries.Add(new SelectListItem { Text = _localizationService.GetResource("Address.SelectCountry"), Value = "0" });

                foreach (var c in _countryService.GetAllCountries())
                {
                    model.AvailableCountries.Add(new SelectListItem
                    {
                        Text = c.GetLocalized(x => x.Name),
                        Value = c.Id.ToString(),
                        Selected = c.Id == model.CountryId
                    });
                }

                if (_customerSettings.StateProvinceEnabled)
                {
                    //states
                    var states = _stateProvinceService.GetStateProvincesByCountryId(model.CountryId).ToList();
                    if (states.Count > 0)
                    {
                        model.AvailableStates.Add(new SelectListItem { Text = _localizationService.GetResource("Address.SelectState"), Value = "0" });

                        foreach (var s in states)
                        {
                            model.AvailableStates.Add(new SelectListItem { Text = s.GetLocalized(x => x.Name), Value = s.Id.ToString(), Selected = (s.Id == model.StateProvinceId) });
                        }
                    }
                    else
                    {
                        bool anyCountrySelected = model.AvailableCountries.Any(x => x.Selected);

                        model.AvailableStates.Add(new SelectListItem
                        {
                            Text = _localizationService.GetResource(anyCountrySelected ? "Address.OtherNonUS" : "Address.SelectState"),
                            Value = "0"
                        });
                    }

                }
            }

            //custom customer attributes
            var customAttributes = PrepareCustomCustomerAttributes(_workContext.CurrentCustomer, overrideCustomCustomerAttributesXml);
            customAttributes.ForEach(model.CustomerAttributes.Add);
        }

        [NonAction]
        protected virtual string ParseCustomCustomerAttributes(FormCollection form)
        {
            if (form == null)
                throw new ArgumentNullException("form");

            string attributesXml = "";
            var attributes = _customerAttributeService.GetAllCustomerAttributes();
            foreach (var attribute in attributes)
            {
                string controlId = string.Format("customer_attribute_{0}", attribute.Id);
                switch (attribute.AttributeControlType)
                {
                    case AttributeControlType.DropdownList:
                    case AttributeControlType.RadioList:
                        {
                            var ctrlAttributes = form[controlId];
                            if (!String.IsNullOrEmpty(ctrlAttributes))
                            {
                                int selectedAttributeId = int.Parse(ctrlAttributes);
                                if (selectedAttributeId > 0)
                                    attributesXml = _customerAttributeParser.AddCustomerAttribute(attributesXml,
                                        attribute, selectedAttributeId.ToString());
                            }
                        }
                        break;
                    case AttributeControlType.Checkboxes:
                        {
                            var cblAttributes = form[controlId];
                            if (!String.IsNullOrEmpty(cblAttributes))
                            {
                                foreach (var item in cblAttributes.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                                {
                                    int selectedAttributeId = int.Parse(item);
                                    if (selectedAttributeId > 0)
                                        attributesXml = _customerAttributeParser.AddCustomerAttribute(attributesXml,
                                            attribute, selectedAttributeId.ToString());
                                }
                            }
                        }
                        break;
                    case AttributeControlType.ReadonlyCheckboxes:
                        {
                            //load read-only (already server-side selected) values
                            var attributeValues = _customerAttributeService.GetCustomerAttributeValues(attribute.Id);
                            foreach (var selectedAttributeId in attributeValues
                                .Where(v => v.IsPreSelected)
                                .Select(v => v.Id)
                                .ToList())
                            {
                                attributesXml = _customerAttributeParser.AddCustomerAttribute(attributesXml,
                                            attribute, selectedAttributeId.ToString());
                            }
                        }
                        break;
                    case AttributeControlType.TextBox:
                    case AttributeControlType.MultilineTextbox:
                        {
                            var ctrlAttributes = form[controlId];
                            if (!String.IsNullOrEmpty(ctrlAttributes))
                            {
                                string enteredText = ctrlAttributes.Trim();
                                attributesXml = _customerAttributeParser.AddCustomerAttribute(attributesXml,
                                    attribute, enteredText);
                            }
                        }
                        break;
                    case AttributeControlType.Datepicker:
                    case AttributeControlType.ColorSquares:
                    case AttributeControlType.FileUpload:
                    //not supported customer attributes
                    default:
                        break;
                }
            }

            return attributesXml;
        }

        #endregion

        #region Login / logout
        
        [HttpGet]
        public ActionResult Login()
        {
            return RedirectToAction("Login", "Customer");
        }

        [HttpPost]
        [CaptchaValidator]
        public ActionResult Login(LoginModel model, string returnUrl, bool captchaValid)
        {
            //validate CAPTCHA
            if (_captchaSettings.Enabled && _captchaSettings.ShowOnLoginPage && !captchaValid)
            {
                ModelState.AddModelError("", _localizationService.GetResource("Common.WrongCaptcha"));
            }

            if (ModelState.IsValid)
            {
                if (_customerSettings.UsernamesEnabled && model.Username != null)
                {
                    model.Username = model.Username.Trim();
                }
                var loginResult = _customerRegistrationService.ValidateCustomer(_customerSettings.UsernamesEnabled ? model.Username : model.Email, model.Password);
                switch (loginResult)
                {
                    case CustomerLoginResults.Successful:
                        {
                            var customer = _customerSettings.UsernamesEnabled ? _customerService.GetCustomerByUsername(model.Username) : _customerService.GetCustomerByEmail(model.Email);

                            //migrate shopping cart
                            _shoppingCartService.MigrateShoppingCart(_workContext.CurrentCustomer, customer, true);

                            //sign in new customer
                            _authenticationService.SignIn(customer, model.RememberMe);

                            //activity log
                            _customerActivityService.InsertActivity("PublicStore.Login", _localizationService.GetResource("ActivityLog.PublicStore.Login"), customer);

                            if (String.IsNullOrEmpty(returnUrl) || !Url.IsLocalUrl(returnUrl))
                                return RedirectToRoute("HomePage");

                            return Redirect(returnUrl);
                        }
                    case CustomerLoginResults.CustomerNotExist:
                        ModelState.AddModelError("", _localizationService.GetResource("Account.Login.WrongCredentials.CustomerNotExist"));
                        break;
                    case CustomerLoginResults.Deleted:
                        ModelState.AddModelError("", _localizationService.GetResource("Account.Login.WrongCredentials.Deleted"));
                        break;
                    case CustomerLoginResults.NotActive:
                        ModelState.AddModelError("", _localizationService.GetResource("Account.Login.WrongCredentials.NotActive"));
                        break;
                    case CustomerLoginResults.NotRegistered:
                        ModelState.AddModelError("", _localizationService.GetResource("Account.Login.WrongCredentials.NotRegistered"));
                        break;
                    case CustomerLoginResults.WrongPassword:
                    default:
                        ModelState.AddModelError("", _localizationService.GetResource("Account.Login.WrongCredentials"));
                        break;
                }
            }

            //If we got this far, something failed, redisplay form
            model.UsernamesEnabled = _customerSettings.UsernamesEnabled;
            model.DisplayCaptcha = _captchaSettings.Enabled && _captchaSettings.ShowOnLoginPage;
            return View(model);
        }

        public ActionResult Logout()
        {
            //external authentication
            ExternalAuthorizerHelper.RemoveParameters();

            if (_workContext.OriginalCustomerIfImpersonated != null)
            {
                //logout impersonated customer
                _genericAttributeService.SaveAttribute<int?>(_workContext.OriginalCustomerIfImpersonated,
                    SystemCustomerAttributeNames.ImpersonatedCustomerId, null);
                //redirect back to customer details page (admin area)
                return this.RedirectToAction("Edit", "Customer", new { id = _workContext.CurrentCustomer.Id, area = "Admin" });

            }

            //standard logout 

            //activity log
            _customerActivityService.InsertActivity("PublicStore.Logout", _localizationService.GetResource("ActivityLog.PublicStore.Logout"));

            _authenticationService.SignOut();
            return RedirectToRoute("HomePage");
        }

        #endregion

        #region Password recovery

        [NopHttpsRequirement(SslRequirement.Yes)]
        public ActionResult PasswordRecovery()
        {
            var model = new PasswordRecoveryModel();
            return View(model);
        }

        [HttpPost, ActionName("PasswordRecovery")]
        [FormValueRequired("send-email")]
        public ActionResult PasswordRecoverySend(PasswordRecoveryModel model)
        {
            if (ModelState.IsValid)
            {
                var customer = _customerService.GetCustomerByEmail(model.Email);
                if (customer != null && customer.Active && !customer.Deleted)
                {
                    //save token and current date
                    var passwordRecoveryToken = Guid.NewGuid();
                    _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.PasswordRecoveryToken, passwordRecoveryToken.ToString());
                    DateTime? generatedDateTime = DateTime.UtcNow;
                    _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.PasswordRecoveryTokenDateGenerated, generatedDateTime);

                    //send email
                    _workflowMessageService.SendCustomerPasswordRecoveryMessage(customer, _workContext.WorkingLanguage.Id);

                    model.Result = _localizationService.GetResource("Account.PasswordRecovery.EmailHasBeenSent");
                }
                else
                {
                    model.Result = _localizationService.GetResource("Account.PasswordRecovery.EmailNotFound");
                }

                return View(model);
            }

            //If we got this far, something failed, redisplay form
            return View(model);
        }


        [NopHttpsRequirement(SslRequirement.Yes)]
        public ActionResult PasswordRecoveryConfirm(string token, string email)
        {
            var customer = _customerService.GetCustomerByEmail(email);
            if (customer == null)
                return RedirectToRoute("HomePage");

            var model = new PasswordRecoveryConfirmModel();

            //validate token
            if (!customer.IsPasswordRecoveryTokenValid(token))
            {
                model.DisablePasswordChanging = true;
                model.Result = _localizationService.GetResource("Account.PasswordRecovery.WrongToken");
            }

            //validate token expiration date
            if (customer.IsPasswordRecoveryLinkExpired(_customerSettings))
            {
                model.DisablePasswordChanging = true;
                model.Result = _localizationService.GetResource("Account.PasswordRecovery.LinkExpired");
            }

            return View(model);
        }

        [HttpPost, ActionName("PasswordRecoveryConfirm")]
        [FormValueRequired("set-password")]
        public ActionResult PasswordRecoveryConfirmPOST(string token, string email, PasswordRecoveryConfirmModel model)
        {
            var customer = _customerService.GetCustomerByEmail(email);
            if (customer == null)
                return RedirectToRoute("HomePage");

            //validate token
            if (!customer.IsPasswordRecoveryTokenValid(token))
            {
                model.DisablePasswordChanging = true;
                model.Result = _localizationService.GetResource("Account.PasswordRecovery.WrongToken");
            }

            //validate token expiration date
            if (customer.IsPasswordRecoveryLinkExpired(_customerSettings))
            {
                model.DisablePasswordChanging = true;
                model.Result = _localizationService.GetResource("Account.PasswordRecovery.LinkExpired");
                return View(model);
            }

            if (ModelState.IsValid)
            {
                var response = _customerRegistrationService.ChangePassword(new ChangePasswordRequest(email,
                    false, _customerSettings.DefaultPasswordFormat, model.NewPassword));
                if (response.Success)
                {
                    _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.PasswordRecoveryToken, "");

                    model.DisablePasswordChanging = true;
                    model.Result = _localizationService.GetResource("Account.PasswordRecovery.PasswordHasBeenChanged");
                }
                else
                {
                    model.Result = response.Errors.FirstOrDefault();
                }

                return View(model);
            }

            //If we got this far, something failed, redisplay form
            return View(model);
        }

        #endregion

        #region Register

        [HttpGet]
        //[NopHttpsRequirement(SslRequirement.Yes)]
        public ActionResult Register()
        {
            //check whether registration is allowed
            if (_customerSettings.UserRegistrationType == UserRegistrationType.Disabled)
                return RedirectToRoute("RegisterResult", new { resultId = (int)UserRegistrationType.Disabled });
            
            VendorRegisterViewModel model = new VendorRegisterViewModel();
            PrepareVendorRegisterModel(model);
            return View(model);
        }

        [NonAction]
        protected virtual void PrepareVendorRegisterModel(VendorRegisterViewModel model)
        {
            model.Countries = VendorMembershipHelper.GetCountriesNames();
            
            //countries and states
            //if (_customerSettings.CountryEnabled)
            {
                model.AvailableCountries.Add(new SelectListItem { Text = _localizationService.GetResource("Address.SelectCountry"), Value = "0" });
                foreach (var c in _countryService.GetAllCountries())
                {
                    model.AvailableCountries.Add(new SelectListItem
                    {
                        Text = c.GetLocalized(x => x.Name),
                        Value = c.Id.ToString(),
                        Selected = c.Id == model.CountryId
                    });
                }

                //if (_customerSettings.StateProvinceEnabled)
                {
                    //states
                    var states = _stateProvinceService.GetStateProvincesByCountryId(model.CountryId).ToList();
                    if (states.Count > 0)
                    {
                        model.AvailableStates.Add(new SelectListItem { Text = _localizationService.GetResource("Address.SelectState"), Value = "0" });

                        foreach (var s in states)
                        {
                            model.AvailableStates.Add(new SelectListItem { Text = s.GetLocalized(x => x.Name), Value = s.Id.ToString(), Selected = (s.Id == model.StateProvinceId) });
                        }
                    }
                    else
                    {
                        bool anyCountrySelected = model.AvailableCountries.Any(x => x.Selected);

                        model.AvailableStates.Add(new SelectListItem
                        {
                            Text = _localizationService.GetResource(anyCountrySelected ? "Address.OtherNonUS" : "Address.SelectState"),
                            Value = "0"
                        });
                    }

                }
            }

            var categoryService = EngineContext.Current.Resolve<ICategoryService>();
            var AllCategories = categoryService.GetAllCategories();

            foreach (var EachCategory in AllCategories)
            {
                var cat = new Nop.Plugin.Misc.VendorMembership.DTOs.Category();
                cat.CategoryId = EachCategory.Id;
                cat.Name = EachCategory.Name;

                if (EachCategory.ParentCategoryId != 0)
                {
                    var ParentCategory = _categories.SingleOrDefault(c => c.CategoryId.Equals(EachCategory.ParentCategoryId));
                    if (ParentCategory != null)
                    {
                        if (ParentCategory.ChildrenCategories == null)
                        {
                            ParentCategory.ChildrenCategories = new List<DTOs.Category>();
                        }
                        ParentCategory.ChildrenCategories.Add(cat);
                    }
                }
                else
                {
                    _categories.Add(cat);
                }
            }

            model.SelectedItems = new int[] { 1, 3 };
            model.Options = new List<SelectListItem>();
            foreach (var _category in _categories)
            {
                model.Options.Add(new SelectListItem { Value = _category.CategoryId.ToString(), Text = _category.Name });
                if (_category.ChildrenCategories != null && _category.ChildrenCategories.Count > 0)
                {
                    foreach (var _childCategory in _category.ChildrenCategories)
                    {
                        model.Options.Add(new SelectListItem { Value = _childCategory.CategoryId.ToString(), Text = "--" + _childCategory.Name });
                    }
                }
            }

            //return model;
        }

        [HttpPost]
        [CaptchaValidator]
        [HoneypotValidator]
        [PublicAntiForgery]
        [ValidateInput(false)]
        public ActionResult Register(VendorRegisterViewModel vrmodel, string returnUrl, bool captchaValid, FormCollection form)
        {
            if (vrmodel.CountryId == 0)
            {
                ModelState.AddModelError("CountryId", "This is a required field");
                if (vrmodel.StateProvinceId == 0)
                {
                    ModelState.AddModelError("StateProvinceId", "This is a required field");
                }                
                PrepareVendorRegisterModel(vrmodel);
                return View(vrmodel);
            }

            var dataSettingsManager = new DataSettingsManager();
            var dataProviderSettings = dataSettingsManager.LoadSettings();
            var context = new VendorMembershipContext(dataProviderSettings.DataConnectionString);
            using (var transaction = context.Database.BeginTransaction())
            {
                try
                {
                    if (ModelState.IsValid)
                    {
                        if (ValidateVendorModel(vrmodel))
                        {
                            Vendor vendor = new Vendor();
                            vendor.Name = vrmodel.AttentionTo;
                            vendor.Email = vrmodel.Email;
                            vendor.Active = true;
                            var vendorServiceCore = EngineContext.Current.Resolve<IVendorService>();
                            vendorServiceCore.InsertVendor(vendor);

                            Address address = new Address
                            {
                                City = vrmodel.City,
                                CountryId = vrmodel.CountryId,
                                StateProvinceId = vrmodel.StateProvinceId,
                                PhoneNumber = vrmodel.PhoneNumber,
                                CreatedOnUtc = DateTime.UtcNow,
                                Email = vrmodel.Email,
                                ZipPostalCode = vrmodel.ZipPostalCode,
                                Address1 = vrmodel.Address1,
                                Address2 = vrmodel.Address2
                            };
                            _addressService.InsertAddress(address);
                            _vendorAddressService.InsertVendorAddress(new VendorAddress
                            {
                                VendorId = vendor.Id,
                                AddressId = address.Id,
                                AddressType = AddressType.Address
                            });

                            _genericAttributeService.SaveAttribute(
                                vendor,
                                Nop.Plugin.Misc.VendorMembership.Domain.VendorAttributes.ShopName,
                                vrmodel.ShopName);

                            _genericAttributeService.SaveAttribute(
                                vendor,
                                Nop.Plugin.Misc.VendorMembership.Domain.VendorAttributes.Password,
                                vrmodel.Password);

                            _genericAttributeService.SaveAttribute(
                                vendor,
                                Nop.Plugin.Misc.VendorMembership.Domain.VendorAttributes.EnableGoogleAnalytics,
                                vrmodel.EnableGoogleAnalytics);

                            _genericAttributeService.SaveAttribute(
                                vendor,
                                Nop.Plugin.Misc.VendorMembership.Domain.VendorAttributes.GoogleAnalyticsAccountNumber,
                                vrmodel.GoogleAnalyticsAccountNumber);

                            _genericAttributeService.SaveAttribute(
                                vendor,
                                Nop.Plugin.Misc.VendorMembership.Domain.VendorAttributes.LogoImage,
                                vrmodel.LogoImage);

                            //_genericAttributeService.SaveAttribute(
                            //    vendor,
                            //    Nop.Plugin.Misc.VendorMembership.Domain.VendorAttributes.PreferredShippingCarrier,
                            //    model.PreferredShippingCarrier);

                            _genericAttributeService.SaveAttribute(
                                vendor,
                                Nop.Plugin.Misc.VendorMembership.Domain.VendorAttributes.PreferredSubdomainName,
                                vrmodel.PreferredSubdomainName);

                            var productServiceCore = EngineContext.Current.Resolve<Nop.Services.Catalog.IProductService>();

                            foreach (var BusinessTypeId in vrmodel.BusinessTypeIds)
                            {
                                var vb = new VendorBusinessType();
                                //var vendorServiceExt = new VendorService(_vendorBusinessTypeRepository);
                                vb.VendorId = vendor.Id;
                                vb.BusinessTypeId = BusinessTypeId;
                                //vendorServiceExt.InsertVendorBusinessType(vb);
                            }

                            // create groupdeals
                            for (int i = 0; i < 10; i++)
                            {
                                int groupDealProductId = _groupDealService.CreateGroupDealProduct(vrmodel.ShopName + " register", 25m);
                                foreach (var categoryId in vrmodel.BusinessTypeIds)
                                {
                                    var productCategory = new ProductCategory
                                    {
                                        ProductId = groupDealProductId,
                                        CategoryId = categoryId,
                                        IsFeaturedProduct = false,
                                        DisplayOrder = 0
                                    };
                                    _categoryService.InsertProductCategory(productCategory);
                                }
                            }
                            vendor.SetHasPaidGroupDeals(true);

                            var model = new RegisterModel();
                            PrepareCustomerRegisterModel(model, false);
                            //enable newsletter by default
                            model.Newsletter = _customerSettings.NewsletterTickedByDefault;
                            model = vrmodel.ToRegisterModel(model);

                            // core code starts
                            //check whether registration is allowed
                            if (_customerSettings.UserRegistrationType == UserRegistrationType.Disabled)
                                return RedirectToRoute("RegisterResult", new { resultId = (int)UserRegistrationType.Disabled });

                            if (_workContext.CurrentCustomer.IsRegistered())
                            {
                                //Already registered customer. 
                                _authenticationService.SignOut();

                                //Save a new record
                                _workContext.CurrentCustomer = _customerService.InsertGuestCustomer();
                            }
                            var customer = _workContext.CurrentCustomer;

                            //add to 'Vendors' role
                            var vendorRole = _customerService.GetCustomerRoleBySystemName(SystemCustomerRoleNames.Vendors);
                            if (vendorRole == null)
                                throw new NopException("'Vendors' role could not be loaded");
                            customer.CustomerRoles.Add(vendorRole);
                            customer.VendorId = vendor.Id;

                            //custom customer attributes
                            var customerAttributesXml = ParseCustomCustomerAttributes(form);
                            var customerAttributeWarnings = _customerAttributeParser.GetAttributeWarnings(customerAttributesXml);
                            foreach (var error in customerAttributeWarnings)
                            {
                                ModelState.AddModelError("", error);
                            }

                            //validate CAPTCHA
                            if (_captchaSettings.Enabled && _captchaSettings.ShowOnRegistrationPage && !captchaValid)
                            {
                                ModelState.AddModelError("", _localizationService.GetResource("Common.WrongCaptcha"));
                            }


                            if (_customerSettings.UsernamesEnabled && model.Username != null)
                            {
                                model.Username = model.Username.Trim();
                            }

                            bool isApproved = _customerSettings.UserRegistrationType == UserRegistrationType.Standard;
                            var registrationRequest = new CustomerRegistrationRequest(customer, model.Email,
                                _customerSettings.UsernamesEnabled ? model.Username : model.Email, model.Password, _customerSettings.DefaultPasswordFormat, isApproved);
                            var registrationResult = _customerRegistrationService.RegisterCustomer(registrationRequest);
                            if (registrationResult.Success)
                            {
                                //properties
                                if (_dateTimeSettings.AllowCustomersToSetTimeZone)
                                {
                                    _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.TimeZoneId, model.TimeZoneId);
                                }
                                //VAT number
                                if (_taxSettings.EuVatEnabled)
                                {
                                    _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.VatNumber, model.VatNumber);

                                    string vatName;
                                    string vatAddress;
                                    var vatNumberStatus = _taxService.GetVatNumberStatus(model.VatNumber, out vatName, out vatAddress);
                                    _genericAttributeService.SaveAttribute(customer,
                                        SystemCustomerAttributeNames.VatNumberStatusId,
                                        (int)vatNumberStatus);
                                    //send VAT number admin notification
                                    if (!String.IsNullOrEmpty(model.VatNumber) && _taxSettings.EuVatEmailAdminWhenNewVatSubmitted)
                                        _workflowMessageService.SendNewVatSubmittedStoreOwnerNotification(customer, model.VatNumber, vatAddress, _localizationSettings.DefaultAdminLanguageId);

                                }

                                //form fields
                                if (_customerSettings.GenderEnabled)
                                    _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.Gender, model.Gender);
                                _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.FirstName, model.FirstName);
                                _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.LastName, model.LastName);
                                if (_customerSettings.DateOfBirthEnabled)
                                {
                                    DateTime? dateOfBirth = model.ParseDateOfBirth();
                                    _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.DateOfBirth, dateOfBirth);
                                }
                                if (_customerSettings.CompanyEnabled)
                                    _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.Company, model.Company);
                                if (_customerSettings.StreetAddressEnabled)
                                    _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.StreetAddress, model.StreetAddress);
                                if (_customerSettings.StreetAddress2Enabled)
                                    _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.StreetAddress2, model.StreetAddress2);
                                if (_customerSettings.ZipPostalCodeEnabled)
                                    _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.ZipPostalCode, model.ZipPostalCode);
                                if (_customerSettings.CityEnabled)
                                    _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.City, model.City);
                                if (_customerSettings.CountryEnabled)
                                    _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.CountryId, model.CountryId);
                                if (_customerSettings.CountryEnabled && _customerSettings.StateProvinceEnabled)
                                    _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.StateProvinceId, model.StateProvinceId);
                                if (_customerSettings.PhoneEnabled)
                                    _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.Phone, model.Phone);
                                if (_customerSettings.FaxEnabled)
                                    _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.Fax, model.Fax);

                                //newsletter
                                if (_customerSettings.NewsletterEnabled)
                                {
                                    //save newsletter value
                                    var newsletter = _newsLetterSubscriptionService.GetNewsLetterSubscriptionByEmailAndStoreId(model.Email, _storeContext.CurrentStore.Id);
                                    if (newsletter != null)
                                    {
                                        if (model.Newsletter)
                                        {
                                            newsletter.Active = true;
                                            _newsLetterSubscriptionService.UpdateNewsLetterSubscription(newsletter);
                                        }
                                        //else
                                        //{
                                        //When registering, not checking the newsletter check box should not take an existing email address off of the subscription list.
                                        //_newsLetterSubscriptionService.DeleteNewsLetterSubscription(newsletter);
                                        //}
                                    }
                                    else
                                    {
                                        if (model.Newsletter)
                                        {
                                            _newsLetterSubscriptionService.InsertNewsLetterSubscription(new NewsLetterSubscription
                                            {
                                                NewsLetterSubscriptionGuid = Guid.NewGuid(),
                                                Email = model.Email,
                                                Active = true,
                                                StoreId = _storeContext.CurrentStore.Id,
                                                CreatedOnUtc = DateTime.UtcNow
                                            });
                                        }
                                    }
                                }

                                //save customer attributes
                                _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.CustomCustomerAttributes, customerAttributesXml);

                                //login customer now
                                if (isApproved)
                                    _authenticationService.SignIn(customer, true);

                                //associated with external account (if possible)
                                TryAssociateAccountWithExternalAccount(customer);

                                //insert default address (if possible)
                                var defaultAddress = new Address
                                {
                                    FirstName = customer.GetAttribute<string>(SystemCustomerAttributeNames.FirstName),
                                    LastName = customer.GetAttribute<string>(SystemCustomerAttributeNames.LastName),
                                    Email = customer.Email,
                                    Company = customer.GetAttribute<string>(SystemCustomerAttributeNames.Company),
                                    CountryId = customer.GetAttribute<int>(SystemCustomerAttributeNames.CountryId) > 0 ?
                                        (int?)customer.GetAttribute<int>(SystemCustomerAttributeNames.CountryId) : null,
                                    StateProvinceId = customer.GetAttribute<int>(SystemCustomerAttributeNames.StateProvinceId) > 0 ?
                                        (int?)customer.GetAttribute<int>(SystemCustomerAttributeNames.StateProvinceId) : null,
                                    City = customer.GetAttribute<string>(SystemCustomerAttributeNames.City),
                                    Address1 = customer.GetAttribute<string>(SystemCustomerAttributeNames.StreetAddress),
                                    Address2 = customer.GetAttribute<string>(SystemCustomerAttributeNames.StreetAddress2),
                                    ZipPostalCode = customer.GetAttribute<string>(SystemCustomerAttributeNames.ZipPostalCode),
                                    PhoneNumber = customer.GetAttribute<string>(SystemCustomerAttributeNames.Phone),
                                    FaxNumber = customer.GetAttribute<string>(SystemCustomerAttributeNames.Fax),
                                    CreatedOnUtc = customer.CreatedOnUtc
                                };
                                if (this._addressService.IsAddressValid(defaultAddress))
                                {
                                    //some validation
                                    if (defaultAddress.CountryId == 0)
                                        defaultAddress.CountryId = null;
                                    if (defaultAddress.StateProvinceId == 0)
                                        defaultAddress.StateProvinceId = null;
                                    //set default address
                                    customer.Addresses.Add(defaultAddress);
                                    customer.BillingAddress = defaultAddress;
                                    customer.ShippingAddress = defaultAddress;
                                    _customerService.UpdateCustomer(customer);
                                }

                                //notifications
                                if (_customerSettings.NotifyNewCustomerRegistration)
                                    _workflowMessageService.SendCustomerRegisteredNotificationMessage(customer, _localizationSettings.DefaultAdminLanguageId);

                                switch (_customerSettings.UserRegistrationType)
                                {
                                    case UserRegistrationType.EmailValidation:
                                        {
                                            //email validation message
                                            _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.AccountActivationToken, Guid.NewGuid().ToString());
                                            _workflowMessageService.SendCustomerEmailValidationMessage(customer, _workContext.WorkingLanguage.Id);

                                            //result
                                            //return RedirectToRoute("RegisterResult", new { resultId = (int)UserRegistrationType.EmailValidation });
                                            return RedirectToAction("Index", "Orders", new { area = "Vendor" });
                                        }
                                    case UserRegistrationType.AdminApproval:
                                        {
                                            //return RedirectToRoute("RegisterResult", new { resultId = (int)UserRegistrationType.AdminApproval });
                                            return RedirectToAction("Index", "Orders", new { area = "Vendor" });
                                        }
                                    case UserRegistrationType.Standard:
                                        {
                                            //send customer welcome message
                                            _workflowMessageService.SendCustomerWelcomeMessage(customer, _workContext.WorkingLanguage.Id);

                                            var redirectUrl = Url.RouteUrl("RegisterResult", new { resultId = (int)UserRegistrationType.Standard });
                                            if (!String.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                                                redirectUrl = _webHelper.ModifyQueryString(redirectUrl, "returnurl=" + HttpUtility.UrlEncode(returnUrl), null);
                                            //return Redirect(redirectUrl);
                                            return RedirectToAction("Index", "Orders", new { area = "Vendor" });
                                        }
                                    default:
                                        {
                                            return RedirectToAction("Index", "Orders", new { area = "Vendor" });
                                        }
                                }
                            }

                            //errors
                            foreach (var error in registrationResult.Errors)
                                ModelState.AddModelError("", error);
                        }
                    }
                    transaction.Commit();

                    //If we got this far, something failed, redisplay form
                    PrepareVendorRegisterModel(vrmodel);
                    return View(vrmodel);
                }
                catch(Exception e)
                {
                    transaction.Rollback();
                    ModelState.AddModelError("", e.Message);
                    PrepareVendorRegisterModel(vrmodel);
                    return View(vrmodel);
                }
            }
        }

        [NonAction]
        private bool ValidateVendorModel(VendorRegisterViewModel model)
        {
            bool isModelValid = true;
            var vendor = _vendorService.GetVendorByEmail(model.Email);
            if (vendor != null)
            {
                ModelState.AddModelError("Email", "This email is already taken.");
                isModelValid = false;
            }

            vendor = _vendorService.GetVendorByHost(model.PreferredSubdomainName);
            if (vendor != null)
            {
                ModelState.AddModelError("PreferredSubdomainName", "This subdomain is not available.");
                isModelValid = false;
            }

            return isModelValid;
        }
        
        public ActionResult RegisterResult(int resultId)
        {
            var resultText = "";
            switch ((UserRegistrationType)resultId)
            {
                case UserRegistrationType.Disabled:
                    resultText = _localizationService.GetResource("Account.Register.Result.Disabled");
                    break;
                case UserRegistrationType.Standard:
                    resultText = _localizationService.GetResource("Account.Register.Result.Standard");
                    break;
                case UserRegistrationType.AdminApproval:
                    resultText = _localizationService.GetResource("Account.Register.Result.AdminApproval");
                    break;
                case UserRegistrationType.EmailValidation:
                    resultText = _localizationService.GetResource("Account.Register.Result.EmailValidation");
                    break;
                default:
                    break;
            }
            var model = new RegisterResultModel
            {
                Result = resultText
            };
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CheckUsernameAvailability(string username)
        {
            var usernameAvailable = false;
            var statusText = _localizationService.GetResource("Account.CheckUsernameAvailability.NotAvailable");

            if (_customerSettings.UsernamesEnabled && !String.IsNullOrWhiteSpace(username))
            {
                if (_workContext.CurrentCustomer != null &&
                    _workContext.CurrentCustomer.Username != null &&
                    _workContext.CurrentCustomer.Username.Equals(username, StringComparison.InvariantCultureIgnoreCase))
                {
                    statusText = _localizationService.GetResource("Account.CheckUsernameAvailability.CurrentUsername");
                }
                else
                {
                    var customer = _customerService.GetCustomerByUsername(username);
                    if (customer == null)
                    {
                        statusText = _localizationService.GetResource("Account.CheckUsernameAvailability.Available");
                        usernameAvailable = true;
                    }
                }
            }

            return Json(new { Available = usernameAvailable, Text = statusText });
        }

        [NopHttpsRequirement(SslRequirement.Yes)]
        public ActionResult AccountActivation(string token, string email)
        {
            var customer = _customerService.GetCustomerByEmail(email);
            if (customer == null)
                return RedirectToRoute("HomePage");

            var cToken = customer.GetAttribute<string>(SystemCustomerAttributeNames.AccountActivationToken);
            if (String.IsNullOrEmpty(cToken))
                return RedirectToRoute("HomePage");

            if (!cToken.Equals(token, StringComparison.InvariantCultureIgnoreCase))
                return RedirectToRoute("HomePage");

            //activate user account
            customer.Active = true;
            _customerService.UpdateCustomer(customer);
            _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.AccountActivationToken, "");
            //send welcome message
            _workflowMessageService.SendCustomerWelcomeMessage(customer, _workContext.WorkingLanguage.Id);

            var model = new AccountActivationModel();
            model.Result = _localizationService.GetResource("Account.AccountActivation.Activated");
            return View(model);
        }

        #endregion

        #region My account / Info

        [ChildActionOnly]
        public ActionResult CustomerNavigation(int selectedTabId = 0)
        {
            var model = new CustomerNavigationModel();
            model.HideAvatar = !_customerSettings.AllowCustomersToUploadAvatars;
            model.HideRewardPoints = !_rewardPointsSettings.Enabled;
            model.HideForumSubscriptions = !_forumSettings.ForumsEnabled || !_forumSettings.AllowCustomersToManageSubscriptions;
            model.HideReturnRequests = !_orderSettings.ReturnRequestsEnabled ||
                _orderService.SearchReturnRequests(_storeContext.CurrentStore.Id, _workContext.CurrentCustomer.Id, 0, null, 0, 1).Count == 0;
            model.HideDownloadableProducts = _customerSettings.HideDownloadableProductsTab;
            model.HideBackInStockSubscriptions = _customerSettings.HideBackInStockSubscriptionsTab;

            model.SelectedTab = (CustomerNavigationEnum)selectedTabId;

            return PartialView(model);
        }

        [NopHttpsRequirement(SslRequirement.Yes)]
        public ActionResult Info()
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return new HttpUnauthorizedResult();

            var customer = _workContext.CurrentCustomer;

            var model = new CustomerInfoModel();
            PrepareCustomerInfoModel(model, customer, false);

            return View(model);
        }

        [HttpPost]
        [PublicAntiForgery]
        [ValidateInput(false)]
        public ActionResult Info(CustomerInfoModel model, FormCollection form)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return new HttpUnauthorizedResult();

            var customer = _workContext.CurrentCustomer;

            //custom customer attributes
            var customerAttributesXml = ParseCustomCustomerAttributes(form);
            var customerAttributeWarnings = _customerAttributeParser.GetAttributeWarnings(customerAttributesXml);
            foreach (var error in customerAttributeWarnings)
            {
                ModelState.AddModelError("", error);
            }

            try
            {
                if (ModelState.IsValid)
                {
                    //username 
                    if (_customerSettings.UsernamesEnabled && this._customerSettings.AllowUsersToChangeUsernames)
                    {
                        if (!customer.Username.Equals(model.Username.Trim(), StringComparison.InvariantCultureIgnoreCase))
                        {
                            //change username
                            _customerRegistrationService.SetUsername(customer, model.Username.Trim());
                            //re-authenticate
                            _authenticationService.SignIn(customer, true);
                        }
                    }
                    //email
                    if (!customer.Email.Equals(model.Email.Trim(), StringComparison.InvariantCultureIgnoreCase))
                    {
                        //change email
                        _customerRegistrationService.SetEmail(customer, model.Email.Trim());
                        //re-authenticate (if usernames are disabled)
                        if (!_customerSettings.UsernamesEnabled)
                        {
                            _authenticationService.SignIn(customer, true);
                        }
                    }

                    //properties
                    if (_dateTimeSettings.AllowCustomersToSetTimeZone)
                    {
                        _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.TimeZoneId, model.TimeZoneId);
                    }
                    //VAT number
                    if (_taxSettings.EuVatEnabled)
                    {
                        var prevVatNumber = customer.GetAttribute<string>(SystemCustomerAttributeNames.VatNumber);

                        _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.VatNumber, model.VatNumber);
                        if (prevVatNumber != model.VatNumber)
                        {
                            string vatName;
                            string vatAddress;
                            var vatNumberStatus = _taxService.GetVatNumberStatus(model.VatNumber, out vatName, out vatAddress);
                            _genericAttributeService.SaveAttribute(customer,
                                    SystemCustomerAttributeNames.VatNumberStatusId,
                                    (int)vatNumberStatus);
                            //send VAT number admin notification
                            if (!String.IsNullOrEmpty(model.VatNumber) && _taxSettings.EuVatEmailAdminWhenNewVatSubmitted)
                                _workflowMessageService.SendNewVatSubmittedStoreOwnerNotification(customer, model.VatNumber, vatAddress, _localizationSettings.DefaultAdminLanguageId);
                        }
                    }

                    //form fields
                    if (_customerSettings.GenderEnabled)
                        _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.Gender, model.Gender);
                    _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.FirstName, model.FirstName);
                    _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.LastName, model.LastName);
                    if (_customerSettings.DateOfBirthEnabled)
                    {
                        DateTime? dateOfBirth = model.ParseDateOfBirth();
                        _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.DateOfBirth, dateOfBirth);
                    }
                    if (_customerSettings.CompanyEnabled)
                        _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.Company, model.Company);
                    if (_customerSettings.StreetAddressEnabled)
                        _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.StreetAddress, model.StreetAddress);
                    if (_customerSettings.StreetAddress2Enabled)
                        _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.StreetAddress2, model.StreetAddress2);
                    if (_customerSettings.ZipPostalCodeEnabled)
                        _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.ZipPostalCode, model.ZipPostalCode);
                    if (_customerSettings.CityEnabled)
                        _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.City, model.City);
                    if (_customerSettings.CountryEnabled)
                        _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.CountryId, model.CountryId);
                    if (_customerSettings.CountryEnabled && _customerSettings.StateProvinceEnabled)
                        _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.StateProvinceId, model.StateProvinceId);
                    if (_customerSettings.PhoneEnabled)
                        _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.Phone, model.Phone);
                    if (_customerSettings.FaxEnabled)
                        _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.Fax, model.Fax);

                    //newsletter
                    if (_customerSettings.NewsletterEnabled)
                    {
                        //save newsletter value
                        var newsletter = _newsLetterSubscriptionService.GetNewsLetterSubscriptionByEmailAndStoreId(customer.Email, _storeContext.CurrentStore.Id);
                        if (newsletter != null)
                        {
                            if (model.Newsletter)
                            {
                                newsletter.Active = true;
                                _newsLetterSubscriptionService.UpdateNewsLetterSubscription(newsletter);
                            }
                            else
                                _newsLetterSubscriptionService.DeleteNewsLetterSubscription(newsletter);
                        }
                        else
                        {
                            if (model.Newsletter)
                            {
                                _newsLetterSubscriptionService.InsertNewsLetterSubscription(new NewsLetterSubscription
                                {
                                    NewsLetterSubscriptionGuid = Guid.NewGuid(),
                                    Email = customer.Email,
                                    Active = true,
                                    StoreId = _storeContext.CurrentStore.Id,
                                    CreatedOnUtc = DateTime.UtcNow
                                });
                            }
                        }
                    }

                    if (_forumSettings.ForumsEnabled && _forumSettings.SignaturesEnabled)
                        _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.Signature, model.Signature);

                    //save customer attributes
                    _genericAttributeService.SaveAttribute(_workContext.CurrentCustomer, SystemCustomerAttributeNames.CustomCustomerAttributes, customerAttributesXml);

                    return RedirectToRoute("CustomerInfo");
                }
            }
            catch (Exception exc)
            {
                ModelState.AddModelError("", exc.Message);
            }


            //If we got this far, something failed, redisplay form
            PrepareCustomerInfoModel(model, customer, true, customerAttributesXml);
            return View(model);
        }

        public ActionResult RemoveExternalAssociation(int id)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return new HttpUnauthorizedResult();

            //ensure it's our record
            var ear = _openAuthenticationService.GetExternalIdentifiersFor(_workContext.CurrentCustomer)
                .FirstOrDefault(x => x.Id == id);

            if (ear == null)
                return RedirectToAction("Info");

            _openAuthenticationService.DeletExternalAuthenticationRecord(ear);

            return RedirectToAction("Info");
        }

        #endregion

        #region My account / Addresses

        [NopHttpsRequirement(SslRequirement.Yes)]
        public ActionResult Addresses()
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return new HttpUnauthorizedResult();

            var customer = _workContext.CurrentCustomer;

            var model = new CustomerAddressListModel();
            var addresses = customer.Addresses
                //enabled for the current store
                .Where(a => a.Country == null || _storeMappingService.Authorize(a.Country))
                .ToList();
            foreach (var address in addresses)
            {
                var addressModel = new AddressModel();
                addressModel.PrepareModel(
                    address: address,
                    excludeProperties: false,
                    addressSettings: _addressSettings,
                    localizationService: _localizationService,
                    stateProvinceService: _stateProvinceService,
                    addressAttributeFormatter: _addressAttributeFormatter,
                    loadCountries: () => _countryService.GetAllCountries());
                model.Addresses.Add(addressModel);
            }
            return View(model);
        }

        [NopHttpsRequirement(SslRequirement.Yes)]
        public ActionResult AddressDelete(int addressId)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return new HttpUnauthorizedResult();

            var customer = _workContext.CurrentCustomer;

            //find address (ensure that it belongs to the current customer)
            var address = customer.Addresses.FirstOrDefault(a => a.Id == addressId);
            if (address != null)
            {
                customer.RemoveAddress(address);
                _customerService.UpdateCustomer(customer);
                //now delete the address record
                _addressService.DeleteAddress(address);
            }

            return RedirectToRoute("CustomerAddresses");
        }

        [NopHttpsRequirement(SslRequirement.Yes)]
        public ActionResult AddressAdd()
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return new HttpUnauthorizedResult();

            var model = new CustomerAddressEditModel();
            model.Address.PrepareModel(
                address: null,
                excludeProperties: false,
                addressSettings: _addressSettings,
                localizationService: _localizationService,
                stateProvinceService: _stateProvinceService,
                addressAttributeService: _addressAttributeService,
                addressAttributeParser: _addressAttributeParser,
                loadCountries: () => _countryService.GetAllCountries());

            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddressAdd(CustomerAddressEditModel model, FormCollection form)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return new HttpUnauthorizedResult();

            var customer = _workContext.CurrentCustomer;

            //custom address attributes
            var customAttributes = form.ParseCustomAddressAttributes(_addressAttributeParser, _addressAttributeService);
            var customAttributeWarnings = _addressAttributeParser.GetAttributeWarnings(customAttributes);
            foreach (var error in customAttributeWarnings)
            {
                ModelState.AddModelError("", error);
            }

            if (ModelState.IsValid)
            {
                var address = model.Address.ToEntity();
                address.CustomAttributes = customAttributes;
                address.CreatedOnUtc = DateTime.UtcNow;
                //some validation
                if (address.CountryId == 0)
                    address.CountryId = null;
                if (address.StateProvinceId == 0)
                    address.StateProvinceId = null;
                customer.Addresses.Add(address);
                _customerService.UpdateCustomer(customer);

                return RedirectToRoute("CustomerAddresses");
            }

            //If we got this far, something failed, redisplay form
            model.Address.PrepareModel(
                address: null,
                excludeProperties: true,
                addressSettings: _addressSettings,
                localizationService: _localizationService,
                stateProvinceService: _stateProvinceService,
                addressAttributeService: _addressAttributeService,
                addressAttributeParser: _addressAttributeParser,
                loadCountries: () => _countryService.GetAllCountries());

            return View(model);
        }

        [NopHttpsRequirement(SslRequirement.Yes)]
        public ActionResult AddressEdit(int addressId)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return new HttpUnauthorizedResult();

            var customer = _workContext.CurrentCustomer;
            //find address (ensure that it belongs to the current customer)
            var address = customer.Addresses.FirstOrDefault(a => a.Id == addressId);
            if (address == null)
                //address is not found
                return RedirectToRoute("CustomerAddresses");

            var model = new CustomerAddressEditModel();
            model.Address.PrepareModel(address: address,
                excludeProperties: false,
                addressSettings: _addressSettings,
                localizationService: _localizationService,
                stateProvinceService: _stateProvinceService,
                addressAttributeService: _addressAttributeService,
                addressAttributeParser: _addressAttributeParser,
                loadCountries: () => _countryService.GetAllCountries());

            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult AddressEdit(CustomerAddressEditModel model, int addressId, FormCollection form)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return new HttpUnauthorizedResult();

            var customer = _workContext.CurrentCustomer;
            //find address (ensure that it belongs to the current customer)
            var address = customer.Addresses.FirstOrDefault(a => a.Id == addressId);
            if (address == null)
                //address is not found
                return RedirectToRoute("CustomerAddresses");

            //custom address attributes
            var customAttributes = form.ParseCustomAddressAttributes(_addressAttributeParser, _addressAttributeService);
            var customAttributeWarnings = _addressAttributeParser.GetAttributeWarnings(customAttributes);
            foreach (var error in customAttributeWarnings)
            {
                ModelState.AddModelError("", error);
            }

            if (ModelState.IsValid)
            {
                address = model.Address.ToEntity(address);
                address.CustomAttributes = customAttributes;
                _addressService.UpdateAddress(address);

                return RedirectToRoute("CustomerAddresses");
            }

            //If we got this far, something failed, redisplay form
            model.Address.PrepareModel(
                address: address,
                excludeProperties: true,
                addressSettings: _addressSettings,
                localizationService: _localizationService,
                stateProvinceService: _stateProvinceService,
                addressAttributeService: _addressAttributeService,
                addressAttributeParser: _addressAttributeParser,
                loadCountries: () => _countryService.GetAllCountries());
            return View(model);
        }

        #endregion

        #region My account / Downloadable products

        [NopHttpsRequirement(SslRequirement.Yes)]
        public ActionResult DownloadableProducts()
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return new HttpUnauthorizedResult();

            var customer = _workContext.CurrentCustomer;

            var model = new CustomerDownloadableProductsModel();
            var items = _orderService.GetAllOrderItems(null, customer.Id, null, null,
                null, null, null, true);
            foreach (var item in items)
            {
                var itemModel = new CustomerDownloadableProductsModel.DownloadableProductsModel
                {
                    OrderItemGuid = item.OrderItemGuid,
                    OrderId = item.OrderId,
                    CreatedOn = _dateTimeHelper.ConvertToUserTime(item.Order.CreatedOnUtc, DateTimeKind.Utc),
                    ProductName = item.Product.GetLocalized(x => x.Name),
                    ProductSeName = item.Product.GetSeName(),
                    ProductAttributes = item.AttributeDescription,
                    ProductId = item.ProductId
                };
                model.Items.Add(itemModel);

                if (_downloadService.IsDownloadAllowed(item))
                    itemModel.DownloadId = item.Product.DownloadId;

                if (_downloadService.IsLicenseDownloadAllowed(item))
                    itemModel.LicenseId = item.LicenseDownloadId.HasValue ? item.LicenseDownloadId.Value : 0;
            }

            return View(model);
        }

        public ActionResult UserAgreement(Guid orderItemId)
        {
            var orderItem = _orderService.GetOrderItemByGuid(orderItemId);
            if (orderItem == null)
                return RedirectToRoute("HomePage");

            var product = orderItem.Product;
            if (product == null || !product.HasUserAgreement)
                return RedirectToRoute("HomePage");

            var model = new UserAgreementModel();
            model.UserAgreementText = product.UserAgreementText;
            model.OrderItemGuid = orderItemId;

            return View(model);
        }

        #endregion

        #region My account / Change password

        [NopHttpsRequirement(SslRequirement.Yes)]
        public ActionResult ChangePassword()
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return new HttpUnauthorizedResult();

            var model = new ChangePasswordModel();
            return View(model);
        }

        [HttpPost]
        [PublicAntiForgery]
        public ActionResult ChangePassword(ChangePasswordModel model)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return new HttpUnauthorizedResult();

            var customer = _workContext.CurrentCustomer;

            if (ModelState.IsValid)
            {
                var changePasswordRequest = new ChangePasswordRequest(customer.Email,
                    true, _customerSettings.DefaultPasswordFormat, model.NewPassword, model.OldPassword);
                var changePasswordResult = _customerRegistrationService.ChangePassword(changePasswordRequest);
                if (changePasswordResult.Success)
                {
                    model.Result = _localizationService.GetResource("Account.ChangePassword.Success");
                    return View(model);
                }

                //errors
                foreach (var error in changePasswordResult.Errors)
                    ModelState.AddModelError("", error);
            }


            //If we got this far, something failed, redisplay form
            return View(model);
        }

        #endregion

        #region My account / Avatar

        [NopHttpsRequirement(SslRequirement.Yes)]
        public ActionResult Avatar()
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return new HttpUnauthorizedResult();

            if (!_customerSettings.AllowCustomersToUploadAvatars)
                return RedirectToRoute("CustomerInfo");

            var customer = _workContext.CurrentCustomer;

            var model = new CustomerAvatarModel();
            model.AvatarUrl = _pictureService.GetPictureUrl(
                customer.GetAttribute<int>(SystemCustomerAttributeNames.AvatarPictureId),
                _mediaSettings.AvatarPictureSize,
                false);
            return View(model);
        }

        [HttpPost, ActionName("Avatar")]
        [FormValueRequired("upload-avatar")]
        public ActionResult UploadAvatar(CustomerAvatarModel model, HttpPostedFileBase uploadedFile)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return new HttpUnauthorizedResult();

            if (!_customerSettings.AllowCustomersToUploadAvatars)
                return RedirectToRoute("CustomerInfo");

            var customer = _workContext.CurrentCustomer;

            if (ModelState.IsValid)
            {
                try
                {
                    var customerAvatar = _pictureService.GetPictureById(customer.GetAttribute<int>(SystemCustomerAttributeNames.AvatarPictureId));
                    if ((uploadedFile != null) && (!String.IsNullOrEmpty(uploadedFile.FileName)))
                    {
                        int avatarMaxSize = _customerSettings.AvatarMaximumSizeBytes;
                        if (uploadedFile.ContentLength > avatarMaxSize)
                            throw new NopException(string.Format(_localizationService.GetResource("Account.Avatar.MaximumUploadedFileSize"), avatarMaxSize));

                        byte[] customerPictureBinary = uploadedFile.GetPictureBits();
                        if (customerAvatar != null)
                            customerAvatar = _pictureService.UpdatePicture(customerAvatar.Id, customerPictureBinary, uploadedFile.ContentType, null);
                        else
                            customerAvatar = _pictureService.InsertPicture(customerPictureBinary, uploadedFile.ContentType, null);
                    }

                    int customerAvatarId = 0;
                    if (customerAvatar != null)
                        customerAvatarId = customerAvatar.Id;

                    _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.AvatarPictureId, customerAvatarId);

                    model.AvatarUrl = _pictureService.GetPictureUrl(
                        customer.GetAttribute<int>(SystemCustomerAttributeNames.AvatarPictureId),
                        _mediaSettings.AvatarPictureSize,
                        false);
                    return View(model);
                }
                catch (Exception exc)
                {
                    ModelState.AddModelError("", exc.Message);
                }
            }


            //If we got this far, something failed, redisplay form
            model.AvatarUrl = _pictureService.GetPictureUrl(
                customer.GetAttribute<int>(SystemCustomerAttributeNames.AvatarPictureId),
                _mediaSettings.AvatarPictureSize,
                false);
            return View(model);
        }

        [HttpPost, ActionName("Avatar")]
        [FormValueRequired("remove-avatar")]
        public ActionResult RemoveAvatar(CustomerAvatarModel model, HttpPostedFileBase uploadedFile)
        {
            if (!_workContext.CurrentCustomer.IsRegistered())
                return new HttpUnauthorizedResult();

            if (!_customerSettings.AllowCustomersToUploadAvatars)
                return RedirectToRoute("CustomerInfo");

            var customer = _workContext.CurrentCustomer;

            var customerAvatar = _pictureService.GetPictureById(customer.GetAttribute<int>(SystemCustomerAttributeNames.AvatarPictureId));
            if (customerAvatar != null)
                _pictureService.DeletePicture(customerAvatar);
            _genericAttributeService.SaveAttribute(customer, SystemCustomerAttributeNames.AvatarPictureId, 0);

            return RedirectToRoute("CustomerAvatar");
        }

        #endregion
    }
}
