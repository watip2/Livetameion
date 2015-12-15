using System;
using System.Linq;

namespace Nop.Plugin.Tameion.Auctions.Models
{
    public class ModelsMapper
    {
        public T2 CreateMap<T1, T2>(T1 sourceEntity)
        {
            var source_props = typeof(T1).GetProperties();
            source_props = sourceEntity.GetType().GetProperties();
            // the above two statements are equavalent
            var dest_props = typeof(T2).GetProperties();
            T2 destEntity = Activator.CreateInstance<T2>();

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
                catch (Exception exc) { }
            }

            return destEntity;
        }
    }
}
