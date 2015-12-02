using Nop.Core.Domain.Catalog;
using Nop.Core.Domain.Vendors;
using Nop.Core.Infrastructure;
using Nop.Services.Catalog;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Nop.Plugin.Tameion.Auctions.Helpers
{
    public class AuctionsHelper
    {
        // return countries as IEnumerable<Country>
        /*
         .GroupBy(x => x)    
         .Select(x => x.First())
         are used to get distinct countries because duplicate countries are being returned
        */
        //public static IEnumerable<Country> GetCountries()
        //{
        //    return CultureInfo.GetCultures(CultureTypes.SpecificCultures)
        //         .Select(x => new Country
        //         {
        //             Id = new RegionInfo(x.LCID).Name,
        //             Name = new RegionInfo(x.LCID).EnglishName
        //         })
        //            .GroupBy(c => c.Id)
        //            .Select(c => c.First())
        //            .OrderBy(x => x.Name);
        //}

        // return countries as IEnumerable<Country>
        /*
         .GroupBy(x => x)    
         .Select(x => x.First())
         are used to get distinct countries because duplicate countries are being returned
        */
        public static IEnumerable<string> GetCountriesNames()
        {
            return CultureInfo.GetCultures(CultureTypes.SpecificCultures)
                 .Select(x => new RegionInfo(x.LCID).EnglishName)
                 .GroupBy(x => x)    
                 .Select(x => x.First())
                 .OrderBy(x => x);
        }

        public static void CreateManyToManyRelationship(Vendor Vendor, int[] CategoryIds)
        {
            var categoryService = EngineContext.Current.Resolve<ICategoryService>();
            List<Category> Categories = new List<Category>();

            foreach(var CategoryId in CategoryIds)
            {
                var Category = categoryService.GetCategoryById(CategoryId);
                Categories.Add(Category);
            }

            foreach (var Category in Categories)
            {
                if (Category.Id.Equals(0))
                {
                    categoryService.InsertCategory(Category);
                }
            }

        }
    }
}
