using Nop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.VendorMembership.Domain
{
    public class DateRangeFilter
    {
        public DateTime DateFrom = DateTime.MinValue;
        public DateTime DateTo = DateTime.MinValue;
        public string Operation = string.Empty;

        private void ClearFilter()
        {
            this.DateFrom = DateTime.MinValue;
            this.DateTo = DateTime.MinValue;
            this.Operation = string.Empty;
        }

        public void GreaterThan(DateTime DateTime)
        {
            this.ClearFilter();
            this.DateFrom = DateTime;
            this.Operation = "GREATER_THAN";
        }

        public void LessThan(DateTime DateTime)
        {
            this.ClearFilter();
            this.DateFrom = DateTime;
            this.Operation = "LESS_THAN";
        }

        public void EqualTo(DateTime DateTime)
        {
            this.ClearFilter();
            this.DateFrom = DateTime;
            this.Operation = "EQUAL_TO";
        }

        public void Between(DateTime DateFrom, DateTime DateTo)
        {
            this.ClearFilter();
            this.DateFrom = DateFrom;
            this.DateTo = DateTo;
            this.Operation = "BETWEEN";
        }
    }
}
