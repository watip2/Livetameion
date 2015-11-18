using Nop.Core;
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
        GroupDeal GetById(int GroupDealId);
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
    }
}
