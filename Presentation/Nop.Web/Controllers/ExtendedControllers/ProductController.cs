﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.ServiceModel.Syndication;
using System.Web.Mvc;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Customers;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Media;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Seo;
using Nop.Core.Domain.Vendors;
using Nop.Services.Catalog;
using Nop.Services.Customers;
using Nop.Services.Directory;
using Nop.Services.Events;
using Nop.Services.Helpers;
using Nop.Services.Localization;
using Nop.Services.Logging;
using Nop.Services.Media;
using Nop.Services.Messages;
using Nop.Services.Orders;
using Nop.Services.Security;
using Nop.Services.Seo;
using Nop.Services.Shipping;
using Nop.Services.Stores;
using Nop.Services.Tax;
using Nop.Services.Vendors;
using Nop.Web.Extensions;
using Nop.Web.Framework;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Security;
using Nop.Web.Framework.Security.Captcha;
using Nop.Web.Infrastructure.Cache;
using Nop.Web.Models.Catalog;
using Nop.Web.Models.Media;

namespace Nop.Web.Controllers
{
    public partial class ProductController : BasePublicController
    {
        [ChildActionOnly]
        public ActionResult HomepageProductsMT(int? productThumbPictureSize)
        {
            var host = this.Request.Headers["Host"].ToString();
            var vendor = _vendorService.GetVendorByHost(host);
            IEnumerable<Product> products;
            if (vendor != null)
            {
                // get tenant's products
                products = _productService.GetAllProductsDisplayedOnHomePageByTenantId(vendor.Id);
            }
            else
            {
                // get main store's products
                products = _productService.GetAllProductsDisplayedOnHomePage();
            }
            
            //ACL and store mapping
            products = products.Where(p => _aclService.Authorize(p) && _storeMappingService.Authorize(p)).ToList();
            //availability dates
            products = products.Where(p => p.IsAvailable()).ToList();

            if (products.Count() == 0)
                return Content("");

            var model = PrepareProductOverviewModels(products, true, true, productThumbPictureSize).ToList();
            return PartialView("HomepageProducts", model);
        }
    }
}
