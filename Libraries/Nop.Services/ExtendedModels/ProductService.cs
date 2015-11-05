using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Nop.Core;
using Nop.Core.Caching;
using Nop.Core.Data;
using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Common;
using Nop.Core.Domain.Localization;
using Nop.Core.Domain.Orders;
using Nop.Core.Domain.Security;
using Nop.Core.Domain.Shipping;
using Nop.Core.Domain.Stores;
using Nop.Data;
using Nop.Services.Customers;
using Nop.Services.Events;
using Nop.Services.Localization;
using Nop.Services.Messages;
using Nop.Services.Security;
using Nop.Services.Stores;

namespace Nop.Services.Catalog
{
    /// <summary>
    /// Product service
    /// </summary>
    public partial class ProductService : IProductService
    {
        public IList<Product> GetAllProductsDisplayedOnHomePageByTenantId(int id)
        {
            var query = from p in _productRepository.Table
                        orderby p.DisplayOrder, p.Name
                        where p.Published &&
                        !p.Deleted &&
                        p.ShowOnHomePage &&
                        p.VendorId.Equals(id)
                        select p;
            var products = query.ToList();

            return products;
        }
    }
}
