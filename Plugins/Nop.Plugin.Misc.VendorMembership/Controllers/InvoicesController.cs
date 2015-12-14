using Nop.Core;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Directory;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Payments;
using Nop.Core.Domain.Shipping;
using Nop.Core.Domain.Tax;
using Nop.Plugin.Misc.VendorMembership;
using Nop.Plugin.Misc.VendorMembership.ActionFilters;
using Nop.Plugin.Misc.VendorMembership.Domain;
using Nop.Plugin.Misc.VendorMembership.Services;
using Nop.Plugin.Misc.VendorMembership.ViewModels;
using Nop.Services.Affiliates;
using Nop.Services.Catalog;
using Nop.Services.Common;
using Nop.Services.Directory;
using Nop.Services.Discounts;
using Nop.Services.ExportImport;
using Nop.Services.Helpers;
using Nop.Services.Localization;
using Nop.Services.Media;
using Nop.Services.Messages;
using Nop.Services.Orders;
using Nop.Services.Payments;
using Nop.Services.Security;
using Nop.Services.Shipping;
using Nop.Services.Stores;
using Nop.Services.Vendors;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Kendoui;
using System;
using System.Linq;
using System.Web.Mvc;

namespace Nop.Plugin.Misc.VendorMembership.Controllers
{
    //[VendorAuthorize]
    public class InvoicesController : BasePluginController
    {
        private readonly IOrderService _orderService;
        private readonly IOrderReportService _orderReportService;
        private readonly IOrderProcessingService _orderProcessingService;
        private readonly IPriceCalculationService _priceCalculationService;
        private readonly IDateTimeHelper _dateTimeHelper;
        private readonly IPriceFormatter _priceFormatter;
        private readonly IDiscountService _discountService;
        private readonly ILocalizationService _localizationService;
        private readonly IWorkContext _workContext;
        private readonly ICurrencyService _currencyService;
        private readonly IEncryptionService _encryptionService;
        private readonly IPaymentService _paymentService;
        private readonly IMeasureService _measureService;
        private readonly IPdfService _pdfService;
        private readonly IAddressService _addressService;
        private readonly ICountryService _countryService;
        private readonly IStateProvinceService _stateProvinceService;
        private readonly Nop.Services.Catalog.IProductService _productService;
        private readonly IExportManager _exportManager;
        private readonly IPermissionService _permissionService;
        private readonly IWorkflowMessageService _workflowMessageService;
        private readonly ICategoryService _categoryService;
        private readonly IManufacturerService _manufacturerService;
        private readonly IProductAttributeService _productAttributeService;
        private readonly IProductAttributeParser _productAttributeParser;
        private readonly IProductAttributeFormatter _productAttributeFormatter;
        private readonly IShoppingCartService _shoppingCartService;
        private readonly IGiftCardService _giftCardService;
        private readonly IDownloadService _downloadService;
        private readonly IShipmentService _shipmentService;
        private readonly IShippingService _shippingService;
        private readonly IStoreService _storeService;
        private readonly IVendorService _vendorService;
        private readonly IAddressAttributeParser _addressAttributeParser;
        private readonly IAddressAttributeService _addressAttributeService;
        private readonly IAddressAttributeFormatter _addressAttributeFormatter;
        private readonly IAffiliateService _affiliateService;
        private readonly IPictureService _pictureService;

        private readonly CurrencySettings _currencySettings;
        private readonly TaxSettings _taxSettings;
        private readonly MeasureSettings _measureSettings;
        private readonly AddressSettings _addressSettings;
        private readonly ShippingSettings _shippingSettings;
        private readonly IInvoiceService _invoiceService;

