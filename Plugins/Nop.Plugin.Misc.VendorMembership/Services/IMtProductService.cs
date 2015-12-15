using Nop.Core.Domain.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.VendorMembership.Services
{
    public interface IMtProductService
    {
        IList<Product> GetAllProductsDisplayedOnHomePage(string host = null);
    }
}
