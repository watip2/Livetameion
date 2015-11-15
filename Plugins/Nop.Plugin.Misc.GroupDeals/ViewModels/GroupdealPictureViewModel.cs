using Nop.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Nop.Plugin.Misc.GroupDeals.ViewModels
{
    public class GroupdealPictureViewModel : BaseNopEntityModel
    {
        public int GroupdealId { get; set; }
        [UIHint("Picture")]
        public int PictureId { get; set; }
        public string PictureUrl { get; set; }
        public int DisplayOrder { get; set; }
        [AllowHtml]
        public string OverrideAltAttribute { get; set; }
        [AllowHtml]
        public string OverrideTitleAttribute { get; set; }
    }
}
