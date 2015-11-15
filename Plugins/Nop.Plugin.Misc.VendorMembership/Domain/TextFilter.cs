using Nop.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nop.Plugin.Misc.VendorMembership.Domain
{
    public class TextFilter
    {
        public string Text = string.Empty;
        public string Operation = string.Empty;

        private void ClearFilter()
        {
            this.Text = string.Empty;
            this.Operation = string.Empty;
        }

        public void StartsWith(string Text)
        {
            this.ClearFilter();
            this.Text = Text;
            this.Operation = "STARTS_WITH";
        }

        public void EndsWith(string Text)
        {
            this.ClearFilter();
            this.Text = Text;
            this.Operation = "ENDS_WITH";
        }

        public void Is(string Text)
        {
            this.ClearFilter();
            this.Text = Text;
            this.Operation = "IS";
        }
    }
}
