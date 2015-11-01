using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.VendorMembership.DTOs
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Name { get; set; }
        public List<Category> ChildrenCategories { get; set; }
    }
}
