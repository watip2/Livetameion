using System;
using System.Collections.Generic;
using System.Web.Routing;
using Nop.Core.Domain.Orders;
using Nop.Core.Plugins;
using Nop.Services.Payments;
using Nop.Services.Customers;
using Nop.Plugin.Tameion.BridgePay.Infrastructure;
using System.Net;
using System.Collections.Specialized;
using Nop.Plugin.Tameion.BridgePay.Models;
using Nop.Services.Configuration;
using Nop.Services.Directory;
using Nop.Core.Domain.Directory;
using Nop.Core;
using Nop.Services.Orders;
using Nop.Services.Security;
using Nop.Plugin.Tameion.BridgePay.Controller;
using System.IO;

namespace Nop.Plugin.Tameion.BridgePay
{
    public class PluginConfig : BasePlugin, IPaymentMethod
    {
        public override PluginDescriptor PluginDescriptor { get; set; }
        private readonly BridgePaySettings _bridgePaySettings;
        private readonly ISettingService _settingService;
        private readonly ICurrencyService _currencyService;
        private readonly ICustomerService _customerService;
        private readonly CurrencySettings _currencySettings;
        private readonly IWebHelper _webHelper;
        private readonly IOrderTotalCalculationService _orderTotalCalculationService;
        private readonly IEncryptionService _encryptionService;
        private readonly BridgePayContext _bridgePayContext;

        public PluginConfig(BridgePaySettings bridgePaySettings,
            ISettingService settingService,
            ICurrencyService currencyService,
            ICustomerService customerService,
            CurrencySettings currencySettings,
            IWebHelper webHelper,
            IOrderTotalCalculationService orderTotalCalculationService,
            IEncryptionService encryptionService,
            BridgePayContext bridgePayContext)
        {
            this._bridgePaySettings = bridgePaySettings;
            this._settingService = settingService;
            this._currencyService = currencyService;
            this._customerService = customerService;
            this._currencySettings = currencySettings;
            this._webHelper = webHelper;
            this._orderTotalCalculationService = orderTotalCalculationService;
            this._encryptionService = encryptionService;
            _bridgePayContext = bridgePayContext;
        }

        public override void Install()
        {
            try
            {
                _bridgePayContext.Install();
            }
            catch (Exception e) { }
            base.Install();
        }

        public override void Uninstall()
        {
            try
            {
                _bridgePayContext.Uninstall();
            }
            catch (Exception e) { }
            base.Uninstall();
        }
        
        public PaymentMethodType PaymentMethodType
        {
            get
            {
                return PaymentMethodType.Standard;
            }
        }
        
        public RecurringPaymentType RecurringPaymentType
        {
            get
            {
                return RecurringPaymentType.Manual;
            }
        }

        public bool SkipPaymentInfo
        {
            get
            {
                return false;
            }
        }

        public bool SupportCapture
        {
            get
            {
                return true;
            }
        }

        public bool SupportPartiallyRefund
        {
            get
            {
                return true;
            }
        }

        public bool SupportRefund
        {
            get
            {
                return true;
            }
        }

        public bool SupportVoid
        {
            get
            {
                return true;
            }
        }

        public CancelRecurringPaymentResult CancelRecurringPayment(CancelRecurringPaymentRequest cancelPaymentRequest)
        {
            throw new NotImplementedException();
        }

        public bool CanRePostProcessPayment(Order order)
        {
            throw new NotImplementedException();
        }

        public CapturePaymentResult Capture(CapturePaymentRequest capturePaymentRequest)
        {
            throw new NotImplementedException();
        }

        public decimal GetAdditionalHandlingFee(IList<ShoppingCartItem> cart)
        {
            var result = this.CalculateAdditionalFee(_orderTotalCalculationService, cart,
                _bridgePaySettings.AdditionalFee, _bridgePaySettings.AdditionalFeePercentage);
            return result;
        }

        public void GetConfigurationRoute(out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            actionName = "Configure";
            controllerName = "BridgePay";
            routeValues = new RouteValueDictionary { { "Namespaces", "Nop.Plugin.Tameion.BridgePay.Controllers" }, { "area", null } };
        }

        public Type GetControllerType()
        {
            return typeof(BridgePayController);
        }

        public void GetPaymentInfoRoute(out string actionName, out string controllerName, out RouteValueDictionary routeValues)
        {
            actionName = "PaymentInfo";
            controllerName = "BridgePay";
            routeValues = new RouteValueDictionary { { "Namespaces", "Nop.Plugin.Tameion.BridgePay.Controllers" }, { "area", null } };
        }

        public bool HidePaymentMethod(IList<ShoppingCartItem> cart)
        {
            //you can put any logic here
            //for example, hide this payment method if all products in the cart are downloadable
            //or hide this payment method if current customer is from certain country
            return false;
        }

        public void PostProcessPayment(PostProcessPaymentRequest postProcessPaymentRequest)
        {
            // nothing
        }

        public ProcessPaymentResult ProcessPayment(ProcessPaymentRequest processPaymentRequest)
        {
            string serviceUrl = string.Format("https://gatewaystage.itstgate.com/SmartPayments/transact.asmx/ProcessCreditCard?UserName=Gyne4392&Password=J4066nh8&TransType=Sale&CardNum=4111111111111111&ExpDate=0117&MagData=data&NameOnCard=sohail&Amount=10&InvNum=1&PNRef=1&Zip=43600&Street=Kamra&CVNum=023&ExtData=ext-data");
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(serviceUrl);
            try
            {
                var httpResponse = (HttpWebResponse)request.GetResponse();
                //Receipt Receipt = null;
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var responstText = streamReader.ReadToEnd();
                    //Receipt = serializer.Deserialize<Receipt>(responstText);
                }
            }
            catch (Exception ex)
            { }
            /////////////////////////////////////////////////////////////////////////////////////////////////
            var result = new ProcessPaymentResult();