        public InvoicesController(IOrderService orderService,
            IOrderReportService orderReportService,
            IOrderProcessingService orderProcessingService,
            IPriceCalculationService priceCalculationService,
            IDateTimeHelper dateTimeHelper,
            IPriceFormatter priceFormatter,
            IDiscountService discountService,
            ILocalizationService localizationService,
            IWorkContext workContext,
            ICurrencyService currencyService,
            IEncryptionService encryptionService,
            IPaymentService paymentService,
            IMeasureService measureService,
            IPdfService pdfService,
            IAddressService addressService,
            ICountryService countryService,
            IStateProvinceService stateProvinceService,
            Nop.Services.Catalog.IProductService productService,
            IExportManager exportManager,
            IPermissionService permissionService,
            IWorkflowMessageService workflowMessageService,
            ICategoryService categoryService,
            IManufacturerService manufacturerService,
            IProductAttributeService productAttributeService,
            IProductAttributeParser productAttributeParser,
            IProductAttributeFormatter productAttributeFormatter,
            IShoppingCartService shoppingCartService,
            IGiftCardService giftCardService,
            IDownloadService downloadService,
            IShipmentService shipmentService,
            IShippingService shippingService,
            IStoreService storeService,
            IVendorService vendorService,
            IAddressAttributeParser addressAttributeParser,
            IAddressAttributeService addressAttributeService,
            IAddressAttributeFormatter addressAttributeFormatter,
            IAffiliateService affiliateService,
            IPictureService pictureService,
            CurrencySettings currencySettings,
            TaxSettings taxSettings,
            MeasureSettings measureSettings,
            AddressSettings addressSettings,
            ShippingSettings shippingSettings,
            IInvoiceService invoiceService)
        {
            this._orderService = orderService;
            this._orderReportService = orderReportService;
            this._orderProcessingService = orderProcessingService;
            this._priceCalculationService = priceCalculationService;
            this._dateTimeHelper = dateTimeHelper;
            this._priceFormatter = priceFormatter;
            this._discountService = discountService;
            this._localizationService = localizationService;
            this._workContext = workContext;
            this._currencyService = currencyService;
            this._encryptionService = encryptionService;
            this._paymentService = paymentService;
            this._measureService = measureService;
            this._pdfService = pdfService;
            this._addressService = addressService;
            this._countryService = countryService;
            this._stateProvinceService = stateProvinceService;
            this._productService = productService;
            this._exportManager = exportManager;
            this._permissionService = permissionService;
            this._workflowMessageService = workflowMessageService;
            this._categoryService = categoryService;
            this._manufacturerService = manufacturerService;
            this._productAttributeService = productAttributeService;
            this._productAttributeParser = productAttributeParser;
            this._productAttributeFormatter = productAttributeFormatter;
            this._shoppingCartService = shoppingCartService;
            this._giftCardService = giftCardService;
            this._downloadService = downloadService;
            this._shipmentService = shipmentService;
            this._shippingService = shippingService;
            this._storeService = storeService;
            this._vendorService = vendorService;
            this._addressAttributeParser = addressAttributeParser;
            this._addressAttributeService = addressAttributeService;
            this._addressAttributeFormatter = addressAttributeFormatter;
            this._affiliateService = affiliateService;
            this._pictureService = pictureService;

            this._currencySettings = currencySettings;
            this._taxSettings = taxSettings;
            this._measureSettings = measureSettings;
            this._addressSettings = addressSettings;
            this._shippingSettings = shippingSettings;
            this._invoiceService = invoiceService;
        }

        public ActionResult Index()
        {
            return RedirectToAction("List");
        }
        
