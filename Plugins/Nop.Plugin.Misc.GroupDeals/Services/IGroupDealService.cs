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
        int CreateGroupDealProduct(string groupDealName);
    }
}
