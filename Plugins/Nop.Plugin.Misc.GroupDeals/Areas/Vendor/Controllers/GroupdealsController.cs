using Nop.Core;
using Nop.Core.Data;
using Nop.Plugin.Misc.GroupDeals.Models;
using Nop.Plugin.Misc.GroupDeals.Services;
using Nop.Services.Catalog;
using Nop.Services.Helpers;
using Nop.Services.Media;
using Nop.Services.Vendors;
using Nop.Services.Seo;
using Nop.Services.Directory;
using Nop.Core.Domain.Directory;
using Nop.Services.Localization;
using Nop.Services.Customers;
using Nop.Services.Tax;
using Nop.Services.Common;
using Nop.Services.ExportImport;
using Nop.Services.Logging;
using Nop.Services.Security;
using Nop.Services.Stores;
using Nop.Services.Orders;
using Nop.Services.Shipping;
using Nop.Core.Domain.Common;
using Nop.Services.Discounts;
using Nop.Plugin.Misc.VendorMembership.ActionFilters;

namespace Nop.Plugin.Misc.GroupDeals.Areas.Vendor.Controllers
{
    [VendorAuthorize]
    public class GroupDealsController : BaseGroupDealsController
    {
        public GroupDealsController(
            IProductService productService,
            IProductTemplateService productTemplateService,
            ICategoryService categoryService,
            IManufacturerService manufacturerService,
            ICustomerService customerService,
            IUrlRecordService urlRecordService,
            IWorkContext workContext,
            ILanguageService languageService,
            ILocalizationService localizationService,
            ILocalizedEntityService localizedEntityService,
            ISpecificationAttributeService specificationAttributeService,
            IPictureService pictureService,
            ITaxCategoryService taxCategoryService,
            IProductTagService productTagService,
            ICopyProductService copyProductService,
            IPdfService pdfService,
            IExportManager exportManager,
            IImportManager importManager,
            ICustomerActivityService customerActivityService,
            IPermissionService permissionService,
            IAclService aclService,
            IStoreService storeService,
            IOrderService orderService,
            IStoreMappingService storeMappingService,
            IVendorService vendorService,
            IShippingService shippingService,
            IShipmentService shipmentService,
            ICurrencyService currencyService,
            CurrencySettings currencySettings,
            IMeasureService measureService,
            MeasureSettings measureSettings,
            AdminAreaSettings adminAreaSettings,
            IDateTimeHelper dateTimeHelper,
            IDiscountService discountService,
            IProductAttributeService productAttributeService,
            IBackInStockSubscriptionService backInStockSubscriptionService,
            IShoppingCartService shoppingCartService,
            IProductAttributeFormatter productAttributeFormatter,
            IProductAttributeParser productAttributeParser,
            IDownloadService downloadService,
            IRepository<GroupDeal> groupDealRepo,
            IRepository<GroupdealPicture> groupdealPictureRepo,
            IGroupDealService groupdealService,
            IGenericAttributeService genericAttributeService)
            : base(productService,
            productTemplateService,
            categoryService,
            manufacturerService,
            customerService,
            urlRecordService,
            workContext,
            languageService,
            localizationService,
            localizedEntityService,
            specificationAttributeService,
            pictureService,
            taxCategoryService,
            productTagService,
            copyProductService,
            pdfService,
            exportManager,
            importManager,
            customerActivityService,
            permissionService,
            aclService,
            storeService,
            orderService,
            storeMappingService,
            vendorService,
            shippingService,
            shipmentService,
            currencyService,
            currencySettings,
            measureService,
            measureSettings,
            adminAreaSettings,
            dateTimeHelper,
            discountService,
            productAttributeService,
            backInStockSubscriptionService,
            shoppingCartService,
            productAttributeFormatter,
            productAttributeParser,
            downloadService,
            groupDealRepo,
            groupdealPictureRepo,
            groupdealService,
            genericAttributeService)
        { }
    }
}