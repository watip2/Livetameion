using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.GroupDeals.Models
{
    public class ModelsMapper
    {
        public T2 CreateMap<T1, T2>(T1 sourceEntity, T2 destEntity = default(T2))
        {
            var source_props = typeof(T1).GetProperties();
            source_props = sourceEntity.GetType().GetProperties();
            // the above two statements are equavalent
            var dest_props = typeof(T2).GetProperties();
            if (destEntity == null)
            {
                destEntity = Activator.CreateInstance<T2>();
            }

            foreach (var source_prop in source_props)
            {

                try
                {
                    var dest_prop = dest_props.Where(p => p.Name.Equals(source_prop.Name)).SingleOrDefault();
                    if (dest_prop != null && source_prop.GetType() == dest_prop.GetType())
                    {
                        dest_prop.SetValue(destEntity, source_prop.GetValue(sourceEntity), null);
                    }
                }
                catch (Exception) { }
            }

            return destEntity;
        }
    }
}