        public ActionResult List(int? orderStatusId = null,
            int? paymentStatusId = null, int? shippingStatusId = null)
        {
            //if (!_permissionService.Authorize(StandardPermissionProvider.ManageOrders))
            //    return AccessDeniedView();

            //if (_workContext.CurrentVendor != null)
            //{
            var orders = _orderService.SearchOrders();
            //}

            foreach (var order in orders)
            {
                var invoice = new Invoice
                {
                    OrderId = order.Id,
                    Commission = order.OrderTotal * new decimal(0.15),
                    StoreId = order.StoreId,
                    IsCommissionPaid = false,
                    CreatedOnUtc = DateTime.Now,
                    PaymentStatus = order.PaymentStatus,
                    ShippingStatus = order.ShippingStatus,
                    BillingAddress = order.BillingAddress,
                    OrderStatus = order.OrderStatus,
                };
                if (_invoiceService.GetInvoicesByOrderId(order.Id) == null)
                {
                    _invoiceService.InsertInvoice(invoice);
                }
                else
                {
                    _invoiceService.UpdateInvoice(invoice);
                }
            }

            //order statuses
            var model = new InvoiceListModel();
            model.AvailableOrderStatuses = OrderStatus.Pending.ToSelectList(false).ToList();
            model.AvailableOrderStatuses.Insert(0, new SelectListItem { Text = _localizationService.GetResource("Admin.Common.All"), Value = "0" });
            if (orderStatusId.HasValue)
            {
                //pre-select value?
                var item = model.AvailableOrderStatuses.FirstOrDefault(x => x.Value == orderStatusId.Value.ToString());
                if (item != null)
                    item.Selected = true;
            }

            //payment statuses
            model.AvailablePaymentStatuses = PaymentStatus.Pending.ToSelectList(false).ToList();
            model.AvailablePaymentStatuses.Insert(0, new SelectListItem { Text = _localizationService.GetResource("Admin.Common.All"), Value = "0" });
            if (paymentStatusId.HasValue)
            {
                //pre-select value?
                var item = model.AvailablePaymentStatuses.FirstOrDefault(x => x.Value == paymentStatusId.Value.ToString());
                if (item != null)
                    item.Selected = true;
            }

            //shipping statuses
            model.AvailableShippingStatuses = ShippingStatus.NotYetShipped.ToSelectList(false).ToList();
            model.AvailableShippingStatuses.Insert(0, new SelectListItem { Text = _localizationService.GetResource("Admin.Common.All"), Value = "0" });
            if (shippingStatusId.HasValue)
            {
                //pre-select value?
                var item = model.AvailableShippingStatuses.FirstOrDefault(x => x.Value == shippingStatusId.Value.ToString());
                if (item != null)
                    item.Selected = true;
            }

            //stores
            model.AvailableStores.Add(new SelectListItem { Text = _localizationService.GetResource("Admin.Common.All"), Value = "0" });
            foreach (var s in _storeService.GetAllStores())
                model.AvailableStores.Add(new SelectListItem { Text = s.Name, Value = s.Id.ToString() });

            ////vendors
            //model.AvailableVendors.Add(new SelectListItem { Text = _localizationService.GetResource("Admin.Common.All"), Value = "0" });
            //foreach (var v in _vendorService.GetAllVendors(showHidden: true))
            //    model.AvailableVendors.Add(new SelectListItem { Text = v.Name, Value = v.Id.ToString() });

            ////warehouses
            //model.AvailableWarehouses.Add(new SelectListItem { Text = _localizationService.GetResource("Admin.Common.All"), Value = "0" });
            //foreach (var w in _shippingService.GetAllWarehouses())
            //    model.AvailableWarehouses.Add(new SelectListItem { Text = w.Name, Value = w.Id.ToString() });

            //payment methods
            model.AvailablePaymentMethods.Add(new SelectListItem { Text = _localizationService.GetResource("Admin.Common.All"), Value = "" });
            foreach (var pm in _paymentService.LoadAllPaymentMethods())
                model.AvailablePaymentMethods.Add(new SelectListItem { Text = pm.PluginDescriptor.FriendlyName, Value = pm.PluginDescriptor.SystemName });

            ////billing countries
            //foreach (var c in _countryService.GetAllCountriesForBilling(true))
            //{
            //    model.AvailableCountries.Add(new SelectListItem { Text = c.Name, Value = c.Id.ToString() });
            //}
            //model.AvailableCountries.Insert(0, new SelectListItem { Text = _localizationService.GetResource("Admin.Common.All"), Value = "0" });

            //a vendor should have access only to orders with his products
            model.IsLoggedInAsVendor = _workContext.CurrentVendor != null;

            return View(model);
        }

