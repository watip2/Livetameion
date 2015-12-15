using Nop.Admin.Models.Catalog;
using Nop.Plugin.Misc.GroupDeals.Enums;
using Nop.Web.Framework;
using Nop.Web.Framework.Localization;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Nop.Plugin.Misc.GroupDeals.ViewModels
{
    public class GroupDealViewModel : ProductModel
    {
        public List<SelectListItem> AvailableCountries { get; set; }
        [NopResourceDisplayName("Country")]
        public string Country { get; set; }
        [NopResourceDisplayName("State/Province")]
        public string StateOrProvince { get; set; }
        [NopResourceDisplayName("City")]
        public string City { get; set; }
        public string CouponCode { get; set; }
        public int GroupdealStatusId { get; set; }
        [NopResourceDisplayName("Fine Print")]
        public string FinePrint { get; set; }
        [NopResourceDisplayName("Status")]
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
