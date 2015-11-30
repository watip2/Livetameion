using Nop.Admin.Models.Catalog;
using Nop.Plugin.Misc.GroupDeals.Enums;
using Nop.Web.Framework.Localization;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Nop.Plugin.Misc.GroupDeals.ViewModels
{
    public class GroupDealViewModel : ProductModel
    {
        public List<SelectListItem> AvailableCountries { get; set; }
        public string Country { get; set; }
        public string StateOrProvince { get; set; }
        public string City { get; set; }
        public string CouponCode { get; set; }
        public int GroupdealStatusId { get; set; }
        public GroupdealStatus GroupdealStatus
        {
            get { return (GroupdealStatus)this.GroupdealStatusId; }
            set { this.GroupdealStatusId = (int)value; }
        }
        public string GroupdealStatusName { get; set; }
        
        public GroupdealPictureViewModel GroupdealPictureViewModel { get; set; }   
    }

    public partial class GroupDealLocalizedModel : ProductLocalizedModel, ILocalizedModel
    {

    }
}
