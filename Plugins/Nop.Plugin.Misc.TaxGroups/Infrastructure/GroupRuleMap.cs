using Nop.Plugin.Misc.TaxGroups.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.TaxGroups.Infrastructure
{
    public class GroupRuleMap : EntityTypeConfiguration<GroupRule>
    {
        public GroupRuleMap()
        {
            // the Install() method of ObjectContext class will create table with the following name
            // this table name is overwritten by the name of navigational property in parent class
            ToTable("GroupRules");
            HasKey(gr => gr.Id);

            Property(gr => gr.Name);
            Property(gr => gr.Active);
            Property(gr => gr.CreatedOnUtc);
            Property(gr => gr.Deleted);
            Property(gr => gr.DisplayOrder);
            Property(gr => gr.SeName);
            Property(gr => gr.UpdatedOnUtc);
        }
    }
}