        [HttpPost]
        public ActionResult InvoiceList(DataSourceRequest command, InvoiceListModel model)
        {
            //if (!_permissionService.Authorize(StandardPermissionProvider.ManageOrders))
            //    return AccessDeniedView();

            //a vendor should have access only to his products
            if (_workContext.CurrentVendor != null)
            {
                model.VendorId = _workContext.CurrentVendor.Id;
            }
            
            DateTime? startDateValue = (model.StartDate == null) ? null
                            : (DateTime?)_dateTimeHelper.ConvertToUtcTime(model.StartDate.Value, _dateTimeHelper.CurrentTimeZone);

            DateTime? endDateValue = (model.EndDate == null) ? null
                            : (DateTime?)_dateTimeHelper.ConvertToUtcTime(model.EndDate.Value, _dateTimeHelper.CurrentTimeZone).AddDays(1);

            OrderStatus? orderStatus = model.OrderStatusId > 0 ? (OrderStatus?)(model.OrderStatusId) : null;
            PaymentStatus? paymentStatus = model.PaymentStatusId > 0 ? (PaymentStatus?)(model.PaymentStatusId) : null;
            ShippingStatus? shippingStatus = model.ShippingStatusId > 0 ? (ShippingStatus?)(model.ShippingStatusId) : null;

            var filterByProductId = 0;
            var product = _productService.GetProductById(model.ProductId);
            //if (product != null && HasAccessToProduct(product))
            //    filterByProductId = model.ProductId;

            //load invoices
            var invoices = _invoiceService.SearchInvoices(storeId: model.StoreId,
                //vendorId: model.VendorId,
                productId: filterByProductId,
                warehouseId: model.WarehouseId,
                billingCountryId: model.BillingCountryId,
                paymentMethodSystemName: model.PaymentMethodSystemName,
                createdFromUtc: startDateValue,
                createdToUtc: endDateValue,
                os: orderStatus,
                ps: paymentStatus,
                ss: shippingStatus,
                billingEmail: model.CustomerEmail,
                orderNotes: model.OrderNotes,
                orderGuid: model.OrderGuid,
                pageIndex: command.Page - 1,
                pageSize: command.PageSize);

            foreach (var invoice in invoices)
            {
                var order = _orderService.GetOrderById(invoice.OrderId);
                invoice.OrderTotal = order.OrderTotal;
            }
            
            var gridModel = new DataSourceResult
            {
                Data = invoices.Select(x =>
                {
                    var store = _storeService.GetStoreById(x.StoreId);
                    return new InvoiceModel
                    {
                        Id = x.Id,
                        StoreName = store != null ? store.Name : "Unknown",
                        OrderTotal = _priceFormatter.FormatPrice(x.OrderTotal, true, false),
                        OrderStatus = x.OrderStatus.GetLocalizedEnum(_localizationService, _workContext),
                        PaymentStatus = x.PaymentStatus.GetLocalizedEnum(_localizationService, _workContext),
                        ShippingStatus = x.ShippingStatus.GetLocalizedEnum(_localizationService, _workContext),
            //            CustomerEmail = x.BillingAddress.Email,
            //            CustomerFullName = string.Format("{0} {1}", x.BillingAddress.FirstName, x.BillingAddress.LastName),
                        CreatedOn = _dateTimeHelper.ConvertToUserTime(x.CreatedOnUtc, DateTimeKind.Utc),
                        Commission = x.Commission
                    };
                }),
                Total = invoices.TotalCount
            };
            
            //summary report
            //currently we do not support productId and warehouseId parameters for this report
            var reportSummary = _orderReportService.GetOrderAverageReportLine(
                storeId: model.StoreId,
                vendorId: model.VendorId,
                billingCountryId: model.BillingCountryId,
                orderId: 0,
                paymentMethodSystemName: model.PaymentMethodSystemName,
                os: orderStatus,
                ps: paymentStatus,
                ss: shippingStatus,
                startTimeUtc: startDateValue,
                endTimeUtc: endDateValue,
                billingEmail: model.CustomerEmail,
                orderNotes: model.OrderNotes);
            var profit = _orderReportService.ProfitReport(
                storeId: model.StoreId,
                vendorId: model.VendorId,
                billingCountryId: model.BillingCountryId,
                paymentMethodSystemName: model.PaymentMethodSystemName,
                os: orderStatus,
                ps: paymentStatus,
                ss: shippingStatus,
                startTimeUtc: startDateValue,
                endTimeUtc: endDateValue,
                billingEmail: model.CustomerEmail,
                orderNotes: model.OrderNotes);
            var primaryStoreCurrency = _currencyService.GetCurrencyById(_currencySettings.PrimaryStoreCurrencyId);
            if (primaryStoreCurrency == null)
                throw new Exception("Cannot load primary store currency");

            gridModel.ExtraData = new OrderAggreratorModel
            {
                aggregatorprofit = _priceFormatter.FormatPrice(profit, true, false),
                aggregatorshipping = _priceFormatter.FormatShippingPrice(reportSummary.SumShippingExclTax, true, primaryStoreCurrency, _workContext.WorkingLanguage, false),
                aggregatortax = _priceFormatter.FormatPrice(reportSummary.SumTax, true, false),
                aggregatortotal = _priceFormatter.FormatPrice(reportSummary.SumOrders, true, false)
            };
            return new JsonResult
            {
                Data = gridModel
            };
        }

