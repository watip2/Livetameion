using Nop.Core;
using Nop.Core.Domain.Media;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.GroupDeals.Models
{
    public class GroupdealPicture : BaseEntity
    {
        public int GroupdealId { get; set; }
        public int PictureId { get; set; }
        public int DisplayOrder { get; set; }

        [ForeignKey("GroupdealId")]
        public virtual GroupDeal GroupDeal { get; set; }
        [ForeignKey("PictureId")]
        public virtual Picture Picture { get; set; }
    }
}