            var customer = _customerService.GetCustomerById(processPaymentRequest.CustomerId);

            var webClient = new WebClient();
            var form = new NameValueCollection();
            //form.Add("x_login", _authorizeNetPaymentSettings.LoginId);
            //form.Add("x_tran_key", _authorizeNetPaymentSettings.TransactionKey);
            
            ////we should not send "x_test_request" parameter. otherwise, the transaction won't be logged in the sandbox
            ////if (_authorizeNetPaymentSettings.UseSandbox)
            ////    form.Add("x_test_request", "TRUE");
            ////else
            ////    form.Add("x_test_request", "FALSE");

            //form.Add("x_delim_data", "TRUE");
            //form.Add("x_delim_char", "|");
            //form.Add("x_encap_char", "");
            //form.Add("x_version", GetApiVersion());
            //form.Add("x_relay_response", "FALSE");
            //form.Add("x_method", "CC");
            //form.Add("x_currency_code", _currencyService.GetCurrencyById(_currencySettings.PrimaryStoreCurrencyId).CurrencyCode);
            //if (_authorizeNetPaymentSettings.TransactMode == TransactMode.Authorize)
            //    form.Add("x_type", "AUTH_ONLY");
            //else if (_authorizeNetPaymentSettings.TransactMode == TransactMode.AuthorizeAndCapture)
            //    form.Add("x_type", "AUTH_CAPTURE");
            //else
            //    throw new NopException("Not supported transaction mode");

            //var orderTotal = Math.Round(processPaymentRequest.OrderTotal, 2);
            //form.Add("x_amount", orderTotal.ToString("0.00", CultureInfo.InvariantCulture));
            //form.Add("x_card_num", processPaymentRequest.CreditCardNumber);
            //form.Add("x_exp_date", processPaymentRequest.CreditCardExpireMonth.ToString("D2") + processPaymentRequest.CreditCardExpireYear.ToString());
            //form.Add("x_card_code", processPaymentRequest.CreditCardCvv2);
            //form.Add("x_first_name", customer.BillingAddress.FirstName);
            //form.Add("x_last_name", customer.BillingAddress.LastName);
            //form.Add("x_email", customer.BillingAddress.Email);
            //if (!string.IsNullOrEmpty(customer.BillingAddress.Company))
            //    form.Add("x_company", customer.BillingAddress.Company);
            //form.Add("x_address", customer.BillingAddress.Address1);
            //form.Add("x_city", customer.BillingAddress.City);
            //if (customer.BillingAddress.StateProvince != null)
            //    form.Add("x_state", customer.BillingAddress.StateProvince.Abbreviation);
            //form.Add("x_zip", customer.BillingAddress.ZipPostalCode);
            //if (customer.BillingAddress.Country != null)
            //    form.Add("x_country", customer.BillingAddress.Country.TwoLetterIsoCode);
            ////x_invoice_num is 20 chars maximum. hece we also pass x_description
            //form.Add("x_invoice_num", processPaymentRequest.OrderGuid.ToString().Substring(0, 20));
            //form.Add("x_description", string.Format("Full order #{0}", processPaymentRequest.OrderGuid));
            //form.Add("x_customer_ip", _webHelper.GetCurrentIpAddress());

            //var responseData = webClient.UploadValues(GetAuthorizeNetUrl(), form);
            //var reply = Encoding.ASCII.GetString(responseData);

            //if (!String.IsNullOrEmpty(reply))
            //{
            //    string[] responseFields = reply.Split('|');
            //    switch (responseFields[0])
            //    {
            //        case "1":
            //            result.AuthorizationTransactionCode = string.Format("{0},{1}", responseFields[6], responseFields[4]);
            //            result.AuthorizationTransactionResult = string.Format("Approved ({0}: {1})", responseFields[2], responseFields[3]);
            //            result.AvsResult = responseFields[5];
            //            //responseFields[38];
            //            if (_authorizeNetPaymentSettings.TransactMode == TransactMode.Authorize)
            //            {
            //                result.NewPaymentStatus = PaymentStatus.Authorized;
            //            }
            //            else
            //            {
            //                result.NewPaymentStatus = PaymentStatus.Paid;
            //            }
            //            break;
            //        case "2":
            //            result.AddError(string.Format("Declined ({0}: {1})", responseFields[2], responseFields[3]));
            //            break;
            //        case "3":
            //            result.AddError(string.Format("Error: {0}", reply));
            //            break;

            //    }
            //}
            //else
            //{
            //    result.AddError("Authorize.NET unknown error");
            //}

            return result;
        }

        public ProcessPaymentResult ProcessRecurringPayment(ProcessPaymentRequest processPaymentRequest)
        {
            throw new NotImplementedException();
        }

        public RefundPaymentResult Refund(RefundPaymentRequest refundPaymentRequest)
        {
            throw new NotImplementedException();
        }

        public VoidPaymentResult Void(VoidPaymentRequest voidPaymentRequest)
        {
            throw new NotImplementedException();
        }
    }
}
