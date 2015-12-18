using Nop.Core;
using Nop.Core.Domain.Catalog;
using Nop.Plugin.Misc.GroupDeals.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.GroupDeals.Services
{
    public interface IGroupDealService
    {
        void InsertGroupDeal(GroupDeal GroupDeal);
        GroupDeal GetGroupDealById(int GroupDealId);
        void DeleteGroupdeal(GroupDeal groupdeal);
        IEnumerable<GroupDeal> GetAllGroupDealsByVendorId(int vendorId);
        //IPagedList<GroupDeal> GetAllGroupdeals(string name = "",
        //    int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);
        IEnumerable<GroupDeal> GetAllGroupdeals();
        void UpdateGroupdeal(GroupDeal groupdeal);
        IList<GroupdealPicture> GetGroupdealPicturesByGroupdealId(int GroupdealId);
        GroupdealPicture GetGroupdealPictureById(int groupdealPictureId);
        void UpdateGroupdealPicture(GroupdealPicture groupdealPicture);
        void DeleteGroupdealPicture(GroupdealPicture groupdealPicture);
        void InsertGroupdealPicture(GroupdealPicture groupdealPicture);
        string GenerateGroupdealCouponCode();
        //////////////////////////////////////////////////////////////////////////////////////////
        void InsertGroupDealProduct(Product groupDealProduct);
        Product GetGroupDealProductById(int groupDealId);
        void DeleteGroupDealProduct(Product groupDealProduct);
        IEnumerable<GroupDeal> GetAllGroupDealProductsByVendorId(int vendorId);
        //IPagedList<GroupDeal> GetAllGroupdeals(string name = "",
        //    int pageIndex = 0, int pageSize = int.MaxValue, bool showHidden = false);
        IEnumerable<Product> GetAllGroupDealProducts();
        void UpdateGroupDealProduct(Product groupDealProduct);
        IList<GroupdealPicture> GetGroupDealProductPicturesByGroupDealId(int groupDealProductId);
        GroupdealPicture GetGroupDealProductPictureById(int groupDealPictureId);
        void UpdateGroupDealProductPicture(GroupdealPicture groupDealProductPicture);
        void DeleteGroupDealProductPicture(GroupdealPicture groupDealPicture);
        void InsertGroupDealProductPicture(GroupdealPicture groupDealProductPicture);
        string GenerateGroupDealProductCouponCode();
        int CreateGroupDealProduct(string Name, decimal Price);
        IPagedList<Product> SearchProducts(
            int pageIndex = 0,
            int pageSize = int.MaxValue,
            IList<int> categoryIds = null,
            int manufacturerId = 0,
            int storeId = 0,
            int vendorId = 0,
            int warehouseId = 0,
            ProductType? productType = null,
            bool visibleIndividuallyOnly = false,
            bool? featuredProducts = null,
            decimal? priceMin = null,
            decimal? priceMax = null,
            int productTagId = 0,
            string keywords = null,
            bool searchDescriptions = false,
            bool searchSku = true,
            bool searchProductTags = false,
            int languageId = 0,
            IList<int> filteredSpecs = null,
            ProductSortingEnum orderBy = ProductSortingEnum.Position,
            bool showHidden = false,
            bool? overridePublished = null);
        IPagedList<Product> SearchProducts(
            out IList<int> filterableSpecificationAttributeOptionIds,
            bool loadFilterableSpecificationAttributeOptionIds = false,
            int pageIndex = 0,
            int pageSize = 2147483647,  //Int32.MaxValue
            IList<int> categoryIds = null,
            int manufacturerId = 0,
            int storeId = 0,
            int vendorId = 0,
            int warehouseId = 0,
            ProductType? productType = null,
            bool visibleIndividuallyOnly = false,
            bool? featuredProducts = null,
            decimal? priceMin = null,
            decimal? priceMax = null,
            int productTagId = 0,
            string keywords = null,
            bool searchDescriptions = false,
            bool searchSku = true,
            bool searchProductTags = false,
            int languageId = 0,
            IList<int> filteredSpecs = null,
            ProductSortingEnum orderBy = ProductSortingEnum.Position,
            bool showHidden = false,
            bool? overridePublished = null);
    }
}