        //[HttpPost, ActionName("List")]
        //[FormValueRequired("go-to-order-by-number")]
        //public ActionResult GoToOrderId(OrderListModel model)
        //{
        //    var order = _orderService.GetOrderById(model.GoDirectlyToNumber);
        //    if (order == null)
        //        return List();

        //    return RedirectToAction("Edit", "Order", new { id = order.Id });
        //}

        //public ActionResult ProductSearchAutoComplete(string term)
        //{
        //    const int searchTermMinimumLength = 3;
        //    if (String.IsNullOrWhiteSpace(term) || term.Length < searchTermMinimumLength)
        //        return Content("");

        //    //a vendor should have access only to his products
        //    var vendorId = 0;
        //    if (_workContext.CurrentVendor != null)
        //    {
        //        vendorId = _workContext.CurrentVendor.Id;
        //    }

        //    //products
        //    const int productNumber = 15;
        //    var products = _productService.SearchProducts(
        //        vendorId: vendorId,
        //        keywords: term,
        //        pageSize: productNumber,
        //        showHidden: true);

        //    var result = (from p in products
        //                  select new
        //                  {
        //                      label = p.Name,
        //                      productid = p.Id
        //                  })
        //                  .ToList();
        //    return Json(result, JsonRequestBehavior.AllowGet);
        //}

        //[HttpPost, ActionName("List")]
        //[FormValueRequired("exportxml-all")]
        //public ActionResult ExportXmlAll(OrderListModel model)
        //{
        //    if (!_permissionService.Authorize(StandardPermissionProvider.ManageOrders))
        //        return AccessDeniedView();

        //    //a vendor cannot export orders
        //    if (_workContext.CurrentVendor != null)
        //        return AccessDeniedView();

        //    DateTime? startDateValue = (model.StartDate == null) ? null
        //                    : (DateTime?)_dateTimeHelper.ConvertToUtcTime(model.StartDate.Value, _dateTimeHelper.CurrentTimeZone);

        //    DateTime? endDateValue = (model.EndDate == null) ? null
        //                    : (DateTime?)_dateTimeHelper.ConvertToUtcTime(model.EndDate.Value, _dateTimeHelper.CurrentTimeZone).AddDays(1);

        //    OrderStatus? orderStatus = model.OrderStatusId > 0 ? (OrderStatus?)(model.OrderStatusId) : null;
        //    PaymentStatus? paymentStatus = model.PaymentStatusId > 0 ? (PaymentStatus?)(model.PaymentStatusId) : null;
        //    ShippingStatus? shippingStatus = model.ShippingStatusId > 0 ? (ShippingStatus?)(model.ShippingStatusId) : null;

        //    var filterByProductId = 0;
        //    var product = _productService.GetProductById(model.ProductId);
        //    if (product != null && HasAccessToProduct(product))
        //        filterByProductId = model.ProductId;

        //    //load orders
        //    var orders = _orderService.SearchOrders(storeId: model.StoreId,
        //        vendorId: model.VendorId,
        //        productId: filterByProductId,
        //        warehouseId: model.WarehouseId,
        //        billingCountryId: model.BillingCountryId,
        //        paymentMethodSystemName: model.PaymentMethodSystemName,
        //        createdFromUtc: startDateValue,
        //        createdToUtc: endDateValue,
        //        os: orderStatus,
        //        ps: paymentStatus,
        //        ss: shippingStatus,
        //        billingEmail: model.CustomerEmail,
        //        orderNotes: model.OrderNotes,
        //        orderGuid: model.OrderGuid);

        //    try
        //    {
        //        var xml = _exportManager.ExportOrdersToXml(orders);
        //        return new XmlDownloadResult(xml, "orders.xml");
        //    }
        //    catch (Exception exc)
        //    {
        //        ErrorNotification(exc);
        //        return RedirectToAction("List");
        //    }
        //}

        public ActionResult Pay(int invoiceId)
        {
            _invoiceService.GetInvoiceById(invoiceId);

            return View();
        }
    }
}
